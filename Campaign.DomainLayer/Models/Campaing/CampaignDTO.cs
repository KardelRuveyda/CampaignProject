using Campaign.DomainLayer.Models.ProducObject;
using Campaign.DomainLayer.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaign.DomainLayer.Models.Campaing
{
    public class CampaignDTO : Entity
    {
        public CampaignDTO(string name, ProductDTO product, int limit, int targetSalesCount, int duration)
        {
            id = new Guid();
            Product = product;
            Name = new NameObject(name);
            Duration = new DurationObject(duration);
            Limit = new PriceLimitObject(limit);
            Count = new SalesTargetObject(targetSalesCount);
            Status = true;
        }
        public NameObject Name { get; private set; }
        public ProductDTO Product { get; private set; }
        public DurationObject Duration { get; private set; }
        public PriceLimitObject Limit { get; private set; }
        public SalesTargetObject Count { get; private set; }
        public bool Status { get; private set; }
        public int TotalSalesCount { get; private set; }

        public void CloseCampaign()
        {
            Status = false;
        }
        public bool HasTargetSalesCountExceed(int quantity)
        {
            var result = TotalSalesCount + quantity > Count.Value;
            return result;
        }
        public bool IsPriceLimitExceed()
        {
            var maxPriceLimitValue = Product.RealPrice.Value * (100 - Limit.Value) / 100;

            return Product.Price.Value < maxPriceLimitValue;
        }

        public bool HasDuration(TimeSpan localTime)
        {
            var result = localTime.Hours < Duration.Value;
            return result;
        }

        public string GetStatusString()
        {
            var result = Status ? "Active" : "Passive";
            return result;
        }

        public void IncraseTotalSalesCount(int quantity)
        {
            TotalSalesCount += quantity;
        }
    }
}
