using Campaign.DomainLayer.IServices;
using Campaign.DomainLayer.Models.ProducObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Campaign.DomainLayer.Services
{
    public class ProductService : IProductService
    {
        private List<ProductDTO> ProductList { get; set; }

        public ProductService()
        {
            if (ProductList == null)
            {
                ProductList = new List<ProductDTO>();
            }
        }

        public ProductDTO GetProduct(string productCode)
        {
            var product = ProductList.FirstOrDefault(x => x.ProductCode.Value == productCode);
            if (product != null)
            {
                return product;
            }
            else
            {
                Logger.Log("Product not found");
                return null;
            }
        }

        private void GiveDiscount()
        {
            foreach (var item in ProductList)
            {
                item.GiveDiscount(RandomDiscountValue());
            }
        }

        private int RandomDiscountValue()
        {
            var result = new Random().Next(-20, 20);
            return result;
        }
        public void IncreaseTime(int totalIncrase)
        {
            for (int i = 0; i < totalIncrase; i++)
            {
                GiveDiscount();
            }
        }

        public void ChangePriceByProduct(string productCode, double price)
        {
            var product = this.ProductList.FirstOrDefault(s => s.ProductCode.Value == productCode);

            if (product != null)
            {
                product.RealPrice.SetPrice(price);
            }
        }

        public void InsertProduct(string productCode, double price, int stock)
        {
            if(ProductList.Any(x => x.ProductCode.Value == productCode))
            {
                Logger.Log("This product already exists");
            }else
            {
                ProductList.Add(new ProductDTO(productCode, stock, price));
                Logger.Log(String.Format("Product created; code {0}, price {1}, stock {2}", productCode,stock,price));
            }
        }
 
    }
}
