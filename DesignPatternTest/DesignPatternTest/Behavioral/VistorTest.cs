using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace DesignPatternTest
{
    /// <summary>
    /// Visitor : overload method
    /// Strategy => Visitor
    /// </summary>
    [TestFixture]
    public class VisitorTest
    {
        [Test]
        public void VIP_Member_Service()
        {
            var service = new Service();
            service.DoService(new Member());
            service.DoService(new VIP());
        }

        [Test]
        public void Math_Visitor()
        {
            // 物件結構
            var o = new ObjectStructure();
            // 誠實人元素物件
            var honestMan = new HonestManElement();
            // 說謊人元素物件
            var lieMan = new LieManElement();

            // 將誠實人、說謊人元素物件放進物件結構
            o.Attach(honestMan);
            o.Attach(lieMan);

            // 數學問題訪問者
            MathVisitor mathVisitor = new MathVisitor();
            Console.WriteLine("[數學問題]");
            o.Display(mathVisitor); // 輸出結果
        }

        [Test]
        public void Physical_Visitor()
        {
            // 物件結構
            var o = new ObjectStructure();
            // 誠實人元素物件
            var honestMan = new HonestManElement();
            // 說謊人元素物件
            var lieMan = new LieManElement();

            // 將誠實人、說謊人元素物件放進物件結構
            o.Attach(honestMan);
            o.Attach(lieMan);

            // 物理問題訪問者
            PhysicsVisitor physicsVisitor = new PhysicsVisitor();
            Console.WriteLine("[物理問題]");
            o.Display(physicsVisitor); // 輸出結果
        }
    }

    public class Service
    {
        private IVisitor visitor = new ImpVisitor();

        public void DoService(VirtualClient virtualClient)
        {
            virtualClient.DoFreeService();
            virtualClient.accept(visitor);
            virtualClient.pay();
        }
    }

    public interface IVisitable
    {
        void accept(IVisitor visitor);
    }

    public interface IVisitor
    {
        void visit(Member member);

        void visit(VIP vip);
    }

    public class VirtualClient : IVisitable
    {
        public void DoFreeService()
        {
            Console.WriteLine("免費服務!");
        }

        public void pay()
        {
            Console.WriteLine("結帳!");
        }

        public virtual void accept(IVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }

    public class VIP : VirtualClient
    {
        public void DoService()
        {
            Console.WriteLine("VIP服務!");
        }

        public override void accept(IVisitor visitor)
        {
            visitor.visit(this);
        }
    }

    public class Member : VirtualClient
    {
        public void DoService()
        {
            Console.WriteLine("會員服務!");
        }

        public override void accept(IVisitor visitor)
        {
            visitor.visit(this);
        }
    }

    public class ImpVisitor : IVisitor
    {
        public void visit(Member member)
        {
            member.DoService();
        }

        public void visit(VIP vip)
        {
            vip.DoService();
        }
    }

    // 物件結構
    internal class ObjectStructure
    {
        private List<Element> elements = new List<Element>();

        //增加元素物件
        public void Attach(Element element)
        {
            elements.Add(element);
        }

        //移除元素物件
        public void Detach(Element element)
        {
            elements.Remove(element);
        }

        //顯示
        public void Display(Visitor visitor)
        {
            foreach (Element e in elements)
            {
                e.Accept(visitor);
            }
        }
    }

    // 元素抽像類別 (要放入物件結構中)
    internal abstract class Element
    {
        // 每個元素要能接收訪問者物件，以便再將自己傳給訪問者
        public abstract void Accept(Visitor visitor);
    }

    // 誠實人元素物件
    internal class HonestManElement : Element
    {
        public string name = "誠實人";

        public override void Accept(Visitor visitor)
        {
            // 將自己傳給訪問者，以便訪問者分辨、執行適合自己的行為
            visitor.visit(this);
        }
    }

    // 說謊人元素物件
    internal class LieManElement : Element
    {
        public string name = "說謊人";

        public override void Accept(Visitor visitor)
        {
            // 將自己傳給訪問者，以便訪問者分辨、執行適合自己的行為
            visitor.visit(this);
        }
    }

    // 訪問者 (能根據不同元素，產生不同結果)
    internal abstract class Visitor
    {
        // 訪問誠實人的多載方法
        public abstract void visit(HonestManElement honestElement);

        // 訪問說謊人的多載方法
        public abstract void visit(LieManElement lieElement);
    }

    // 數學問題訪問者
    internal class MathVisitor : Visitor
    {
        // 訪問誠實人的多載方法
        public override void visit(HonestManElement honestElement)
        {
            Console.WriteLine("{0} 說: 1+1=2", honestElement.name);
        }

        // 訪問說謊人的多載方法
        public override void visit(LieManElement lieElement)
        {
            Console.WriteLine("{0} 說: 1+1=3", lieElement.name);
        }
    }

    // 物理問題訪問者
    internal class PhysicsVisitor : Visitor
    {
        // 訪問誠實人的多載方法
        public override void visit(HonestManElement honestElement)
        {
            Console.WriteLine("{0} 說: 鐵球在水中會沉下去", honestElement.name);
        }

        // 訪問說謊人的多載方法
        public override void visit(LieManElement lieElement)
        {
            Console.WriteLine("{0} 說: 鐵球在水中會浮起來", lieElement.name);
        }
    }
}