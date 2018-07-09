using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiContent.DataAccess
{
    public class ContentDataBase
    {
        protected DataContext _dataContext;
        protected const string INCLUSION = "data-ac-include";
        protected const string EDITOR_CLASS = "acEditor";
        protected const string EMPTY_CONTENT = "Content {0}";

        public ContentDataBase()
        {
            _dataContext = new DataContext();
        }

        protected string GetEmptyContent(int seq, bool includeMarks)
        {
            if (!includeMarks)
            {
                return string.Empty;
            }
            return string.Format(EMPTY_CONTENT, seq + 1);
        }
    }
}