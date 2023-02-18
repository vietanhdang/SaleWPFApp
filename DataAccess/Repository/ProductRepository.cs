using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        /// <summary>
        /// Gets a list of all products.
        /// </summary>
        /// <returns>A list of all products.</returns>
        public List<Product> GetAllProducts()
        {
            using (var context = new Sale_ManagementContext())
            {
                return context.Products.ToList();
            }
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="product">The product to create.</param>
        /// <returns>The created product.</returns>
        public Product CreateProduct(Product product)
        {
            try
            {
                using (var context = new Sale_ManagementContext())
                {
                    context.Products.Add(product);
                    context.SaveChanges();
                    return product;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets a product by ID.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>The product with the given ID.</returns>
        public Product GetProductById(int id)
        {
            using (var context = new Sale_ManagementContext())
            {
                return context.Products.Find(id);
            }
        }

        /// <summary>
        /// Updates a product.
        /// </summary>
        /// <param name="product">The product to update.</param>
        /// <returns>The updated product.</returns>
        public Product UpdateProduct(Product product)
        {
            try
            {
                using (var context = new Sale_ManagementContext())
                {
                    context.Products.Update(product);
                    context.SaveChanges();
                    return product;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <param name="product">The product to delete.</param>
        public void DeleteProduct(Product product)
        {
            try
            {
                using (var context = new Sale_ManagementContext())
                {
                    context.Products.Remove(product);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// search product by value and type
        /// </summary>
        /// <param name="value">The value of the product.</param>
        /// <param name="type">The type of the product.</param>
        /// 1: ID
        /// 2: Name
        /// 3: Price
        /// 4: UnitInStock
        /// <returns>The product with the given name and type.</returns>
        public List<Product> SearchProduct(string value, int type)
        {
            try
            {
                using (var context = new Sale_ManagementContext())
                {
                    switch (type)
                    {
                        case 2:
                            return context.Products
                                .Where(p => p.ProductName.ToLower().Contains(value))
                                .ToList();
                        case 3:
                            return context.Products
                                .Where(p => p.UnitPrice == Convert.ToDecimal(value))
                                .ToList();
                        case 4:
                            return context.Products
                                .Where(p => p.UnitsInStock == Convert.ToInt32(value))
                                .ToList();
                        default:
                            return context.Products
                                .Where(p => p.ProductId == Convert.ToInt32(value))
                                .ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
