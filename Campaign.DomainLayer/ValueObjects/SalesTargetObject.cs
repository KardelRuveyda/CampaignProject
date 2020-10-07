using System;
using System.Collections.Generic;
using System.Text;

namespace Campaign.DomainLayer.ValueObjects
{
    public class SalesTargetObject : ValueObjectBase
    {
        public int Value { get; set; }

        public SalesTargetObject(int count)
        {
            SetTargetClass(count);
        }

        public void SetTargetClass(int count)
        {
            if (count < 0)
            {
                Logger.Log("Target sales count value must be greather than zero");
            }
            else
            {
                Value = count;
            }
        }

        public override IEnumerable<object> GetEqualComponents()
        {
            yield return Value;
        }
    }
}
