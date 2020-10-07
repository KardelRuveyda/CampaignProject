using System;
using System.Collections.Generic;
using System.Text;

namespace Campaign.DomainLayer.ValueObjects
{
    public class QuantityObject : ValueObjectBase
    {
        public int Value { get; set; }
        
        public QuantityObject(int quantity)
        {
            Increase(quantity);
        }

        public void Increase(int quantity)
        {
            if (quantity < 1)
            {
                Logger.Log("Quantity should be greater or equal to zero");
            }
            else
            {
                Value += quantity;
            };
        }

        public override IEnumerable<object> GetEqualComponents()
        {
            yield return Value;
        }
    }
}
