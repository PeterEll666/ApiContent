using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ApiContent.Models;
using ApiContent.DataAccess;
using AutoMapper;
using ApiContent.Models.DTOs;
using HtmlAgilityPack;

namespace ApiContent.DataAccess
{
    public class PageData : ContentDataBase, IPageData
    {
        private IContentData _contentData;

        private const string CONTENT_ID = "data-ac-content-id";
        private const string CONTENT_NAME = "data-ac-content-name";

        public PageData()
        {
            _contentData = new ContentData();
        }

        public async Task<int> AddPage(PageDTO page)
        {
            var dbPage = Mapper.Map<PageDTO, Page>(page);
            _dataContext.Pages.Add(dbPage);
            await _dataContext.SaveChangesAsync();
            await AddPageContents(dbPage.Id, page.Contents);
            return dbPage.Id;
        }

        public async Task<int> UpdatePage(PageDTO page)
        {
            var existing = await _dataContext.Contents.FirstOrDefaultAsync(x => x.Id == page.Id);
            if (existing == null)
            {
                return await AddPage(page);
            }
            Mapper.Map(page, existing);
            _dataContext.Entry(page).State = EntityState.Modified;
            _dataContext.PageContents.RemoveRange(_dataContext.PageContents.Where(x => x.ParentPageId == page.Id));
            await AddPageContents(existing.Id, page.Contents);
            await _dataContext.SaveChangesAsync();
            return page.Id;
        }

        public async Task<bool> UpdatePageContents(int pageId, List<IncludedItemDTO> contents)
        {
            if (!_dataContext.Pages.Any(p => p.Id == pageId)) return false;
            _dataContext.PageContents.RemoveRange(_dataContext.PageContents.Where(x => x.ParentPageId == pageId));
            await AddPageContents(pageId, contents);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<Page> GetPage(int id)
        {
            return await _dataContext.Pages.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PageDTO> GetPageAll(int id, bool includeEditMarks = false, string language = null)
        {
            var page = await _dataContext.Pages.Include(x => x.Template).FirstOrDefaultAsync(x => x.Id == id);
            return await GetFullPage(page, includeEditMarks, language);
        }

        public async Task<PageDTO> GetPageByUrl(string url, bool includeEditMarks = false, string language = null)
        {
            var page = await _dataContext.Pages.Include(x => x.Template).FirstOrDefaultAsync(x => x.Url == url);
            return await GetFullPage(page, includeEditMarks, language);
        }

        public async Task<int> AddOrUpdateMenuItem(PageMenuItem menuItem)
        {
            _dataContext.Entry(menuItem).State = menuItem.Id == 0 ?
                         EntityState.Added :
                         EntityState.Modified;

            await _dataContext.SaveChangesAsync();
            return menuItem.Id;
        }

        public async Task<PageMenuItem> GetMenuItem(int id)
        {
            return await _dataContext.PageMenuItems.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<MenuItemDTO>> GetMenuItems(int parentId, bool includeChildren = true)
        {
            List<PageMenuItem> items = null;
            if (parentId < 1)
            {
                items = await _dataContext.PageMenuItems.Where(x => x.ParentMenuId == null).ToListAsync();
            }
            else
            {
                items = await _dataContext.PageMenuItems.Where(x => x.ParentMenuId == parentId).ToListAsync();
            }
            var dtoItems = Mapper.Map<List<PageMenuItem>,List<MenuItemDTO>>(items);
            if (includeChildren)
            foreach (var item in dtoItems)
            {
                item.Children = await GetMenuItems(item.Id);
            }
            return dtoItems;
        }

        private async Task<PageDTO> GetFullPage(Page page, bool includeEditMarks, string language)
        {
            if (page == null) return null;
            var dtoPage = Mapper.Map<Page, PageDTO>(page);
            dtoPage.Html = await GetPageContent(page.Template.Html, page.Id, language, includeEditMarks);
            return dtoPage;
        }

        private async Task<string> GetPageContent(string htmlIn, int pageId, string language, bool includeEditMarks)
        {
            var items = await _dataContext.PageContents.Include(x => x.Content).Include(x => x.Content.Template).Where(x => x.ParentPageId == pageId).ToListAsync();

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlIn);
            var inclusions = htmlDoc.DocumentNode.SelectNodes("//div[@" + INCLUSION + "]");
            int iSeq = -1;
            foreach (var inclusion in inclusions)
            {
                inclusion.Attributes.Remove(INCLUSION);
                iSeq++;
                if (iSeq >= items.Count)
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
                    inclusion.Attributes.Add(CONTENT_ID, item.ContentId.ToString());
                    inclusion.Attributes.Add(CONTENT_NAME, item.Content.Name);
                }

                if (item.Content.Template.Html.Contains(INCLUSION))
                {
                    inclusion.InnerHtml = await _contentData.GetFullContent(item.Content.Template.Html, item.ContentId, language, includeEditMarks);
                }
                else if (string.IsNullOrEmpty(item.Content.Template.Html))
                {
                    inclusion.InnerHtml = GetEmptyContent(iSeq, includeEditMarks);
                }
                else
                {
                    inclusion.InnerHtml = item.Content.Template.Html;
                }
            }
            return htmlDoc.DocumentNode.OuterHtml;

        }

        private async Task AddPageContents(int parentId, List<IncludedItemDTO> items)
        {
            foreach (var item in items)
            {
                var dbItem = new PageContent();
                dbItem.ParentPageId = parentId;
                dbItem.ContentId = item.Id;
                dbItem.Seq = item.Seq;
                _dataContext.PageContents.Add(dbItem);
            }
            await _dataContext.SaveChangesAsync();
        }

    }
}