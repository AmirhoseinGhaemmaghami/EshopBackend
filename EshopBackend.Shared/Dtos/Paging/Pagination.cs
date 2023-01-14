using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Shared.Dtos.Paging
{
    public class Pagination<T>
    {
        public int PageId { get; set; }

        public int PageSize { get; set; }

        public int Count { get; set; }

        public IEnumerable<T> Data { get; set; }
    }
}

