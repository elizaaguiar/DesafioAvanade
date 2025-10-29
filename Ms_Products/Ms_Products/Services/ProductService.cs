using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ms_Products.DTOs;
using Ms_Products.Entities;
using Ms_Products.Interfaces;

namespace Ms_Products.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public Product CreateProduct(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            _productRepository.CreateProduct(product);
            return product;
        }
        public async Task<Product> GetByGuid(Guid Guid)
        {
            var products = await _productRepository.GetByGuid(Guid);
            return products;
        }

    }
}
