    using FusionAPI_Framework.Models;

    namespace FusionAPI_Framework.Interfaces
    {
        public interface IProduct
        {
            Task<List<Products>> GetAllProducts();
            Task<Products> GetProductById(int id);
            Task AddProduct(Products product);
            Task DeleteProduct(int id);
            Task UpdateProduct(Products product);
        }
    }
