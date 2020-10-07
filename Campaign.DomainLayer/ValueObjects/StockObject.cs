using System;
using System.Collections.Generic;
using System.Text;

namespace Campaign.DomainLayer.ValueObjects
{
    public class StockObject : ValueObjectBase
    {
        public int Value { get; set; }

        public StockObject(int stock)
        {
            SetStock(stock);
        }

        public void SetStock(int stock)
        {
            if (stock < 0)
            {
                Logger.Log("Stock value should be greater or equal to zero");
            }
            else
            {
                Value = stock;
            }
        }

        public bool CheckStock(int value)
        {
            if(Value < value)
            {
                Logger.Log("There is not enough stock");
                return false;
            }else
            {
                return true;
            }
        }

        public bool HasStockCapacity(int quantity)
        {
            bool exist = Value - quantity >= 0;

            if (!exist)
            {
                Logger.Log(String.Format("Product stock is not enough for this quantity capacity. Current Stock Capacity : {0}", Value));
            }

            return exist;
        }

        public void ReduceStock(int value)
        {
            if(CheckStock(value))
            {
                Value -= value;
            }

        }
        public override IEnumerable<object> GetEqualComponents()
        {
            yield return Value;
        }
    }
}
