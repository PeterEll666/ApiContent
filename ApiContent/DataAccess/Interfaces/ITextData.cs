using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiContent.Models;
using ApiContent.Models.DTOs;

namespace ApiContent.DataAccess
{
    interface ITextData
    {
        Task<int> AddOrUpdateText(TextDTO contentText);
        Task<TextDTO> GetText(int id, string language);
        Task<List<TextLang>> GetTexts(List<int> items , string language);

        Task AddTextLang(TextLang contentTextLang);

        Task UpdateTextLang(TextLang contentTextLang);

    }
}
