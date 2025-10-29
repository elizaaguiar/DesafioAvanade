using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ms_Products.DTOs;
using Ms_Products.Entities;
using Ms_Products.Interfaces;
using Ms_Products.Services;

namespace Ms_Products.Controllers
{
    [ApiController]
    [Route("product")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _product;
        public ProductController(ProductService product)
        {
            _product = product;
        }
        [HttpGet("{Guid}")]
        public async Task<IActionResult> GetByGuid(Guid Guid)
        {
            return Ok(await _product.GetByGuid(Guid));
        }
        [HttpPost("register")]
        public IActionResult CreateProduct(ProductDTO productDTO)
        {
            var newProduct = _product.CreateProduct(productDTO);
            var product = new ProductResponseDTO
            {
                Guid = newProduct.Guid,
                Name = newProduct.Name,
                Description = newProduct.Description,
                Price = newProduct.Price,
                CreatedAt = newProduct.CreatedAt
            };
            return Ok(product);
        }
    }
}