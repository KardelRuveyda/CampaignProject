using System;
using System.Collections.Generic;
using System.Text;

namespace Campaign.DomainLayer.ValueObjects
{
    public class NameObject : ValueObjectBase
    {
        public string Value { get; set; }

        public NameObject(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                Logger.Log("Name mustn't be empty");
            }else
            {
                Value = name;
            }
        }
        public override IEnumerable<object> GetEqualComponents()
        {
            yield return Value;
        }
    }
}
