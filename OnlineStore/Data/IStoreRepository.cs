using System.Collections.Generic;
using OnlineStore.Data.Entities;

namespace OnlineStore.Data
{
    public interface IStoreRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        
        IEnumerable<Order> GetAllOrders( bool includeItems);
        IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems);
        Order GetOrderById(string username, int id);
        void AddOrder(Order newOrder);

        bool SaveAll();
        void AddEntity(object model);
    }
}