using Microsoft.EntityFrameworkCore;
using ProductCommon.Interface;
using ProductCommon.Models;
using ProductsDataAccess;
using System.Linq;

namespace ProductsService
{
    public class ProductService : IProductService
    {
        private readonly ShopContext _context;

        public ProductService(ShopContext context)
        {
            _context = context;

            _context.Database.EnsureCreated();
        }

        public async Task<IEnumerable<Product>> GetAllProducts(QuerryPrameters qparams)
        {
            return await _context.Products
                .Skip((qparams.PageNumber - 1) * qparams.PageSize)
                .Take(qparams.PageSize)
                .ToArrayAsync();
        }

        public async Task<Product?> GetProduct(int id)
        {
            return await _context.Products.FindAsync(id);

        }

        public async Task<IEnumerable<Product>> GetAvailableProducts()
        {
            return await _context.Products.Where(p => p.IsAvailable).ToArrayAsync();
        }

        public async Task<Product> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<(bool result, Product product)> PutProduct(int id, Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return (true, product);
            }
            catch (DbUpdateConcurrencyException)
            {
                var exprod = _context.Products.Any(p => p.Id == id);
                if (!exprod)
                {
                    return (false, product);
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<Product?> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return null;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<IEnumerable<Product>> DeleteMultiple(int[] ids)
        {
            var products = new List<Product>();
            foreach (var id in ids)
            {
                var product = await _context.Products.FindAsync(id);

                if (product == null)
                {
                    return new List<Product>();
                }

                products.Add(product);
            }

            _context.Products.RemoveRange(products);
            await _context.SaveChangesAsync();

            return products;
        }
    }
}
