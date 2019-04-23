using NUnit.Framework;
using System;

namespace DesignPatternTest
{
    [TestFixture]
    public class BridgeTest
    {
        [Test]
        public void playlist_play()
        {
            Frame logo = new Frame("片頭 LOGO");

            Playlist playlist1 = new Playlist();
            playlist1.Add(new Frame("Duke 左揮手"));
            playlist1.Add(new Frame("Duke 右揮手"));

            Playlist playlist2 = new Playlist();
            playlist2.Add(new Frame("Duke 走左腳"));
            playlist2.Add(new Frame("Duke 走右腳"));

            Playlist all = new Playlist();
            all.Add(logo);
            all.Add(playlist1._frames);
            all.Add(playlist2._frames);

            all.Play();
        }
    }

    // Abstraction
    public interface RemoteControl
    {
        void On();

        void Off();

        void SetChannel(int channel);

        void GetTvName();
    }

    // Implementor
    public interface TvFucntion
    {
        // 這些是跟廠商高度相依的行為
        // 把這些行為封裝起來
        void On();

        void Off();

        void SetChannel(int channel);
    }

    // ConcreteImplementor
    public class SonyTv : TvFucntion
    {
        public void On()
        {
            throw new NotImplementedException();
        }

        public void Off()
        {
            throw new NotImplementedException();
        }

        public void SetChannel(int channel)
        {
            throw new NotImplementedException();
        }
    }

    // 搖控器實作, 就是 Abstraction 實作
    public class ConcreteRemote : RemoteControl
    {
        private String mTvName;

        // 與電視廠商相依的行為已經被封裝起來,
        // 搖控器不用管這些行為如何實作的
        private TvFucntion mTvFunction;

        public ConcreteRemote(TvFucntion tvFucntion)
        {
            // 只要替換掉 tvFucntion,
            // 就能操作不同廠商的電視
            mTvFunction = tvFucntion;

            // 這個當然也能封裝在 TvFuntion 裡
            mTvName = "Sony TV";
        }

        // 以下就是橋接模式的威力所在,
        // 搖控器現在已經跟電視功能鬆綁了

        public void On()
        {
            mTvFunction.On();
        }

        public void Off()
        {
            mTvFunction.Off();
        }

        public void SetChannel(int channel)
        {
            mTvFunction.SetChannel(channel);
        }

        public void GetTvName()
        {
            throw new NotImplementedException();
        }
    }
}