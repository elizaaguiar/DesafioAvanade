using Ms_Products.Entities;

namespace Ms_Products.Interfaces
{
    public interface IProductRepository
    {
        void CreateProduct(Product product);
        Product GetByGuid(Guid ProductId);
        Task<Product> StockUpdate(Guid ProductId, int Quantity);
    }
}