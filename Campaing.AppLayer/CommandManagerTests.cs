using Campaign.AppLayer;
using Campaign.DomainLayer.IServices;
using Campaign.DomainLayer.Services;
using FluentAssertions;
using System;
using Xunit;


namespace Campaing.AppLayer
{
    public class CommandManagerTests
    {
        private ICampaignService _campaignService;
        private IProductService _productService;
        private IOrderService _orderService;
        private ICommandManager _commandManager;

        private ICommandManager _command;

        private CommandManager GetManager()
        {
            _campaignService = new CampaignService(_orderService);
            _productService = new ProductService();
            _orderService = new OrderService();
 
            return new CommandManager(_productService,_campaignService,_orderService);
        }

        [Fact]
        public void CreateCampaignTest()
        {
            _command = GetManager();

            _command.Execute("create_product", new string[] { "P1", "100", "1000" });
            _command.Execute("create_campaign", new string[] { "C11", "P11", "10", "20", "100" });
            var campaign = _campaignService.GetCampaingInfo("C11");

            //Product code should be p11;

            campaign.Product.ProductCode.Value.Should().Be("P11");

            //Product Duration should be 10 

            campaign.Duration.Should().Be(10);

            //Product Sales Target Count should be 100

            campaign.Count.Should().Be(100);

            //Product Price Limit should be 20

            campaign.Limit.Should().Be(20);
        }

 


    }
}
