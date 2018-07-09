using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.EnterpriseServices;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ApiContent.Models;
using ApiContent.Models.DTOs;
using AutoMapper;
using HtmlAgilityPack;

namespace ApiContent.DataAccess
{
    public class ContentData : ContentDataBase, IContentData
    {

        private ITextData _areaTextData;
        private const string TEXT_ID = "data-ac-text-id";
        private const string TEXT_NAME = "data-ac-text-name";

        public ContentData()
        {
            _areaTextData = new TextData();
        }

        public async Task<int> AddContent(ContentDTO content)
        {
            var dbContent = Mapper.Map<ContentDTO, Content>(content);
            _dataContext.Contents.Add(dbContent);
            await _dataContext.SaveChangesAsync();
            await AddContentTexts(dbContent.Id, content.ContentTexts);
            return dbContent.Id;
        }

        public async Task<int> UpdateContent(ContentDTO content)
        {
            var existing = await _dataContext.Contents.FirstOrDefaultAsync(x => x.Id == content.Id);
            if (existing == null)
            {
                return await AddContent(content);
            }
            Mapper.Map(content, existing);
            _dataContext.Entry(content).State = EntityState.Modified;
            _dataContext.ContentTexts.RemoveRange(_dataContext.ContentTexts.Where(x => x.ParentContentId == content.Id));
            await AddContentTexts(existing.Id, content.ContentTexts);
            await _dataContext.SaveChangesAsync();
            return content.Id;
        }

        public async Task<Content> GetContent(int id)
        {
            return await _dataContext.Contents.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ContentDTO> GetContentAll(int id, bool includeEditMarks, string language = null)
        {
            var content = await _dataContext.Contents.Include(x => x.Template).FirstOrDefaultAsync(x => x.Id == id);
            var contentDto = Mapper.Map<Content, ContentDTO>(content);
            contentDto.Html = await GetFullContent(content.Template.Html, content.Id, language, includeEditMarks);
            return contentDto;
        }

        public async Task<List<Content>> GetContents(string filter)
        {
            ContentFilter uFilter = new ContentFilter(filter);
            IQueryable<Content> areas = _dataContext.Contents;
            if (!string.IsNullOrWhiteSpace(uFilter.Name)) areas = areas.Where(a => a.Name.Contains(uFilter.Name));
            return await areas.ToListAsync();
        }

        public async Task<string> GetFullContent(string htmlIn, int contentId, string language, bool includeEditMarks)
        {
            var items = await _dataContext.ContentTexts.Where(x => x.ParentContentId == contentId).ToListAsync();
            var dbTexts = await _areaTextData.GetTexts(items.Select(x => x.ContentTextId).ToList(), language);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlIn);
            var inclusions = htmlDoc.DocumentNode.SelectNodes("//div[@" + INCLUSION + "]");
            int iSeq = -1;
            foreach (var inclusion in inclusions)
            {
                inclusion.Attributes.Remove(INCLUSION);

                iSeq++;
                if (iSeq >= dbTexts.Count)
                {
                    inclusion.InnerHtml = GetEmptyContent(iSeq, includeEditMarks); 
                    continue;
                }
                var item = items.FirstOrDefault(x => x.Seq == iSeq);
                if (item == null)
                {
                    inclusion.InnerHtml = GetEmptyContent(iSeq, includeEditMarks);
                    continue;
                }

                if (includeEditMarks)
                {
                    inclusion.Attributes.Add(EDITOR_CLASS, null);
                }
                var text = dbTexts.FirstOrDefault(x => x.Id == item.ContentTextId);
                if (string.IsNullOrEmpty(text?.Html))
                {
                    inclusion.InnerHtml = GetEmptyContent(iSeq, includeEditMarks);
                    continue;
                }
                if (includeEditMarks)
                {
                    inclusion.Attributes.Add(TEXT_ID, item.ContentTextId.ToString());
                    inclusion.Attributes.Add(TEXT_NAME, text.Text.Name);
                }

                inclusion.InnerHtml = text.Html;
            }
            return htmlDoc.DocumentNode.OuterHtml;

        }

        private async Task AddContentTexts(int parentId, List<IncludedItemDTO> items)
        {
            foreach (var item in items)
            {
                var dbItem = new ContentText();
                dbItem.ParentContentId = parentId;
                dbItem.ContentTextId = item.Id;
                dbItem.Seq = item.Seq;
                _dataContext.ContentTexts.Add(dbItem);
            }
            await _dataContext.SaveChangesAsync();
        }

    }
}