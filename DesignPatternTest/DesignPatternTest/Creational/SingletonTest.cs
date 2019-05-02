using NUnit.Framework;
using System;
using System.Collections;

namespace DesignPatternTest
{
    [TestFixture]
    public class SingletonTest
    {
        [Test]
        public void Test_Lazy_Singleton()
        {
            // 產生第一個實例
            Console.WriteLine("test = {0}", LazySingleton.GetInstance().test); // a.test = 1
            LazySingleton.GetInstance().test = 10; // 將第一個實例的 test 值，改為 10
            Console.WriteLine("test = {0}", LazySingleton.GetInstance().test); // a.test = 10
        }

        [Test]
        public void Test_Eager_Singleton()
        {
            // 產生第二個實例
            Console.WriteLine("test = {0}", EagerSingleton.GetInstance().test); // b.test = 10
        }
    }

    //Lazy initialization
    public class LazySingleton
    {
        // 多執行緒，lock 使用
        private static readonly object thisLock = new object();

        // 將唯一實例設為 private static
        private static LazySingleton instance;

        public int test = 1;

        // 設為 private，外界不能 new
        private LazySingleton()
        {
        }

        // 外界只能使用靜態方法取得實例
        public static LazySingleton GetInstance()
        {
            // 先判斷目前有沒有實例，沒有的話才開始 lock，
            // 此次的判斷，是避免在有實例的情況，也執行 lock ，影響效能
            if (null == instance)
            {
                // 避免多執行緒可能會產生兩個以上的實例，所以 lock
                lock (thisLock)
                {
                    // lock 後，再判斷一次目前有無實例
                    // 此次的判斷，是避免多執行緒，同時通過前一次的 null == instance 判斷
                    if (null == instance)
                    {
                        instance = new LazySingleton();
                    }
                }
            }

            return instance;
        }
    }

    //Eager initialization
    public sealed class EagerSingleton
    {
        // 設為 static，載入時，即 new 一個實例
        private static readonly EagerSingleton instance = new EagerSingleton();

        public int test = 1;

        // 設為 private，外界不能 new
        private EagerSingleton()
        {
        }

        // 使用靜態方法取得實例，因為載入時就 new 一個實例，所以不用考慮多執行緒的問題
        public static EagerSingleton GetInstance()
        {
            return instance;
        }
    }

    public class RegistrySingleton
    {
        private static Hashtable registry = new Hashtable();

        public static object getInstance(string classname)
        {
            var singleton = registry[classname];
            if (singleton == null)
            {
                singleton = Activator.CreateInstance(Type.GetType(classname,true));
                register(classname, singleton);
            }
            return singleton;
        }

        public static void register(string classname, object singleton)
        {
            registry.Add(classname, singleton);
        }
    }
}