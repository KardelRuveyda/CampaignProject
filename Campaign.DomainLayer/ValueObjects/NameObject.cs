using System;
using System.Collections.Generic;
using System.Text;

namespace Campaign.DomainLayer.ValueObjects
{
    public class NameObject : ValueObjectBase
    {
        public string Value { get; set; }

        public NameObject(string Name)
        {
            if (string.IsNullOrEmpty(Name))
            {
                Logger.Log("Name mustn't be empty");
            }

            Value = Name;
        }
        public override IEnumerable<object> GetEqualComponents()
        {
            yield return Value;
        }
    }
}
