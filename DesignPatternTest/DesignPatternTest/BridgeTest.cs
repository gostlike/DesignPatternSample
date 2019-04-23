using NUnit.Framework;
using System;
using System.Runtime.CompilerServices;

namespace DesignPatternTest
{
    [TestFixture]
    public class BridgeTest
    {
        [Test]
        public void use_sony_remoteControl()
        {
          IRemoteControl remoteControl = new ConcreteRemote(new SonyTv());
          remoteControl.GetTvName();
          remoteControl.On();
          remoteControl.Off();
          remoteControl.SetChannel(199);
        }
    }

    // Abstraction
    public interface IRemoteControl
    {
        void On();

        void Off();

        void SetChannel(int channel);

        string GetTvName();
    }

    // Implementor
    public interface ITvFucntion
    {
        // 這些是跟廠商高度相依的行為
        // 把這些行為封裝起來
        void On();

        void Off();

        void SetChannel(int channel);

        string getTvName();
    }

    // ConcreteImplementor
    public class SonyTv : ITvFucntion
    {
        public string TvName = "Sony TV";
        
        public string getTvName()
        {
            return TvName;
        }

        public void On()
        {
            Console.WriteLine("Sony Tv On");
        }

        public void Off()
        {
            Console.WriteLine("Sony Tv Off");

        }

        public void SetChannel(int channel)
        {
            Console.WriteLine($"Sony Tv Set Channel :{channel}");
        }
    }

    // 搖控器實作, 就是 Abstraction 實作
    public class ConcreteRemote : IRemoteControl
    {
        private String mTvName;

        // 與電視廠商相依的行為已經被封裝起來,
        // 搖控器不用管這些行為如何實作的
        private ITvFucntion tvImplementor;

        public ConcreteRemote(ITvFucntion tvFucntion) 
        {
            // 只要替換掉 tvFucntion,
            // 就能操作不同廠商的電視
            tvImplementor = tvFucntion;

            // 這個當然也能封裝在 TvFuntion 裡
            mTvName = tvFucntion.getTvName();
        }

        // 以下就是橋接模式的威力所在,
        // 搖控器現在已經跟電視功能鬆綁了

        public void On()
        {
            tvImplementor.On();
        }

        public void Off()
        {
            tvImplementor.Off();
        }

        public void SetChannel(int channel)
        {
            tvImplementor.SetChannel(channel);
        }

        public string GetTvName()
        {
            return tvImplementor.getTvName();
        }
    }
}