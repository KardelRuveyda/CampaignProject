using Campaign.DomainLayer.Models.Campaing;
using Campaign.DomainLayer.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaign.DomainLayer.Models.ProducObject
{
    public class ProductDTO : Entity
    {
        public ProductDTO(string ProductCode, int stock, double price)
        {
            id = new Guid();
            Stock = new StockObject(stock);
            RealPrice = new PriceObject(price);
            SetPrice(price);
        }

        public ProductCodeObject ProductCode { get; set; }
        public PriceObject RealPrice { get; private set; }

        public PriceObject Price { get; set; }
        public CampaignDTO Campaign { get; set; }
        public StockObject Stock { get; set; }
        public void SetPrice(double price)
        {
            Price = new PriceObject(price);
        }
        public CampaignDTO GetCampaign()
        {
            return Campaign;
        }

        public bool HasCampaign()
        {
            var result = Campaign != null && Campaign.Status;
            return result;
        }

        public void MakeDiscount(double price)
        {
            if (HasCampaign())
            {
                Price.SetPrice(Price.Value + price);
                if (Campaign.IsPriceLimitExceed())
                {
                    Price.SetPrice(RealPrice.Value);
                    Campaign.CloseCampaign();
                }
            }
        }

    }
}
