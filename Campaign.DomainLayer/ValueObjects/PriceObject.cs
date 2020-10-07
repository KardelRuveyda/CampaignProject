using System;
using System.Collections.Generic;
using System.Text;

namespace Campaign.DomainLayer.ValueObjects
{
    public class PriceObject : ValueObjectBase
    {
        public double Value { get; set; }

        public PriceObject(double price)
        {
            SetPrice(price);
        }

        public void SetPrice(double price)
        {
            if (price < 0)
            {
                Logger.Log("Price value must be equal or greater to zero");
            }else
            {
                Value = price;
            }
        }

        public override IEnumerable<object> GetEqualComponents()
        {
            yield return Value;
        }
    }
}
