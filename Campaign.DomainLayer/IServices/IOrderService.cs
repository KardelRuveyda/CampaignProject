using Campaign.DomainLayer.Models.Order;
using Campaign.DomainLayer.Models.ProducObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaign.DomainLayer.IServices
{
    public interface IOrderService
    {
        List<OrderDTO> GetOrders();
        List<OrderDTO> GetOrdersForCampaignName(string CampaignName);
        int GetSalesCampaingTotal(string campaignName);
        double GetItemPriceCampaignAvarage(string campaignName);
        void InsertOrder(ProductDTO product, int quantity, TimeSpan sysTime);
    }
}
