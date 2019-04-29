using NUnit.Framework;
using System;

namespace DesignPatternTest
{
    /// <summary>
    /// Principles for Abstract Class Inheritance
    /// </summary>
    [TestFixture]
    public class MediatorTest
    {
        [Test]
        public void MakeDrink_by_Recipe()
        {
            var commander = new ConcreteMediator(); // 中介者

            var medic = new MedicColleague("醫護兵", commander); // 醫護兵
            var infantry = new InfantryColleague("戰士", commander); // 步兵

            medic.Send("normal", "醫護兵待命");
            infantry.Send("normal", "準備戰鬥");
            medic.Send("attack", "遭受敵人攻擊");
            infantry.Send("hurt", "我中彈了");
        }
    }

    // 中介者抽像類別
    internal abstract class Mediator
    {
        protected MedicColleague medic; // 醫護兵
        protected InfantryColleague infantry; // 步兵

        // 設定醫護兵
        public MedicColleague Medic
        {
            set { medic = value; }
        }

        // 設定步兵
        public InfantryColleague Infantry
        {
            set { infantry = value; }
        }

        public abstract void Work(string msgType, string msgCon, Colleague colleague);
    }

    // 中介者
    internal class ConcreteMediator : Mediator
    {
        // 中介者處理接收到的訊息
        public override void Work(string msgType, string msgCon, Colleague colleague)
        {
            Console.WriteLine("指揮官 接收到 {0} 訊息：{1} => 準備下達命令貨同步...", colleague.Name, msgCon);
            switch (msgType)
            {
                case "hurt":
                    this.medic.Action(msgCon, colleague);
                    break;

                case "attack":
                    this.infantry.Action(msgCon, colleague);
                    break;

                case "normal":
                    if (colleague != this.medic) this.medic.Receive(msgCon, colleague);
                    if (colleague != this.infantry) this.infantry.Receive(msgCon, colleague);
                    break;
            }
        }
    }

    // 同事抽象類別
    internal abstract class Colleague
    {
        protected string name; // 姓名

        protected Mediator mediator; // 中介者

        // 設定姓名、中介者
        public Colleague(string name, Mediator mediator)
        {
            this.name = name;
            this.mediator = mediator;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        // 發送訊息給中介者
        public void Send(string msgType, string msgCon)
        {
            mediator.Work(msgType, msgCon, this);
        }

        // 接收一般訊息
        public void Receive(string msgCon, Colleague coworker)
        {
            Console.WriteLine("{0} 接收到 {1} 訊息：{2}", this.name, coworker.Name, msgCon);
        }
    }

    // 醫護兵
    internal class MedicColleague : Colleague
    {
        public MedicColleague(string name, Mediator mediator)
            : base(name, mediator)
        {
            mediator.Medic = this;
        }

        // 醫療行動
        public void Action(string msgCon, Colleague colleague)
        {
            Console.WriteLine("{0} 接收到 {1} 訊息：{2}。前往救護中", this.name, colleague.Name, msgCon);
        }
    }

    // 步兵
    internal class InfantryColleague : Colleague
    {
        public InfantryColleague(string name, Mediator mediator)
            : base(name, mediator)
        {
            mediator.Infantry = this;
        }

        // 攻擊行動
        public void Action(string msgCon, Colleague colleague)
        {
            Console.WriteLine("{0} 接收到 {1} 訊息：{2}。前往攻擊中", this.name, colleague.Name, msgCon);
        }
    }
}