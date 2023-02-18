using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    /// <summary>
    /// This interface represents the contract to be implemented by OrderRepository.
    /// @Author: ANHDVHE151359
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Gets a list of all orders.
        /// </summary>
        /// <returns>A list of all orders.</returns>
        List<Order> GetAllOrders();

        /// <summary>
        /// Gets a order by ID.
        /// </summary>
        /// <param name="id">The ID of the order.</param>
        /// <returns>The order with the given ID.</returns>
        Order GetOrderById(int id);

        /// <summary>
        /// Creates a new order.
        /// </summary>
        /// <param name="order">The order to create.</param>
        /// <returns>The created order.</returns>
        Order CreateOrder(Order order);

        /// <summary>
        /// Updates a order.
        /// </summary>
        /// <param name="order">The order to update.</param>
        /// <returns>The updated order.</returns>
        Order UpdateOrder(Order order);

        /// <summary>
        /// Deletes a order.
        /// </summary>
        /// <param name="id">The ID of the order to delete.</param>
        void DeleteOrder(int id);

        /// <summary>
        /// Gets a list of all orders of an User.
        /// </summary>
        /// <param name="id">The email of the user.</param>
        /// <returns>A list of all orders of a user.</returns>
        List<Order> GetOrdersOfUser(int id);

        /// <summary>
        /// Gets a list of all orders of a user by date.
        /// </summary>
        /// <param name="start">The start date of the order.</param>
        /// <param name="end">The end date of the order.</param>
        /// <returns>A list of all orders of a user by date.</returns>
        List<Order> GetOrdersByDateRange(DateTime start, DateTime end);

        /// <summary>
        /// Gets a list details of an order.
        /// </summary>
        /// <param name="id">The ID of the order.</param>
        /// <returns>A list details of an order.</returns>
        List<OrderDetail> GetOrderDetails(int id);
    }
}
