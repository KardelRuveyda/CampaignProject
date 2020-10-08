using Campaign.AppLayer;
using Campaign.DomainLayer.IServices;
using Campaign.DomainLayer.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Campaign.PresentationLayer
{
    public class DependencyLoader
    {
        public ServiceProvider Init()
        {
            ServiceCollection serviceDescriptors = new ServiceCollection();
            serviceDescriptors.AddSingleton<ICampaignService, CampaignService>();
            serviceDescriptors.AddSingleton<IOrderService, OrderService>();
            serviceDescriptors.AddSingleton<IProductService, ProductService>();
            serviceDescriptors.AddSingleton<ICommandManager, CommandManager>();
            return serviceDescriptors.BuildServiceProvider();

        }
    }
}
