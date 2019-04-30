using NUnit.Framework;
using System;

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
}