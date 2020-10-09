using Campaign.AppLayer;
using Campaign.DomainLayer.IServices;
using Campaign.DomainLayer.Services;
using FluentAssertions;
using System;
using System.Linq;
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

        public void CreateProductTest()
        {
            _command = GetManager();
            _command.Execute("create_product", new string[] { "P11", "100", "1000" });
            var product = _productService.GetProduct("P11");
            //product price should be 100
            product.Price.Value.Should().Be(100);
            // product stock should be 1000
            product.Stock.Value.Should().Be(1000);

        }

        [Fact]
        public void GetProductInfoTest()
        {
            _command = GetManager();
            _command.Execute("create_product", new string[] { "P11", "100", "1000" });
            _command.Execute("get_product_info", new string[] { "P11" });
            var product = _productService.GetProduct("P11");
            //Product price should be 100
            product.Price.Value.Should().Be(100);
            // Product stock should be 1000 
            product.Stock.Value.Should().Be(1000);
        }

        [Fact]
        public void CreateOrderTest()
        {
            _command = GetManager();
            _command.Execute("create_product", new string[] { "P11", "100", "1000" });
            _command.Execute("create_order", new string[] { "P11", "4" });
            var order = _orderService.GetOrders().FirstOrDefault();
            //For Order
            //Quantity should be 4
            order.Quantity.Value.Should().Be(4);
            //For Product
            //Product code should be OP1
            order.Product.ProductCode.Value.Should().Be("P11");
            //Product Code should be 100
            order.Product.Price.Value.Should().Be(100);
            // Order Count : 4 ; Stock should be: 996
            order.Product.Stock.Value.Should().Be(996);
        }

        [Fact]
        public void CreateCampaignTest()
        {
            _command = GetManager();
            _command.Execute("create_product", new string[] { "P11", "100", "1000" });
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
        [Fact]
        public void GetCampaignInfoTest()
        {
            _command = GetManager();
            _command.Execute("create_product", new string[] { "P11", "100", "1000" });
            _command.Execute("create_campaign", new string[] { "C11", "P11", "10", "20", "100" });
            _command.Execute("get_campaign_info", new string[] { "C11", "P11", "10", "20", "100" });
            var campaign = _campaignService.GetCampaingInfo("C11");
            //For Campaign
            //Campaign Name should Be C11;
            campaign.Name.Value.Should().Be("C11");
            //For Products
            //Product Code should be P11
            campaign.Product.ProductCode.Value.Should().Be("P11");
            //Product Duration  should be P11
            campaign.Duration.Value.Should().Be(10);
            //Product Limit  should be 20
            campaign.Limit.Value.Should().Be(20);
            //Product Count should be 100
            campaign.Count.Value.Should().Be(100);
        }

        [Fact]
        public void IncreaseTimeTest()
        {
            _command = GetManager();
            var DatetimeNow = _command.GetTime();
            DatetimeNow.Should().Be(new TimeSpan(0, 0, 0));
            _command.Execute("increase_time", new string[] { "1" });
            DatetimeNow = _command.GetTime();
            DatetimeNow.Should().Be(new TimeSpan(1, 0, 0));
        }


    }
}
