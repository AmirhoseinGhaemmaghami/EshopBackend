using EshopBackend.Shared.Dtos.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Shared.Dtos.Sorting
{
    public class PagingSortingInput: PageInput
    {
        public string? SortColumn { get; set; }

        public string? SortOrder { get; set; }
    }
}
