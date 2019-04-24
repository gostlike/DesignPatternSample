using NUnit.Framework;
using System;

namespace DesignPatternTest
{
    [TestFixture]
    public class DecoratorTest
    {
        [Test]
        public void Meal_Description_should_be_FiredChicken_FrenchFires_Drink()
        {
            //order FiredChicken
            //order FrenchFires
            //order Drink
            var meal = new Drink(new FrenchFires(new FriedChicken()));

            Console.WriteLine(meal.Description());

            var expected = "FiredChicken FrenchFires Drink";
            var actual = meal.Description();

            Assert.AreEqual(expected, actual: actual);
        }
    }

    public class FrenchFires : SideDish
    {
        public FrenchFires(Meal meal) : base(meal)
        {
        }

        public override string Description()
        {
            return _meal.Description() + " FrenchFires";
        }

        public override decimal Price()
        {
            return _meal.Price() + 30m;
        }
    }

    public class Drink : SideDish
    {
        public Drink(Meal meal) : base(meal)
        {
        }

        public override string Description()
        {
            return _meal.Description() + " Drink";
        }

        public override decimal Price()
        {
            return _meal.Price() + 10m;
        }
    }

    public abstract class SideDish : Meal
    {
        public Meal _meal;

        protected SideDish(Meal meal)
        {
            _meal = meal;
        }
    }

    public class FriedChicken : Meal
    {
        public override string Description()
        {
            return "FiredChicken";
        }

        public override decimal Price()
        {
            return 10;
        }
    }

    public abstract class Meal
    {
        public abstract string Description();

        public abstract decimal Price();
    }
}