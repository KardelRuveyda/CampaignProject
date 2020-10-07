using System;
using System.Collections.Generic;
using System.Text;

namespace Campaign.DomainLayer
{
    public abstract class Entity
    {
        public Guid id { get; set; }
    }
}
