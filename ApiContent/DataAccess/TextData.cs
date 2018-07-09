using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ApiContent.Models;
using ApiContent.Models.DTOs;
using AutoMapper;

namespace ApiContent.DataAccess
{
    public class TextData : ITextData
    {
        private const string DEFAULT_LANG = "DEFAULT";
        private DataContext _dataContext;
        
        public TextData()
        {
            _dataContext = new DataContext();
        }

        public async Task<int> AddOrUpdateText(TextDTO text)
        {
            if (string.IsNullOrEmpty(text.Language)) text.Language = DEFAULT_LANG;
            var contentTextDb = Mapper.Map<TextDTO,Text>(text);
            Text dbText = null;
            if (text.Id != 0)
            {
                dbText = await _dataContext.Texts.FirstOrDefaultAsync(x => x.Id == text.Id);
                if (dbText == null) return -1;
            }
            else
            {
                dbText = new Text();
                dbText.Languages = new List<TextLang>();
                _dataContext.Texts.Add(dbText);
            }
            Mapper.Map(text, dbText);
            TextLang lang = null;
            if (dbText.Id != 0)
            {
                lang = await _dataContext.TextLangs.FirstOrDefaultAsync(x => x.Id == text.Id && x.Language == text.Language);
            }
            if (lang == null)
            { 
                {
                    lang = new TextLang();
                    lang.Language = text.Language;
                    _dataContext.TextLangs.Add(lang);
                }
                lang.Html = text.Html;
            }
            await _dataContext.SaveChangesAsync();
            return dbText.Id;
        }

        public async Task<TextDTO> GetText(int id, string language)
        {
            if (string.IsNullOrEmpty(language)) language = DEFAULT_LANG;
            var dbText = await _dataContext.TextLangs.Include(x => x.Text).FirstOrDefaultAsync(x => x.Id == id && x.Language == language);
            return Mapper.Map<TextLang, TextDTO>(dbText);
        }

        public async Task<List<TextLang>> GetTexts(List<int> ids, string language)
        {
            var nnIds = new List<int>();
            foreach (var i in ids)
            {
                nnIds.Add((int)i);
            }
            if (string.IsNullOrEmpty(language)) language = DEFAULT_LANG;
            var dbTexts = await _dataContext.TextLangs.Include(x =>x.Text).Where(t => nnIds.Contains(t.Id) && t.Language == language).ToListAsync();
            if (!string.IsNullOrEmpty(language))
            {
                var missingTexts = nnIds.Except(dbTexts.Select(d => d.Id));
                dbTexts.AddRange(await _dataContext.TextLangs.Where(t => nnIds.Contains(t.Id) && t.Language == DEFAULT_LANG).ToListAsync());
            }
            return dbTexts;
        }

        public async Task AddTextLang(TextLang textLang)
        {
            _dataContext.TextLangs.Add(textLang);

            await _dataContext.SaveChangesAsync();
 
        }

        public async Task UpdateTextLang(TextLang textLang)
        {
            _dataContext.Entry(textLang).State = EntityState.Modified;

            await _dataContext.SaveChangesAsync();

        }

    }
}