using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using ApiContent.Models;
using ApiContent.Models.DTOs;

namespace ApiContent.DataAccess
{
    interface IPageData
    {
        Task<int> AddPage(PageDTO page);
        Task<int> UpdatePage(PageDTO page);

        Task<bool> UpdatePageContents(int pageId, List<IncludedItemDTO> contents);

        Task<Page> GetPage(int id);

        Task<PageDTO> GetPageAll(int id, bool includeEditMarks = false, string language = null);
        Task<PageDTO> GetPageByUrl(string url, bool includeEditMarks = false, string language = null);

        Task<int> AddOrUpdateMenuItem(PageMenuItem menuItem);
        Task<PageMenuItem> GetMenuItem(int id);
        Task<List<MenuItemDTO>> GetMenuItems(int parentId, bool includeChildren=true);

    }
}
