using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBroker.Infra.Dtos
{
    public class PagedObject<T>
    {


        public PagedObject(List<T> contents, int totalPages, int currentPage, int totalRecords)
        {
            Contents = contents;
            this.TotalPages = totalPages;
            this.CurrentPage = currentPage;
            this.TotalRecords = totalRecords;
        }
        public List<T> Contents { get; }
        public int TotalPages { get; }
        public int CurrentPage { get; }
        public int TotalRecords { get; }
    }
}
