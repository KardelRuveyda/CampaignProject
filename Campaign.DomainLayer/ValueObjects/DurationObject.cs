﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Campaign.DomainLayer.ValueObjects
{
    public class DurationObject : ValueObjectBase
    {
        public int Value { get; set; }

        public DurationObject(int hour)
        {
            Increase(hour);
        }

        public void Increase(int hour)
        {
            if (hour < 0)
            {
                Logger.Log("Duration value is bigger than zero. ");
            }
            else
            {
                Value += hour;
            }
        }

        public override IEnumerable<object> GetEqualComponents()
        {
            yield return Value;
        }
    }
}
