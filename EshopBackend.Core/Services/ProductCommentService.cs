using EshopBackend.Shared.Dtos.Paging;
using EshopBackend.Shared.Dtos.ProductComment;
using EshopBackend.Shared.Entities.Account;
using EshopBackend.Shared.Entities.Store;
using EshopBackend.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Core.Services
{
    public class ProductCommentService : IProductCommentService
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductCommentService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> AddProductComment(ProductCommentInputDto productCommentInputDto, string email)
        {
            try
            {
                var user = await this.unitOfWork.Repository<User>().GetSingleWithSpecAsync(u => u.Email == email);
                if (user == null)
                    return false;


                await this.unitOfWork.Repository<ProductComment>().AddAsync(
                new ProductComment()
                {
                    Comment = productCommentInputDto.Comment,
                    UserId = user.Id,
                    ProductId = productCommentInputDto.ProductId
                });
                return await this.unitOfWork.Complete() > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<List<ProductComment>> GetProductComments(long id)
        {
            return await this.unitOfWork.Repository<ProductComment>().GetAllWithSpecAsync(
                pc => pc.ProductId == id, OrderByDesc: pc => pc.CreateDate,
                paging: new PageInput() { PageId = 1, PageSize = 10 }, Includes: pc => pc.User);
        }
    }
}
