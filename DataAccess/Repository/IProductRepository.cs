using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    /// <summary>
    /// This interface represents the contract to be implemented by ProductRepository.
    /// @Author: ANHDVHE151359
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Gets a list of all products.
        /// </summary>
        /// <returns>A list of all products.</returns>
        List<Product> GetAllProducts();

        /// <summary>
        /// Gets a product by ID.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>The product with the given ID.</returns>
        Product GetProductById(int id);

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="product">The product to create.</param>
        /// <returns>The created product.</returns>
        Product CreateProduct(Product product);

        /// <summary>
        /// Updates a product.
        /// </summary>
        /// <param name="product">The product to update.</param>
        /// <returns>The updated product.</returns>
        Product UpdateProduct(Product product);

        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <param name="product">The product to delete.</param>
        void DeleteProduct(Product product);
        
        /// <summary>
        /// search product by value and type
        /// </summary>
        /// <param name="value">The value of the product.</param>
        /// <param name="type">The type of the product.</param>
        /// <returns>The product with the given name and type.</returns>
        List<Product> SearchProduct(string value, int type);
    }
}
