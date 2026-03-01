using System;
using System.Collections.Generic;

namespace SupermarketReceipt.Domain.Products
{
    /// <summary>
    /// Contains product details
    /// </summary>
    public class Product(string name, ProductUnit unit)
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; } = name;
        public ProductUnit Unit { get; } = unit;

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