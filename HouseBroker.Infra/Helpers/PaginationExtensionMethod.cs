using HouseBroker.Infra.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBroker.Infra.Helpers
{
    public static class PaginationExtensionMethod
    {
        public static async Task<PagedObject<T>> PageAsync<T>(this IQueryable<T> query, int pageSize, int pageNumber)
        {
            pageSize = (pageSize > 0) ? pageSize : 15;
            pageNumber = (pageNumber > 0) ? pageNumber : 1;
            var contents = await query.Skip(pageSize * (pageNumber - 1))
                     .Take(pageSize)
                     .ToListAsync();
            return new PagedObject<T>(contents, (int)Math.Ceiling(query.Count() / (float)pageSize), pageNumber, query.Count());
        }

        public static PagedObject<T> Page<T>(this IEnumerable<T> query, int pageSize, int pageNumber)
        {
            pageSize = (pageSize > 0) ? pageSize : 15;
            pageNumber = (pageNumber > 0) ? pageNumber : 1;
            var contents = query.Skip(pageSize * (pageNumber - 1))
                     .Take(pageSize);
            return new PagedObject<T>(contents.ToList(), (int)Math.Ceiling(query.Count() / (float)pageSize), pageNumber, query.Count());
        }
        public static PagedObject<T> PageList<T>(this List<T> query, int pageSize, int pageNumber)
        {
            pageSize = (pageSize > 0) ? pageSize : 15;
            pageNumber = (pageNumber > 0) ? pageNumber : 1;
            var contents = query.Skip(pageSize * (pageNumber - 1))
                     .Take(pageSize);
            return new PagedObject<T>(contents.ToList(), (int)Math.Ceiling(query.Count() / (float)pageSize), pageNumber, query.Count());
        }
    }
}
