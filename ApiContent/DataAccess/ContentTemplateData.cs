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
    public class ContentTemplateData : IContentTemplateData
    {
        private DataContext _dataContext;

        public ContentTemplateData()
        {
            _dataContext = new DataContext();
        }

        public async Task<int> AddOrUpdateContentTemplate(ContentTemplate contentTemplate)
        {
            _dataContext.Entry(contentTemplate).State = contentTemplate.Id == 0 ?
                        EntityState.Added :
                        EntityState.Modified;

            await _dataContext.SaveChangesAsync();
            return contentTemplate.Id;
        }

        public async Task<ContentTemplate> GetContentTemplate(int id)
        {
            return await _dataContext.ContentTemplates.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<ContentTemplate>> GetContentTemplates(string filter)
        {
            ContentTemplateFilter uFilter = new ContentTemplateFilter(filter);
            IQueryable<ContentTemplate> templates = _dataContext.ContentTemplates;
            if (!string.IsNullOrWhiteSpace(uFilter.Name)) templates = templates.Where(a => a.Name.Contains(uFilter.Name));
            return await templates.ToListAsync();
        }
        
    }
}