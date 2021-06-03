using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;


namespace GameShop.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext context;

        public ProductRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return context.Product.OrderByDescending(x => x.ProductId);
        }

        public IEnumerable<Product> GetConsoles()
        {
            return context.Product.Where(x => x.CategoryId == 1).OrderByDescending(x => x.ProductId);
        }

        public IEnumerable<Product> GetGames()
        {
            return context.Product.Where(x => x.CategoryId == 2).OrderByDescending(x => x.ProductId);
        }

        public IEnumerable<Product> GetPeriferals()
        {
            return context.Product.Where(x => x.CategoryId == 3).OrderByDescending(x => x.ProductId);
        }

        public Product GetProduct(int id)
        {
            return context.Product.Find(id);
        }

        public Manufacturer GetManufacturer(int id)
        {
            return context.Manufacturer.Find(id);
        }

        public Publisher GetPublisher(int? id)
        {
            return context.Publisher.Find(id);
        }

        public int AddUser(User user)
        {
            context.User.Add(user);
            return context.SaveChanges();
        }

        public bool CheckEmail(string email)
        {
            var ret = context.User.Where(x => x.Email == email);
            return ret.Count() != 0;
        }

        public bool Login(string email, string password)
        {
            if(context.User.Where(x => x.Email == email).Count() == 1)
            {
                var retUserAll = context.User.Where(x => x.Email == email);
                var retUser = retUserAll.FirstOrDefault();

                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: retUser.Salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));
                if (hashed == retUser.Password)
                {
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<Manufacturer> GetManufacturers()
        {
            return context.Manufacturer.OrderByDescending(x => x.ManufacturerId);
        }

        public IEnumerable<Publisher> GetPublishers()
        {
            return context.Publisher.OrderByDescending(x => x.PublisherId);
        }

        public IEnumerable<Category> GetCategories()
        {
            return context.Category.OrderByDescending(x => x.CategoryId);
        }

        public int AddProduct(Product product)
        {
            context.Product.Add(product);
            return context.SaveChanges();
        }

        public int AddManufacturer(Manufacturer manufacturer)
        {
            context.Manufacturer.Add(manufacturer);
            return context.SaveChanges();
        }

        public int AddPublisher(Publisher publisher)
        {
            context.Publisher.Add(publisher);
            return context.SaveChanges();
        }

        public bool CheckCompany(string Company, int IsSelected)
        {
            if (IsSelected == 1)
            {
                var ret = context.Manufacturer.Where(x => x.ManufacturerName == Company);
                return ret.Count() != 0;
            }
            else
            {
                var ret = context.Publisher.Where(x => x.PublisherName == Company);
                return ret.Count() != 0;
            }
        }
    }
}
