using System;
using System.Collections.Generic;

namespace SupermarketReceipt.Domain.Products
{
    /// <summary>
    /// Contains product details
    /// </summary>
    public class Product
    {
        public Product(string name, ProductUnit unit)
        {
            Id = Guid.NewGuid();
            Name = name;
            Unit = unit;
        }

        public Guid Id { get; }
        public string Name { get; }
        public ProductUnit Unit { get; }

        public override bool Equals(object obj)
        {
            return obj is Product product &&
                   Name == product.Name &&
                   Unit == product.Unit;
        }

        public override int GetHashCode()
        {
            var hashCode = -1996304355;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Unit.GetHashCode();
            return hashCode;
        }
    }
}