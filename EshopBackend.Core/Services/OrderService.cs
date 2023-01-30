using EshopBackend.Shared.Dtos.Order;
using EshopBackend.Shared.Entities.Account;
using EshopBackend.Shared.Entities.Order;
using EshopBackend.Shared.Entities.Store;
using EshopBackend.Shared.Interfaces;
using Ganss.Xss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IProductService productService;

        public OrderService(IUnitOfWork unitOfWork, IProductService productService)
        {
            this.unitOfWork = unitOfWork;
            this.productService = productService;
        }

        public async Task<Order> AddProductToOrder(long userId, long ProductId, int OrderQty)
        {
            try
            {
                var user = await this.unitOfWork.Repository<User>().GetByIdAsync(userId);
                if (user == null)
                    return null;

                var order = (await this.unitOfWork.Repository<Order>().GetAllWithSpecAsync(o =>
                !o.IsPaid && !o.Deleted && o.UserId == userId, Includes: o => o.OrderLines,
                OrderByDesc: o => o.LastUpdateDate)).FirstOrDefault();

                if (order == null)
                    order = await this.CreateOrder(userId);

                var product = await this.productService.GetProductById(ProductId);

                var existingOrderLine = order.OrderLines?.FirstOrDefault(o => o.ProductId == product.Id);
                if (existingOrderLine != null)
                {
                    existingOrderLine.OrderQty += OrderQty;
                }
                else
                {
                    var orderLine = new OrderLine()
                    {
                        OrderId = order.Id,
                        OrderQty = OrderQty,
                        Price = product.Price,
                        ProductId = ProductId,
                    };
                    await this.unitOfWork.Repository<OrderLine>().AddAsync(orderLine);
                }

                await this.unitOfWork.Complete();

                await IncludeProducts(order);
                return removedOrderLines(order);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private Order removedOrderLines(Order order)
        {
            order.OrderLines = order.OrderLines.Where(od => !od.Deleted).ToList();
            return order;
        }

        public async Task<Order> CreateOrder(long userId)
        {
            var user = await this.unitOfWork.Repository<User>().GetByIdAsync(userId);
            if (user == null)
                return null;

            var order = await this.unitOfWork.Repository<Order>().AddAsync(new Order() { UserId = user.Id });
            await unitOfWork.Complete();

            return order;
        }

        public async Task<bool> DeleteBasket(long userId)
        {
            var order = (await this.unitOfWork.Repository<Order>().GetAllWithSpecAsync(o => o.UserId == userId &&
            !o.IsPaid, OrderByDesc:o=>o.LastUpdateDate)).FirstOrDefault();

            foreach (var item in order.OrderLines)
            {
                await this.unitOfWork.Repository<OrderLine>().DeleteAsync(item);
            }
            await this.unitOfWork.Repository<Order>().DeleteAsync(order);
            return await this.unitOfWork.Complete() > 0;
        }

        public async Task<Order> DeleteBasketItem(long userId, long productId)
        {
            try
            {
                var order = (await this.unitOfWork.Repository<Order>().GetAllWithSpecAsync(o => o.UserId == userId &&
                !o.IsPaid, Includes: o => o.OrderLines, OrderByDesc: o=> o.LastUpdateDate)).FirstOrDefault();

                if (order == null)
                    return null;

                var orderLine = order.OrderLines.FirstOrDefault(o => o.ProductId == productId
                && !o.Deleted);

                await this.unitOfWork.Repository<OrderLine>().DeleteAsync(orderLine);
                var res = await this.unitOfWork.Complete() > 0;

                if (res && order.OrderLines.Where(ol => !ol.Deleted).Any())
                {
                    await IncludeProducts(order);
                    return removedOrderLines(order);
                }

                else
                {
                    await this.unitOfWork.Repository<Order>().DeleteAsync(order);
                    await this.unitOfWork.Complete();
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Order> GetOrder(long userId)
        {
            var user = await this.unitOfWork.Repository<User>().GetByIdAsync(userId);
            if (user == null)
                return null;

            var order = (await this.unitOfWork.Repository<Order>().GetAllWithSpecAsync(o =>
            !o.IsPaid && !o.Deleted && o.UserId == userId, Includes: o => o.OrderLines, 
            OrderByDesc: o => o.LastUpdateDate)).FirstOrDefault();

            if (order == null)
                return null;

            await IncludeProducts(order);
            return removedOrderLines(order);
        }

        protected async Task<Order> IncludeProducts(Order order)
        {
            var productIds = order.OrderLines.Select(o => o.ProductId);
            var products = await this.unitOfWork.Repository<Product>().GetAllWithSpecAsync(p =>
            productIds.Contains(p.Id));
            return order;
        }
    }
}
