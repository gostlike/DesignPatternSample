using NUnit.Framework;
using System;

namespace DesignPatternTest
{
    /// <summary>
    /// Principles for Abstract Class Inheritance
    /// </summary>
    [TestFixture]
    public class TemplateMethodTest
    {
        [Test]
        public void MakeDrink_by_Recipe()
        {
            var coffee = new Coffee();
            coffee.prepareRecipe();

            var tea = new Tea();
            tea.prepareRecipe();
        }
    }

    public abstract class Beverage
    {
        // 不宣告為 virtual,abstract 保護,不讓次類別推翻這個方法, 這是統一的演算法, T
        // 允許次類別 Injection
        public void prepareRecipe()
        {
            // 煮開水
            boilWater();

            // 用沸水沖泡
            // 跟上面程式碼比, 方法名更通用
            brew();

            // 把飲料倒進杯子
            pourInCup();

            // 加配料
            // 跟上面程式碼比, 方法名更通用
            addCondiments();
        }

        // 因為咖啡跟茶處理這些方法的做法不同,
        // 所以宣告為抽象方法,
        // 留給次類別去處理
        public abstract void brew();

        public abstract void addCondiments();

        private void boilWater()
        {
            // 不管是茶或咖啡做法都一樣
            // 可以直接把實作寫在超類別
            Console.WriteLine("First Boil Water...");
        }

        private void pourInCup()
        {
            // 不管是茶或咖啡做法都一樣
            // 可以直接把實作寫在超類別
            Console.WriteLine("Pour In Cup...");
        }
    }

    public class Tea : Beverage
    {
        public override void brew()
        {
            Console.WriteLine("Steeping the tea");
        }

        public override void addCondiments()
        {
            Console.WriteLine("Adding lemon, Tea Done");
        }
    }

    public class Coffee : Beverage
    {
        public override void brew()
        {
            Console.WriteLine("Dripping coffee through filter");
        }

        public override void addCondiments()
        {
            Console.WriteLine("Adding sugar and milk, Coffee Done");
        }
    }

    public abstract class BeverageWithHook
    {
        public void prepareRecipe()
        {
            boilWater();
            brew();
            pourInCup();

            // 加上一個判斷式, 如果客戶
            // 想要配料才真的加配料
            if (customerWantsCondiments())
            {
                addCondiments();
            }
        }

        protected abstract void brew();

        protected abstract void addCondiments();

        private void boilWater()
        {
            // 不管是茶或咖啡做法都一樣
            // 可以直接把實作寫在超類別
        }

        private void pourInCup()
        {
            // 不管是茶或咖啡做法都一樣
            // 可以直接把實作寫在超類別
        }

        // 這就是一個掛鉤, 通常是空的實作。
        // 次類別可以推翻(Override) 它,
        // 但不見得要這麼做
        public virtual bool customerWantsCondiments()
        {
            return true;
        }
    }
}