using Campaign.DomainLayer;
using Campaign.DomainLayer.IServices;
using System;
using System.Collections.Generic;

namespace Campaign.AppLayer
{
    public class CommandManager : ICommandManager
    {
        private readonly IProductService _productService;
        private readonly ICampaignService _campaingService;
        private readonly IOrderService _orderService;

        private static Dictionary<string, Action<string[]>> CommandList;
        private TimeSpan sysTime;

        public CommandManager(IProductService productService, ICampaignService campaignService, IOrderService orderService)
        {
            this._productService = productService;
            this._campaingService = campaignService;
            this._orderService = orderService;
            sysTime = new TimeSpan(0, 0, 0);
            Init();
        }

        public void Execute(string command, string[] objs)
        {

            if (CommandList.ContainsKey(command))
            {
                CommandList[command].Invoke(objs);
            }
            else
            {
                Logger.Log("Command is not found.");
            }
        }

        public void Init()
        {
            if (CommandList == null)
            {
                CommandList = new Dictionary<string, Action<string[]>>();

                CommandList.Add("create_product", CreateProductCommand);
                CommandList.Add("get_product_info", GetProductInfoCommand);
                CommandList.Add("create_order", CreateOrderCommand);
                CommandList.Add("create_campaign", CreateCampaignCommand);
                CommandList.Add("get_campaign_info", GetCampaignInfoCommand);
                CommandList.Add("increase_time", IncraseTimeCommand);
                CommandList.Add("clean", CleanCommands);
            }
        }

        private void CleanCommands(string[] obj)
        {
            Console.Clear();
        }

        private void IncraseTimeCommand(string[] obj)
        {
            int totalIncrase = GetParameter<int>(obj, 0);

            sysTime = sysTime.Add(new TimeSpan(totalIncrase, 0, 0));

            Logger.Log(string.Format("Time : {0}", sysTime.ToString("hh\\:mm")));

            _productService.IncreaseTime(totalIncrase);
        }

        private void GetCampaignInfoCommand(string[] obj)
        {
            string campaignName = GetParameter<string>(obj, 0);

            _campaingService.GetCampaingInfo(campaignName);
        }

        private void CreateCampaignCommand(string[] obj)
        {
            string campaignName = GetParameter<string>(obj, 0);
            string productCode = GetParameter<string>(obj, 1);
            int duration = GetParameter<int>(obj, 2);
            int priceLimit = GetParameter<int>(obj, 3);
            int salesTarget = GetParameter<int>(obj, 4);

            var product = _productService.GetProduct(productCode);

            _campaingService.InsertCampaign(campaignName, product, duration, priceLimit, salesTarget);
        }

        private void CreateOrderCommand(string[] obj)
        {
            string productCode = GetParameter<string>(obj, 0);
            int quantity = GetParameter<int>(obj, 1);
            var product = _productService.GetProduct(productCode);

            _orderService.InsertOrder(product, quantity, sysTime);
        }

        private void GetProductInfoCommand(string[] obj)
        {
            string productCode = GetParameter<string>(obj, 0);

            var product = _productService.GetProduct(productCode);

            Logger.Log(String.Format("Product {0} info; price {1}, stock {2}",productCode, product.Price.Value, product.Stock.Value));
        }

        private void CreateProductCommand(string[] obj)
        {
            var productCode = GetParameter<string>(obj, 0);
            double price = GetParameter<double>(obj, 1);
            int stock = GetParameter<int>(obj, 2);

            _productService.InsertProduct(productCode, price, stock);
        }


        private T GetParameter<T>(string[] values, int index)
        {
            try
            {
                return (T)Convert.ChangeType(values[index], typeof(T));
            }
            catch (Exception ex)
            {
                Logger.Log("Unexcepted paramater value.");
                return Activator.CreateInstance<T>();
            }
        }

        public TimeSpan GetTime()
        {
            return sysTime;
        }
    }
}
