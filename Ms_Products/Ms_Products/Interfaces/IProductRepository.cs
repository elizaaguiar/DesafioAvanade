using Ms_Products.Entities;

namespace Ms_Products.Interfaces
{
    public interface IProductRepository
    {
        void CreateProduct(Product product);
        Task<Product> GetByGuid(Guid Guid);
    }
}