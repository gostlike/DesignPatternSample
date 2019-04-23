using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace DesignPatternTest
{
    [TestFixture]
    public class CompositeTest
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

    public class Playlist : IPlayer
    {
        internal List<Frame> _frames;

        public Playlist()
        {
            _frames = new List<Frame>();
        }

        public void Play()
        {
            foreach (var frame in _frames)
            {
                frame.Play();
            }
        }

        public void Add(Frame frame)
        {
            _frames.Add(frame);
        }

        public void Add(List<Frame> frames)
        {
            _frames.AddRange(frames);
        }

        public void Remove(Frame frame)
        {
            _frames.Remove(frame);
        }
    }

    public class Frame : IPlayer
    {
        private readonly string _image;

        public Frame(string image)
        {
            _image = image;
        }

        public void Play()
        {
            Console.WriteLine(_image);
        }
    }

    public interface IPlayer
    {
        void Play();
    }
}