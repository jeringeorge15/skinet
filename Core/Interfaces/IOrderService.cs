using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.OrderAggregate;

namespace Core.Interfaces
{
    public interface IOrderService
    {
         Task<Order> CreateOrderAsync( string buyerEmail, int deliveryMethod, string basketId, Address shippingAddress);
         Task<IReadOnlyList<Order>> GetOrderForUserAsync( string buyerEmail);

         Task<Order> GetOrderById(int id, string buyerEmailId);

         Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
         
    }
}