using EshopBackend.Shared.Dtos.ProductComment;
using EshopBackend.Shared.Entities.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Shared.Interfaces
{
    public interface IProductCommentService
    {
        Task<List<ProductComment>> GetProductComments(long id);

        Task<bool> AddProductComment(ProductCommentInputDto productCommentInputDto, long userId);
    }
}
