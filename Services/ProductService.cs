using System.Collections.Generic;
using System.Linq;
using EstoqueAPI.Models;

namespace EstoqueAPI.Services
{
    public static class ProductService
    {
        static List<Product> Products { get; }
        static int nextId = 2;

        static ProductService()
        {
            Products = new List<Product>
            {
                new Product { Id = 1, Name = "Camera", Sku = "Cam" , Quantity = 9 , Price = 10.80m},
                new Product { Id = 2, Name = "Relogio", Sku = "relog" , Quantity = 5 , Price = 7.50m}
            };
        }

        public static List<Product> GetAll()
        {
            return Products;
        }
        public static Product Get(int id)
        {
            var product = Products.Find(product => product.Id == id); 
            return product;
            // foreach (Product product in Products)
            // {
            //     if (product.Id == id)
            //     {
            //         return product;
            //     }
            // }
            
        }

        public static void Add(Product product)
        {
            nextId += 1;
            product.Id = nextId;
            Products.Add(product);
        }

        public static void Update(Product product)
        {
            var oldProduct = Get(product.Id);
            if(product.Price == 0){
                oldProduct.Quantity = product.Quantity;
            }
            else if(product.Quantity == 0){
                oldProduct.Price = product.Price;
            }
            else{
                oldProduct.Sku = product.Sku;
                oldProduct.Name = product.Name;
                oldProduct.Quantity = product.Quantity;
                oldProduct.Price = product.Price;
            }
            
            // foreach (Product prod in Products)
            // {
            //     if (prod.Id == product.Id)
            //     {
            //         prod.Sku = product.Sku;
            //         prod.Name = product.Name;
            //         prod.Quantity = product.Quantity;
            //         prod.Price = product.Price;
            //     }
            // }
        }

        public static void Delete(int id)
        {
            var product = Get(id);
            if (product is null)
            {
                return;
            }

            Products.Remove(product);
        }
    }
}