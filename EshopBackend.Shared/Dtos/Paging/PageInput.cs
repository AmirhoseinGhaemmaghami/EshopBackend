using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Shared.Dtos.Paging
{
    public class PageInput
    {
        public int PageId { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
