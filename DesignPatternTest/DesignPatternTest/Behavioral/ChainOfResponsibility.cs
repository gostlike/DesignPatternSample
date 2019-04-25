using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternTest
{
    /// <summary>
    /// Command Implement and Injection by Client
    /// </summary>
    [TestFixture]
    public class ChainOfResponsibilityTest
    {
        [Test]
        public void manager_approve_leave_chain_by_ChainOfResponsibility()
        {
            ManagerHandler manager = new Manager("Manager");
            ManagerHandler director = new Director("Director");
            ManagerHandler generalManager = new GeneralManager("GenerManager");
            manager.SetUpManager(director);
            director.SetUpManager(generalManager);

            var request = new LeaveRequest();
            request.Name = "Harry";
            request.Days = 1;
            manager.ApplyLeaveRequest(request);

            request.Days = 4;
            manager.ApplyLeaveRequest(request);

            request.Days = 7;
            manager.ApplyLeaveRequest(request);
        }

        [Test]
        public void manager_approve_leave_chain_by_flyweight_and_LINQ()
        {
            Func<int, bool>[] approveArray = {
                 d =>d<=3,
                 d =>d<=5,
                 d =>d>5,
            };

            var ApproveChain = new Dictionary<string, Func<int, bool>>()
            {
                {"Manager",d=>d<=3},
                {"Director",d=>d<=5},
                {"GeneralManager",d=>d>5}
            };

            var request = new LeaveRequest();
            request.Name = "Harry";
            request.Days = 1;
            var approve = ApproveChain
                .FirstOrDefault(k => k.Value(request.Days) == true);
            Console.WriteLine($"{approve.Key} Approve!");

            request.Days = 4;
            approve = ApproveChain
                .FirstOrDefault(k => k.Value(request.Days) == true);
            Console.WriteLine($"{approve.Key} Approve!");

            request.Days = 6;
            approve = ApproveChain
                .FirstOrDefault(k => k.Value(request.Days) == true);
            Console.WriteLine($"{approve.Key} Approve!");
        }
    }

    public class GeneralManager : ManagerHandler
    {
        public GeneralManager(string name) : base(name)
        {
        }

        public override void ApplyLeaveRequest(LeaveRequest leaveRequest)
        {
            if (leaveRequest.Days > 5)
            {
                Console.WriteLine($"GenerManager:{_name} Approve!");
            }
            else
            {
                UpManager.ApplyLeaveRequest(leaveRequest);
            }
        }
    }

    public class Director : ManagerHandler
    {
        public Director(string name) : base(name)
        {
        }

        public override void ApplyLeaveRequest(LeaveRequest leaveRequest)
        {
            if (leaveRequest.Days <= 5)
            {
                Console.WriteLine($"Director:{_name} Approve!");
            }
            else
            {
                UpManager.ApplyLeaveRequest(leaveRequest);
            }
        }
    }

    public class Manager : ManagerHandler
    {
        public Manager(string name) : base(name)
        {
        }

        public override void ApplyLeaveRequest(LeaveRequest leaveRequest)
        {
            if (leaveRequest.Days <= 2)
            {
                Console.WriteLine($"Manager:{_name} Approve!");
            }
            else
            {
                UpManager.ApplyLeaveRequest(leaveRequest);
            }
        }
    }

    public abstract class ManagerHandler
    {
        protected string _name;
        protected ManagerHandler UpManager;

        public ManagerHandler(string name)
        {
            _name = name;
        }

        //public Action<ManagerHandler> SetUpManager = manager => UpManager = manager;
        public void SetUpManager(ManagerHandler manager)
        {
            UpManager = manager;
        }

        abstract public void ApplyLeaveRequest(LeaveRequest leaveRequest);
    }

    public class ManagerHandler2
    {
        public static string _name;
        public static ManagerHandler2 UpManager;

        public void SetUpManager(ManagerHandler2 manager)
        {
            UpManager = manager;
        }

        public Action<LeaveRequest, string, int> ApplyLeaveRequest = (l, m, d) =>
         {
             if (l.Days < d)
                 Console.WriteLine($"{_name} approve!");
             else
                 UpManager.ApplyLeaveRequest(l, m, d);
         };

        public ManagerHandler2(string name)
        {
            _name = name;
        }
    }

    public class LeaveRequest
    {
        public string Name { get; set; }
        public int Days { get; set; }
    }
}