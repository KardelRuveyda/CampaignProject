using Campaign.DomainLayer.Models.ProducObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaign.DomainLayer.IServices
{
    public interface IProductService
    {
        ProductDTO GetProduct(string productCode);
        void ChangePriceByProduct(string productCode, double price);
        void  IncreaseTime(int totalIncrase);
        void InsertProduct(string productCode, double price, int stock);
    }
}
