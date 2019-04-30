using NUnit.Framework;
using System;

namespace DesignPatternTest
{
    [TestFixture]
    public class SimpleFactoryTest
    {
        [Test]
        public void SimpleFactory()
        {
            var factory = new SimpleFactory();
            var toy = factory.CreateToy("ToyCat");
            toy.Play();
            toy = factory.CreateToy("ToyCar");
            toy.Play();
        }

        [Test]
        public void Factory()
        {
            ToyFactory factory = new ToyCarFactory();
            var car = factory.CreateToy();
            car.Play();

            factory = new ToyCatFactory();
            var cat = factory.CreateToy();
            cat.Play();
        }

        [Test]
        public void AbstractFactory()
        {
            AbstractFactory factory;
            
            factory = new Factory_A();
            factory.CreateComponentA().WorkA();
            factory.CreateComponentB().WorkB();

            factory = new Factory_B();
            factory.CreateComponentA().WorkA();
            factory.CreateComponentB().WorkB();
        }
    }

    #region Simple Factory

    public abstract class Toy
    {
        public abstract void Play();
    }

    public class ToyCar : Toy
    {
        public override void Play()
        {
            Console.WriteLine("Play Toy Car!");
        }
    }

    public class ToyCat : Toy
    {
        public override void Play()
        {
            Console.WriteLine("Play Toy Cat!");
        }
    }

    public class SimpleFactory
    {
        public Toy CreateToy(string name)
        {
            switch (name)
            {
                case "ToyCar":
                    return new ToyCar();

                case "ToyCat":
                    return new ToyCat();

                default:
                    return default(Toy);
            }
        }
    }

    #endregion Simple Factory

    #region Factory 改善 Simple Factory 修改 factory.create,符合OCP扩充的方式

    internal interface ToyFactory
    {
        Toy CreateToy();
    }

    internal class ToyCarFactory : ToyFactory
    {
        public Toy CreateToy()
        {
            return new ToyCar();
        }
    }

    internal class ToyCatFactory : ToyFactory
    {
        public Toy CreateToy()
        {
            return new ToyCat();
        }
    }

    #endregion Factory 改善 Simple Factory 修改 factory.create,符合OCP扩充的方式

    #region Abstract Factory

    //生產ComponentA的抽像類別
    internal abstract class ComponentA
    {
        public abstract void WorkA();
    }

    //生產ComponentB的抽像類別
    internal abstract class ComponentB
    {
        public abstract void WorkB();
    }

    //A 品牌ComponentA
    internal class ComponentA_A : ComponentA
    {
        public override void WorkA()
        {
            Console.WriteLine("A 品牌 製造的 ComponentA");
        }
    }

    //A 品牌ComponentB
    internal class ComponentB_A : ComponentB
    {
        public override void WorkB()
        {
            Console.WriteLine("A 品牌 製造的 ComponentB");
        }
    }

    //B 品牌ComponentA
    internal class ComponentA_B : ComponentA
    {
        public override void WorkA()
        {
            Console.WriteLine("B 品牌 製造的 ComponentA");
        }
    }

    //B 品牌ComponentB
    internal class ComponentB_B : ComponentB
    {
        public override void WorkB()
        {
            Console.WriteLine("B 品牌 製造的 ComponentB");
        }
    }

    // 抽象工廠
    internal interface AbstractFactory
    {
        ComponentA CreateComponentA();

        ComponentB CreateComponentB();
    }

    // 回傳 A 品牌，實做的物件
    internal class Factory_A : AbstractFactory
    {
        public ComponentA CreateComponentA()
        {
            return new ComponentA_A();
        }

        public ComponentB CreateComponentB()
        {
            return new ComponentB_A();
        }
    }

    // 回傳 B 品牌，實做的物件
    internal class Factory_B : AbstractFactory
    {
        public ComponentA CreateComponentA()
        {
            return new ComponentA_B();
        }

        public ComponentB CreateComponentB()
        {
            return new ComponentB_B();
        }
    }

    #endregion Abstract Factory
}