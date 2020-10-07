using Campaign.DomainLayer.IServices;
using Campaign.DomainLayer.Models.Campaing;
using Campaign.DomainLayer.Models.ProducObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Campaign.DomainLayer.Services
{
    public class CampaignService : ICampaignService
    {
        private List<CampaignDTO> CampaignList { get; set; }

        private readonly IOrderService _orderService;
        public CampaignService(IOrderService orderService)
        {
            if (CampaignList == null)
            {
                CampaignList = new List<CampaignDTO>();
                this._orderService = orderService;
            }
        }

        private CampaignDTO GetCampaign(Func<CampaignDTO, bool> predicate)
        {
            var result = CampaignList.FirstOrDefault(predicate);
            return result;
        }


        public CampaignDTO GetCampaignName(string name)
        {
            var result = GetCampaign(x => x.Name.Value == name);
            return result;
        }

        public CampaignDTO GetCampaignProductCode(string productCode)
        {
            var result = GetCampaign(x => x.Product.ProductCode.Value == productCode);
            return result;
        }

        public CampaignDTO GetCampaingInfo(string name)
        {
            var campaign = GetCampaignName(name);

            if (campaign != null)
            {
                int salesTotal = _orderService.GetSalesCampaingTotal(campaign.Name.Value);
                double avarageItemPrice = _orderService.GetItemPriceCampaignAvarage(campaign.Name.Value);

                Logger.Log(String.Format("Campaign Info: {0}, " +
                    "Status: {1}, Target Sales: {2}, Total Sales : {3}", campaign.Name.Value, campaign.GetStatusString(), campaign.Count.Value, salesTotal));

                return campaign;

            }
            else
            {
                Logger.Log("Compaign name is not found. ERROR");
                return null;
            }
        }


        public void InsertCampaign(string campaignName, ProductDTO product, int priceLimit, int salesTargetCount, int duration)
        {
            if (GetCampaignName(campaignName) == null)
            {
                if (!product.HasCampaign() && product.Stock.HasStockCapacity(salesTargetCount))
                {

                    var campaign = new CampaignDTO(campaignName, product, duration, priceLimit, salesTargetCount);

                    product.SetCampaign(campaign);

                    CampaignList.Add(campaign);

                    Logger.Log(String.Format("Campaign created; Name: {0}, Product: {1}, Duration: {2}, Limit {3}, Target Sales Count {4}", campaign.Name.Value, product.ProductCode.Value, product.ProductCode.Value, campaign.Duration.Value, campaign.Limit.Value, campaign.Count.Value));
                }
            }
            else
            {
                throw new LogicException("Campaign name already exists");
            }
        }
    }
}
