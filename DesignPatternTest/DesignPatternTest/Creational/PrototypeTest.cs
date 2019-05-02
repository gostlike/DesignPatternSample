using NUnit.Framework;
using System;
using System.Collections;
using System.Linq;

namespace DesignPatternTest
{
    [TestFixture]
    public class PrototypeTest
    {
        [Test]
        public void Test_Prototype()
        {
            var bmw = new Car();
            var benz = new Car();
            var prototypes = new Cars();
            prototypes.addPrototype("BMW",bmw);
            prototypes.addPrototype("BENZ", benz);

            var bmwPrototype = prototypes.getPrototype("BMW");

        }
    }

    internal class Whell:ICloneable
    {
        public object Clone()
        {   // Shallow copy
            return this.MemberwiseClone();
        }
    }

    internal class Car:ICloneable
    {
        private Whell[] wheels = { new Whell(), new Whell(), new Whell(), new Whell()};
        public object Clone()
        {
            return  (Car)this.MemberwiseClone();
            //var car = (Car)this.MemberwiseClone();
            //car.wheels = (Whell[])this.wheels.Clone();
            //int i=0;
            //foreach (var wheel in car.wheels)
            //{
            //    car.wheels[i] = (Whell)wheel.Clone();
            //    i++;
            //}

            //return car;
        }
    }

    internal class Cars
    {
        private Hashtable prototypes = new Hashtable();

        public void addPrototype(string brand, Car car)
        {
            prototypes.Add(brand,car);
        }

        public Car getPrototype(string brand)
        {
            return (Car)((Car)prototypes[brand]).Clone();
        }
    }
}