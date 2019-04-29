using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;

namespace DesignPatternTest
{
    /// <summary>
    /// Command Implement and Injection by Client
    /// </summary>
    [TestFixture]
    public class MementoTest
    {
        [Test]
        public void Application()
        {
            Application application = new Application();
            Recovery recovery = new Recovery();

            Console.WriteLine(application.getState());

            Backup backup = application.backup();  // 建立備忘
            recovery.add(backup); // 加入備忘錄

            application.setState("customer setting");
            Console.WriteLine(application.getState());

            var date = backup.date; // 假設 date 是使用者自行設定所要取得的還原時間！
            application.recover(recovery.retrieve(date)); // 取得備忘來還原
            Console.WriteLine(application.getState());
        }
    }
    /// <summary>
    /// Memento
    /// </summary>
    public class Backup
    {
        internal string state;
        internal DateTime date;
        public Backup(string state)
        {
            this.state = state;
            this.date = new DateTime();
        }
    }
    /// <summary>
    /// Originator
    /// </summary>
    class Application
    {
        private string state = "default setting";

        public Backup backup()
        {
            return new Backup(state);
        }
        public void recover(Backup backup)
        {
            this.state = backup.state;
        }

        public void setState(string state)
        {
            this.state = state;
        }
        public string getState()
        {
            return state;
        }
    }
    /// <summary>
    /// Caretaker
    /// </summary>
    class Recovery
    {
        private Dictionary<DateTime,Backup> backups = new Dictionary<DateTime, Backup>();
        
        public void add(Backup backup)
        {
            backups.Add(backup.date,backup);
        }
        public Backup retrieve(DateTime date)
        {
            if (backups.ContainsKey(date))
            {
                Backup backup;
                backups.TryGetValue(date, out backup);
                backups.Remove(date);
                return backup;
            }
            else
            {
                return default(Backup);
            }
        }
    }

}