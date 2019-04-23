﻿using NUnit.Framework;
using System;

namespace DesignPatternTest
{
    /// <summary>
    /// Command Implement and Injection by Client
    /// </summary>
    [TestFixture]
    public class CommandTest
    {
        [Test]
        public void people_array_foreach_count_correct()
        {
            //Instructor Injection Style
            (new ACommand()).execute(new DrawingImpl());

            //Delegate Style , no need ACommand
            Action<DrawingImpl> action = d =>
            {
                d.processAnother();
                d.processOther();
                d.processSome();
            };
            action(new DrawingImpl());
        }
    }

    public interface IDrawing
    {
        void processSome();

        void processOther();

        void processAnother();
    }

    public class DrawingImpl : IDrawing
    {
        public void processSome()
        {
            Console.WriteLine("    - 對圖片作 Some 處理");
        }

        public void processOther()
        {
            Console.WriteLine("    - 對圖片作 Other 處理");
        }

        public void processAnother()
        {
            Console.WriteLine("    - 對圖片作 Another 處理");
        }
    }

    public interface ICommand
    {
        void execute(IDrawing action);
    }

    public class ACommand : ICommand
    {
        public void execute(IDrawing action)
        {
            action.processAnother();
            action.processOther();
            action.processSome();
        }
    }
}