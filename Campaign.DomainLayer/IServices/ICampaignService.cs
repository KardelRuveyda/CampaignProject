using Campaign.DomainLayer.Models.Campaing;
using Campaign.DomainLayer.Models.ProducObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaign.DomainLayer.IServices
{
    public interface ICampaignService
    {
        CampaignDTO GetCampaignName(string name);
        CampaignDTO GetCampaignProductCode(string productCode);
        CampaignDTO GetCampaingInfo(string name);
        void InsertCampaign(string campaignName, ProductDTO product, int priceLimit, int salesTargetCount, int duration);
    }
}
