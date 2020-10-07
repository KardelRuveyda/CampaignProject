using System;
using System.Collections.Generic;
using System.Text;

namespace Campaign.DomainLayer.ValueObjects
{
   public class PriceLimitObject:ValueObjectBase
    {
        public int Value { get; set; }

        public PriceLimitObject(int limit)
        {
            SetPriceLimit(limit);
        }

        public void SetPriceLimit(int limit)
        {
            if (limit < 0)
            {
                Logger.Log("Price  limit must be greather than zero");
            }
            else
            {
                Value = limit;
            }
        }

        public override IEnumerable<object> GetEqualComponents()
        {
            yield return Value;
        }
    }
}
