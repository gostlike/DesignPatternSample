using NUnit.Framework;
using System;

namespace DesignPatternTest
{
    /// <summary>
    /// Command Implement and Injection by Client
    /// </summary>
    [TestFixture]
    public class StrategyTest
    {
        [Test]
        public void shoppercar_80off()
        {
            var product = new Product(100,50);
            var shoppingCar = new ShoppingCar(new Price80off());
            shoppingCar.Counting(product);

            shoppingCar = new ShoppingCar(new Price60off());
            shoppingCar.Counting(product);

            shoppingCar = new ShoppingCar(new Price70off());
            shoppingCar.Counting(product);
        }

        [Test]
        public void shoppercar_50off()
        {
            var product = new Product(100, 50);
            Console.WriteLine(ShoppingCar.counting2(product, 0.5m));
        }
    }

    public interface IPrice
    {
        decimal price(Product product);
    }

    public class Price80off:IPrice
    {
        public decimal price(Product product)
        {
            return product.price * 0.8m;
        }
    }

    public class Price70off : IPrice
    {
        public decimal price(Product product)
        {
            return product.price * 0.7m;
        }
    }

    public class Price60off : IPrice
    {
        public decimal price(Product product)
        {
            return product.price * 0.6m;
        }
    }

    public class ShoppingCar
    {
        public IPrice priceCounter;

        public ShoppingCar(IPrice priceCounter)
        {
            this.priceCounter = priceCounter;
        }

        public void Counting(Product product)
        {
            Console.WriteLine(priceCounter.price(product));
        }

        public static Func<Product, decimal, decimal> counting2 = (p, discount) => p.price * discount;
    }
}