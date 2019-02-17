using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using OnlineStore.Data.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace OnlineStore.Data
{
    public class StoreRepository : IStoreRepository
    {
        private readonly StoreContext _ctx;
        private readonly ILogger<StoreRepository> _logger;

        public StoreRepository(StoreContext ctx, ILogger<StoreRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }

        public void AddOrder(Order newOrder)
        {
            //convert new products to lookup of product
            foreach (var item in newOrder.Items)
            {
                item.Product = _ctx.Products.Find(item.Product.Id);
            }

            AddEntity(newOrder);
        }

        public void AddProduct(Product newProduct)
        {
            AddEntity(newProduct);
        }

        public IEnumerable<Order> GetAllOrders( bool includeItems)
        {   
            if (includeItems)
            {
                return _ctx.Orders
                           .Include(o => o.Items)
                           .ThenInclude(i => i.Product)
                           .ToList();
            }
            else
            {
                return _ctx.Orders
                           .ToList();
            }
        }

        public IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems)
        {
            if (includeItems)
            {
                return _ctx.Orders
                           .Where(o => o.User.UserName == username)
                           .Include(o => o.Items)
                           .ThenInclude(i => i.Product)
                           .ToList();
            }
            else
            {
                return _ctx.Orders
                           .Where(o => o.User.UserName == username)
                           .ToList();
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("GetAllProducts was called");
                var products = _ctx.Products
                           .OrderBy(p => p.Title)
                           .ToList();
                return products;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all products: {ex}");
                return null;
            }
        }

        public Order GetOrderById(string username, int id)
        {
            return _ctx.Orders
                       .Include(o => o.Items)
                       .ThenInclude(i => i.Product)
                       .Where(o => o.Id == id && o.User.UserName == username)
                       .FirstOrDefault();
        }

        public IEnumerable<Product> GetProductsByCategory( string category)
        {
            try
            {
                _logger.LogInformation("GetProductsByCategory was called");

                return _ctx.Products
                        .Where(p => p.Category == category)
                        .ToList();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to get products by category: {ex}");
                return null;
            }
        }

        public bool SaveAll()
        {
            try
            {
                _logger.LogInformation("SaveAll was called");
                return _ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save all products: {ex}");
                return false;
            }
        }
    }
}
