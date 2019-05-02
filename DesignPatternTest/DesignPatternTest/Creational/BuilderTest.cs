using NUnit.Framework;
using System;

namespace DesignPatternTest
{
    [TestFixture]
    public class DirectorTest
    {
        [Test]
        public void Test_Builder_Drink()
        {
            var aa = new Director();
            aa.setBulider(new 大杯珍奶Builder());
            aa.create();

            Console.WriteLine("----------");

            aa.setBulider(new 小杯紅茶Builder());
            aa.create();
        }
    }

    //標準化的生產步驟
    public interface IBulider
    {
        void 拿杯子();

        void 裝飲料();

        void 加蓋子();

        void 拿吸管();
    }

    //大杯奶茶生產過程，實作 Bulider 介面
    public class 大杯珍奶Builder : IBulider
    {
        public void 拿杯子()
        {
            Console.WriteLine("拿大杯子");
        }

        public void 裝飲料()
        {
            Console.WriteLine("裝珍珠、裝奶茶");
        }

        public void 加蓋子()
        {
            Console.WriteLine("拿大蓋子加蓋");
        }

        public void 拿吸管()
        {
            Console.WriteLine("拿粗吸管");
        }
    }

    //小杯紅茶生產過程，實作 Bulider 介面
    public class 小杯紅茶Builder : IBulider
    {
        public void 拿杯子()
        {
            Console.WriteLine("拿小杯子");
        }

        public void 裝飲料()
        {
            Console.WriteLine("裝紅茶");
        }

        public void 加蓋子()
        {
            Console.WriteLine("拿小蓋子加蓋");
        }

        public void 拿吸管()
        {
            Console.WriteLine("拿細吸管");
        }
    }

    //統一由指揮者 class 執行生產步驟
    internal class Director
    {
        private IBulider builder;

        public void setBulider(IBulider builder)
        {
            this.builder = builder;
        }

        public void create()
        {
            this.builder.拿杯子();
            this.builder.裝飲料();
            this.builder.加蓋子();
            this.builder.拿吸管();
        }
    }
}