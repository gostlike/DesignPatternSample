using NUnit.Framework;
using System;
using System.Runtime.CompilerServices;

namespace DesignPatternTest
{
    [TestFixture]
    public class FacadeTest
    {
        [Test]
        public void InvestmentFacade()
        {
            var investmentFunction = new InvestmentFunction();
            investmentFunction.Investment();
        }
    }


    public class InvestmentFunction
    {
        public void Investment()
        {
            var orderSystem = new OrderSystem();
            var compliantSystem = new CompliantSystem();
            var accountingSystem = new AccountingSystem();
            orderSystem.order();
            compliantSystem.IsCompliant();
            accountingSystem.Accounting();
        }
    }


    public interface IInvestment
    {
        void order();
        void IsCompliant();
        void Accounting();
    }

    

    public class OrderSystem : IInvestment
    {
        public void order()
        {
            Console.WriteLine("Ordering...");
        }

        public void IsCompliant()
        {
            throw new NotImplementedException();
        }

        public void Accounting()
        {
            throw new NotImplementedException();
        }
    }

    public class CompliantSystem: IInvestment
    {
        public void order()
        {
            throw new NotImplementedException();
        }

        public void IsCompliant()
        {
            Console.WriteLine("Compliance pass");
        }

        public void Accounting()
        {
            throw new NotImplementedException();
        }
    }

    public class AccountingSystem:IInvestment
    {
        public void order()
        {
            throw new NotImplementedException();
        }

        public void IsCompliant()
        {
            throw new NotImplementedException();
        }

        public void Accounting()
        {
            Console.WriteLine("Accounting...");
        }
    }
}