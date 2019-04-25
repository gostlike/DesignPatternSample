using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace DesignPatternTest
{
    /// <summary>
    /// Principles for Abstract Class Inheritance
    /// </summary>
    [TestFixture]
    public class ObserverTest
    {
        [Test]
        public void MakeDrink_by_Recipe()
        {
            var john = new Customer("John");
            var steven = new Customer("Steven");
            var harry = new Customer("Harry");
            //Client
            var newspaperOffice = new NewspaperOffice();
            newspaperOffice.SubscribeNewspaper(john);
            newspaperOffice.SubscribeNewspaper(steven);
            newspaperOffice.SubscribeNewspaper(harry);

            //Server Send
            newspaperOffice.SendNewspaper("New Message One");

            //Client Unsubscribe
            newspaperOffice.UnsubscribeNewspaper(harry);

            //Server Send
            newspaperOffice.SendNewspaper("New Message Two");
        }
    }

    public interface ISubject
    {
        void RegisterObserver(IObserver pObserver);

        void RemoveObserver(IObserver pObserver);

        void notifyObservers(string pContent);
    }

    public interface IObserver
    {
        void Update(string pMessage);
    }

    public class NewspaperOffice : ISubject
    {
        public List<IObserver> observers; // 使用List來存放觀察者名單

        public NewspaperOffice()
        {
            observers = new List<IObserver>();
        }

        // 加入觀察者
        public void RegisterObserver(IObserver pObserver)
        {
            observers.Add(pObserver);
        }

        // 移除觀察者
        public void RemoveObserver(IObserver pObserver)
        {
            if (observers.IndexOf(pObserver) >= 0)
                observers.Remove(pObserver);
        }

        // 發送通知給在監聽名單中的觀察者
        public void notifyObservers(string pContent)
        {
            observers.ForEach(o => o.Update(pContent));
        }

        // 訂閱報紙
        public void SubscribeNewspaper(IObserver pCustomer)
        {
            RegisterObserver(pCustomer);
        }

        // 取消訂閱報紙
        public void UnsubscribeNewspaper(IObserver pCustomer)
        {
            RemoveObserver(pCustomer);
        }

        // 發送新消息
        public void SendNewspaper(string pContent)
        {
            Console.WriteLine("Send News..");
            notifyObservers(pContent);
        }
    }

    public class Customer : IObserver
    {
        public string MyName { private get; set; }

        public Customer(string pName)
        {
            MyName = pName;
        }

        // 更新最新消息
        public void Update(string pMessage)
        {
            Console.WriteLine("{0} receive a new message:{1}", MyName, pMessage);
        }
    }
}