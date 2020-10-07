using System;
using System.Collections.Generic;
using System.Text;

namespace Campaign.DomainLayer.ValueObjects
{
    public class ProductCodeObject : ValueObjectBase
    {
        public string Value { get; set; }

        public ProductCodeObject(string code)
        {
            SetProductCode(code);
        }

        public void SetProductCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                Logger.Log("Product code must'nt be empty.");
            }
            else
            {
                Value = code;
            }
        }

        public override IEnumerable<object> GetEqualComponents()
        {
            throw new NotImplementedException();
        }
    }
}
