using EshopBackend.Shared.Dtos.Order;
using EshopBackend.Shared.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Shared.Interfaces
{
    public interface IOrderService
    {
        //create a new order
        Task<Order> CreateOrder(long userId);

        //Add a product to order
        Task<Order> AddProductToOrder(long userId, long ProductId, int OrderQty);

        //get it when refresh
        Task<Order> GetOrder(long userId);

        Task<bool> DeleteBasket(long userId);

        Task<Order> DeleteBasketItem(long userId, long productId);
    }
}
