using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternTest
{
    [TestFixture]
    public class FlyweightTest
    {
        [Test]
        public void productValidatorDictiaryHandle()
        {
            var handler = new FlyweightDictHandler();
            handler.create("PriceValidator", new PriceValidator());
            handler.create("ProfitValidator", new ProfitValidator());

            Console.WriteLine(handler.validate(new Product(0, 5)));
            Console.WriteLine(handler.validate(new Product(10, 5)));
            Console.WriteLine(handler.validate(new Product(10, 15)));
        }
    }

    public class FlyweightDictHandler
    {
        private static Dictionary<string, IValidator> flyweightDict
            = new Dictionary<string, IValidator>();

        public Action<string, IValidator> create = (k, v) =>
        {
            if (!flyweightDict.ContainsValue(v))
                flyweightDict.Add(k, v);
        };

        public Action<string, IValidator> remove = (k, v) => flyweightDict.Remove(k);

        public Func<Product, bool> validate = (p) => flyweightDict.Values.All(v => v.Validate(p));

        //public void create(string profitvalidator, IValidator p)
        //{
        //    if (!productValidatorDictionary.ContainsKey(profitvalidator))
        //        productValidatorDictionary.Add(profitvalidator, p);
    
    }

    public class ProfitValidator : IValidator
    {
        public bool Validate(Product product)
        {
            return product.price > product.cost;
        }
    }

    public class PriceValidator : IValidator
    {
        public bool Validate(Product product)
        {
            return product.price > 0;
        }
    }

    public interface IValidator
    {
        bool Validate(Product product);
    }

    public class Product
    {
        public decimal price;
        public decimal cost;

        public Product(decimal price, decimal cost)
        {
            this.price = price;
            this.cost = cost;
        }
    }
}