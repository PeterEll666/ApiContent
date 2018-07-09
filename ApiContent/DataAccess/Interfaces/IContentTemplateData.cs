using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiContent.Models;

namespace ApiContent.DataAccess
{
    interface IContentTemplateData
    {
        Task<int> AddOrUpdateContentTemplate(ContentTemplate contentTemplate);
        Task<ContentTemplate> GetContentTemplate(int id);
        Task<List<ContentTemplate>> GetContentTemplates(string filter);

    }
}
