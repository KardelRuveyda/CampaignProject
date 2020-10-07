using Campaign.DomainLayer.Models.Campaing;
using Campaign.DomainLayer.Models.ProducObject;
using Campaign.DomainLayer.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaign.DomainLayer.Models.Order
{
    public class OrderDTO:Entity
    {
        public OrderDTO(ProductDTO product, int quantity)
        {
            id = new Guid();
            Product = product;
            Quantity = new QuantityObject(quantity);
        }
        public CampaignDTO Campaign { get; set; }
        public QuantityObject Quantity { get; set; }
        public PriceObject PriceSales { get; set; }
        public ProductDTO Product { get; set; }

        public void SetCampaign(CampaignDTO campaign)
        {
            Campaign = campaign;
        }
        public void SetPriceSales(double price)
        {
            PriceSales = new PriceObject(price);
        }


    }
}
