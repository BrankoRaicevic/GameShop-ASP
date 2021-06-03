using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetConsoles();
        IEnumerable<Product> GetGames();
        IEnumerable<Product> GetPeriferals();

        public Product GetProduct(int id);

        public Manufacturer GetManufacturer(int id);

        public Publisher GetPublisher(int? id);

        public int AddUser(User user);

        public bool CheckEmail(string email);

        public bool Login(string email, string password);

        public IEnumerable<Manufacturer> GetManufacturers();
        public IEnumerable<Publisher> GetPublishers();
        public IEnumerable<Category> GetCategories();

        public int AddProduct(Product product);

        public int AddManufacturer(Manufacturer manufacturer);
        public int AddPublisher(Publisher publisher);

        public bool CheckCompany(string Company, int IsSelected);
    }
}
