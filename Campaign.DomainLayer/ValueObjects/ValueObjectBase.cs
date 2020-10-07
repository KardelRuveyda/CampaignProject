using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace Campaign.DomainLayer.ValueObjects
{
   public abstract class ValueObjectBase
    {
        public abstract IEnumerable<object> GetEqualComponents();



        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;

            }

            if(GetType() != obj.GetType())
            {
                throw new ArgumentException(string.Format("Value Objects of different types {0} and {1}", GetType(), obj.GetType()));
            }

            var objectValue = (ValueObjectBase)obj;

            return GetEqualComponents().SequenceEqual(objectValue.GetEqualComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualComponents()
                .Aggregate(1, (current, obj) =>
                {
                    unchecked
                    {
                        return current * 23 + (obj?.GetHashCode() ?? 0);
                    }
                });
        }

        public static bool operator ==(ValueObjectBase a, ValueObjectBase b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ValueObjectBase a, ValueObjectBase b)
        {
            return !(a == b);
        }
    }
}
