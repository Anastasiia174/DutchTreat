using DutchTreat.Data.Entities;
using System.Collections.Generic;

namespace DutchTreat.Data
{
    public interface IDutchRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        bool SaveAll();
        IEnumerable<Order> GetAllOdrers(bool includeItems);
        Order GetOdrerById(string userName, int id);
        void AddEntity(object model);
        IEnumerable<Order> GetAllOdrersByUser(string username, bool includeItems);
    }
}