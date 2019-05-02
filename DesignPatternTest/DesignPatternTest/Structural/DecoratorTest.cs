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

        [Test]
        public void Test_Sword_Empower()
        {
            Weapon sword = new Sword();
            Console.WriteLine($"{sword.GetName()} attack power: {sword.AttackPower()} ");

            sword = new Long(sword);
            Console.WriteLine($"{sword.GetName()} attack power: {sword.AttackPower()} ");

            sword = new Sharp(sword);
            Console.WriteLine($"{sword.GetName()} attack power: {sword.AttackPower()} ");

            sword = new Toxic(sword);
            Console.WriteLine($"{sword.GetName()} attack power: {sword.AttackPower()} ");

            sword = new Legendary(sword);
            Console.WriteLine($"{sword.GetName()} attack power: {sword.AttackPower()} ");
        }

        [Test]
        public void Test_knife_Empower()
        {
            Weapon knife = new Knife();
            Console.WriteLine($"{knife.GetName()} attack power: {knife.AttackPower()} ");

            knife = new Long(knife);
            Console.WriteLine($"{knife.GetName()} attack power: {knife.AttackPower()} ");

            knife = new Sharp(knife);
            Console.WriteLine($"{knife.GetName()} attack power: {knife.AttackPower()} ");

            knife = new Toxic(knife);
            Console.WriteLine($"{knife.GetName()} attack power: {knife.AttackPower()} ");

            knife = new Legendary(knife);
            Console.WriteLine($"{knife.GetName()} attack power: {knife.AttackPower()} ");
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

    //----------------------------------------------------------------------
    //武器 抽象類
    public abstract class Weapon
    {
        public string Name = "砂鍋大的鐵拳";

        public virtual string GetName()
        {
            return Name;
        }

        public abstract double AttackPower();
    }

    //劍
    public class Sword : Weapon
    {
        public Sword()
        {
            base.Name = "劍";
        }

        public override double AttackPower()
        {
            return 10;
        }
    }

    //刀
    public class Knife : Weapon
    {
        public Knife()
        {
            base.Name = "刀";
        }

        public override double AttackPower()
        {
            return 5;
        }
    }

    //強化武器 抽象裝飾者類
    public abstract class StrengthenDecorator : Weapon
    {
        public abstract override double AttackPower();
    }

    //加長
    public class Long : StrengthenDecorator
    {
        private Weapon _weapon;


        public Long(Weapon weapon)
        {
            _weapon = weapon;
        }

        public override double AttackPower()
        {
            return _weapon.AttackPower() + 5;
        }

        public override string GetName()
        {
            return "加長的" + _weapon.GetName();
        }
    }

    //鋒利
    public class Sharp : StrengthenDecorator
    {
        private Weapon _weapon;

        public Sharp(Weapon weapon)
        {
            _weapon = weapon;
        }

        public override double AttackPower()
        {
            return _weapon.AttackPower() + 20;
        }

        public override string GetName()
        {
            return "鋒利的" + _weapon.GetName();
        }
    }

    //劇毒
    public class Toxic : StrengthenDecorator
    {
        private Weapon _weapon;

        public Toxic(Weapon weapon)
        {
            _weapon = weapon;
        }

        public override double AttackPower()
        {
            return _weapon.AttackPower() + 100;
        }

        public override string GetName()
        {
            return "劇毒的" + _weapon.GetName();
        }
    }

    //傳說
    public class Legendary : StrengthenDecorator
    {
        private Weapon _weapon;

        public Legendary(Weapon weapon)
        {
            _weapon = weapon;
        }

        public override double AttackPower()
        {
            return _weapon.AttackPower() + 9999;
        }

        public override string GetName()
        {
            return "傳說的" + _weapon.GetName();
        }
    }
}