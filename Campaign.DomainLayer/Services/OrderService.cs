using Campaign.DomainLayer.IServices;
using Campaign.DomainLayer.Models.Order;
using Campaign.DomainLayer.Models.ProducObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Campaign.DomainLayer.Services
{
    public class OrderService:IOrderService
    {
        private List<OrderDTO> OrderList { get; set; }

        public OrderService()
        {
            if(OrderList == null)
            {
                OrderList = new List<OrderDTO>();
            }
        }

        public List<OrderDTO> GetOrders()
        {
            return OrderList;
        }

        public List<OrderDTO> GetOrdersForCampaignName(string CampaignName)
        {
            var result = OrderList.Where(s => s.Campaign?.Name?.Value == CampaignName).ToList();
            return result;
        }

        public int GetSalesCampaingTotal(string campaignName)
        {
            var result = GetOrdersForCampaignName(campaignName)?.Sum(x => x.Quantity.Value) ?? 0;
            return result;
        }

        public double GetItemPriceCampaignAvarage(string campaignName)
        {
            int totalSales = GetSalesCampaingTotal(campaignName);
            double salesPrice = GetOrdersForCampaignName(campaignName)?.Sum(x => x.PriceSales.Value) ?? 0;
            return salesPrice / totalSales;
        }

        public void InsertOrder(ProductDTO product, int quantity, TimeSpan sysTime)
        {
            if (product.Stock.HasStockCapacity(quantity))
            {
                product.Stock.ReduceStock(quantity);

                var order = new OrderDTO(product, quantity);

                if (product.HasCampaign())
                {
                    var existingCampaign = product.GetCampaign();

                    if (existingCampaign.HasDuration(sysTime))
                    {
                        existingCampaign.IncraseTotalSalesCount(quantity);

                        order.SetCampaign(existingCampaign);
                        order.SetPriceSales(product.Price.Value);

                        OrderList.Add(order);
                        Logger.Log(String.Format("Succesfully! Order created. Product Code :{0}, Quantity: {1}", product.ProductCode.Value, quantity));
                    }
                }else
                {
                    order.SetPriceSales(product.Price.Value);
                    OrderList.Add(order);
                    Logger.Log(String.Format("Succesfully! Order created. Order created; product {0}, quantity {1}", product.ProductCode.Value, quantity));
                }
            }
        }
    }
}
