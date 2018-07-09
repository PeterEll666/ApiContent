using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiContent.Models;
using ApiContent.Models.DTOs;

namespace ApiContent.DataAccess
{
    interface IContentData
    {
        Task<int> AddContent(ContentDTO content);
        Task<int> UpdateContent(ContentDTO content);
        Task<Content> GetContent(int id);
        Task<ContentDTO> GetContentAll(int id, bool includeEditMarks, string language = null);
        Task<List<Content>> GetContents(string filter);
        Task<string> GetFullContent(string htmlIn, int contentId, string language, bool includeEditMarks);

    }
}
