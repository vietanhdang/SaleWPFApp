using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderRepository : IOrderRepository
    {
        /// <summary>
        /// Gets a list of all orders.
        /// </summary>
        /// <returns>A list of all orders.</returns>
        public List<Order> GetAllOrders()
        {
            using (var context = new Sale_ManagementContext())
            {
                return context.Orders.ToList();
            }
        }

        /// <summary>
        /// Creates a new order.
        /// </summary>
        /// <param name="order">The order to create.</param>
        /// <returns>The created order.</returns>
        public Order CreateOrder(Order order)
        {
            try
            {
                using (var context = new Sale_ManagementContext())
                {
                    context.Orders.Add(order);
                    context.SaveChanges();
                    return order;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets an order by ID.
        /// </summary>
        /// <param name="id">The ID of the order.</param>
        /// <returns>The order with the given ID.</returns>
        public Order GetOrderById(int id)
        {
            using (var context = new Sale_ManagementContext())
            {
                return context.Orders.Find(id);
            }
        }

        /// <summary>
        /// Updates an order.
        /// </summary>
        /// <param name="order">The order to update.</param>
        /// <returns>The updated order.</returns>
        public Order UpdateOrder(Order order)
        {
            try
            {
                using (var context = new Sale_ManagementContext())
                {
                    context.Orders.Update(order);
                    context.SaveChanges();
                    return order;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get orders of a user by email.
        /// </summary>
        /// <param name="id">The email of the user.</param>
        /// <returns>A list of orders of the user.</returns>
        public List<Order> GetOrdersOfUser(int id)
        {
            using (var context = new Sale_ManagementContext())
            {
                return context.Orders.Where(o => o.MemberId == id).ToList();
            }
        }

        /// <summary>
        /// Get orders of a user by email and date.
        /// </summary>
        /// <param name="start">The start date.</param>
        /// <param name="end">The end date.</param>
        /// <returns>A list of orders of the user.</returns>
        public List<Order> GetOrdersByDateRange(DateTime start, DateTime end)
        {
            using (var context = new Sale_ManagementContext())
            {
                return context.Orders
                    .Where(o => o.OrderDate >= start && o.OrderDate <= end)
                    .ToList();
            }
        }

        /// <summary>
        /// Deletes an order.
        /// </summary>
        /// <param name="id">The ID of the order to delete.</param>
        public void DeleteOrder(int id)
        {
            try
            {
                using (var context = new Sale_ManagementContext())
                {
                    var order = context.Orders.FirstOrDefault(o => o.OrderId == id);
                    if (order == null)
                    {
                        throw new Exception("Order not found");
                    }
                    var orderDetails = context.OrderDetails.Where(od => od.OrderId == id).ToList();
                    context.OrderDetails.RemoveRange(orderDetails);
                    context.Orders.Remove(order);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get order details of an order.
        /// </summary>
        public List<OrderDetail> GetOrderDetails(int id)
        {
            using (var context = new Sale_ManagementContext())
            {
                return context.OrderDetails
                    .Include(od => od.Product)
                    .Where(od => od.OrderId == id)
                    .ToList();
            }
        }
    }
}
