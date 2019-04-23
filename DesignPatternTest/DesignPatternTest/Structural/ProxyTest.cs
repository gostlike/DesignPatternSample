using NUnit.Framework;
using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace DesignPatternTest
{
    [TestFixture]
    public class ProxyTest
    {
        [Test]
        public void Printer_Proxy_Working_Until_Printer_Instantiate()
        {
            IPrintable p = new PrinterProxy("Alice");
            Console.WriteLine("現在的名稱是" + p.getPrinterName() + "。");
            p.setPrinterName("Bob");
            Console.WriteLine("現在的名稱是" + p.getPrinterName() + "。");
            //Only Proxy could work
            p.print("Hello, world.");
        }
    }

    public interface IPrintable
    {
        void setPrinterName(string name);   // 命名

        string getPrinterName();            // 取得名稱

        void print(string msg);          // 輸出字串（列印）
    }

    /// <summary>
    /// Printer
    /// </summary>
    public class Printer : IPrintable
    {
        private string name;

        public Printer()
        {
            heavyJob("正在產生Printer的物件個體");
        }

        public Printer(string name)
        {                   // 建構子
            this.name = name;
            heavyJob("正在產生Printer的物件個體(" + name + ")");
        }

        public void setPrinterName(string name)
        {       // 命名
            this.name = name;
        }

        public string getPrinterName()
        {                // 取得名稱
            return name;
        }

        public void print(string msg)
        {              // 輸出名稱
            Console.WriteLine("=== " + name + " ===");
            Console.WriteLine(msg);
        }

        private void heavyJob(string msg)
        {             // 較重的工作（假設）
            Console.WriteLine(msg);
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    Thread.Sleep(1000);
                }
                catch (ThreadInterruptedException e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine(".");
            }
            Console.WriteLine("完成。");
        }
    }

    /// <summary>
    /// Printer Proxy
    /// </summary>
    public class PrinterProxy : IPrintable
    {
        private string name;            // 命名
        private Printer real;           // Printer

        public PrinterProxy()
        {
        }

        public PrinterProxy(string name)
        {      // 建構子
            this.name = name;
        }

        /// <summary>
        /// proxy working but real not instantiate
        /// </summary>
        /// <param name="name"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void setPrinterName(string name)
        {  // 命名
            if (real != null)
            {
                real.setPrinterName(name);  // 「本人」也要命名
            }
            this.name = name;
        }

        /// <summary>
        /// proxy working but real not instantiate
        /// </summary>
        /// <returns></returns>
        public string getPrinterName()
        {    // 取得名稱
            return name;
        }

        /// <summary>
        /// real instantiate
        /// </summary>
        /// <param name="msg"></param>
        public void print(string msg)
        {  // 輸出
            realize();
            real.print(msg);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void realize()
        {   // 產生「本人」
            if (real == null)
            {
                real = new Printer(name);
            }
        }
    }
}