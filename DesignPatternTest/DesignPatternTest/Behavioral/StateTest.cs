using System;
using System.Threading;
using NUnit.Framework;

namespace DesignPatternTest
{
    /// <summary>
    /// Command Implement and Injection by Client
    /// </summary>
    [TestFixture]
    public class StateTest
    {
        [Test]
        public void shoppercar_80off()
        {
            var trafficLight = new TrafficLight();
            //trafficLight.Light = new RED();
            //while (true)
            //{
                trafficLight.Change();
                trafficLight.Change();
                trafficLight.Change();
            //}
        }
    }

    //public interface IState
    //{
    //    void change(TrafficLight trafficLight);
    //}

    public abstract class Light// : IState
    {
        public abstract void change(TrafficLight trafficLight);

        protected void sleep(int sec)
        {
            try
            {
                Thread.Sleep(sec * 1000);
            }
            catch (ThreadInterruptedException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

    public class RED:Light
    {
        public override void change(TrafficLight trafficLight)
        {
            Console.WriteLine("RED...");
            sleep(1);
            trafficLight.Light=new GREEN();
        }
    }

    public class GREEN : Light
    {
        public override void change(TrafficLight trafficLight)
        {
            Console.WriteLine("GREEN...");
            sleep(1);
            trafficLight.Light = new YELLOW();
        }
    }

    public class YELLOW : Light
    {
        public override void change(TrafficLight trafficLight)
        {
            Console.WriteLine("YELLOW...");
            sleep(1);
            trafficLight.Light = new RED();
        }
    }

    public class TrafficLight
    {
        //private IState _light;
        private Light _light = new RED();

        public Light Light
        {
            get { return _light; }
            set { _light = value; }
        }


        public void Change()
       {
           Light.change(this);
       }
    }
}