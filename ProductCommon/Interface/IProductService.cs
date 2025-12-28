using ProductCommon.Models;

namespace ProductCommon.Interface
{
    public interface IProductService
    {
        public Task<IEnumerable<Product>> GetAllProducts(QuerryPrameters parms);

        public Task<Product?> GetProduct(int id);

        public Task<IEnumerable<Product>> GetAvailableProducts();

        public Task<Product> PostProduct(Product product);

        public Task<(bool result, Product product)> PutProduct(int id, Product product);

        public Task<Product?> DeleteProduct(int id);

        public Task<IEnumerable<Product>> DeleteMultiple(int[] ids);
    }
}
