using NUnit.Framework;
using System;

namespace DesignPatternTest
{
    /// <summary>
    /// Command Implement and Injection by Client
    /// </summary>
    [TestFixture]
    public class ChainOfResponsibilityTest
    {
        [Test]
        public void manager_approve_leave_request()
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
        public string _name;
        public ManagerHandler2 UpManager;

        public void SetUpManager(ManagerHandler2 manager)
        {
            UpManager = manager;
        }
        public Action<LeaveRequest,int> ApplyLeaveRequest;

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