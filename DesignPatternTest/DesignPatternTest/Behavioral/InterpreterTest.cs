using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternTest
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    public class InterpreterTest
    {
        [Test]
        public void Expression()
        {
            // 待解譯文字
            Context context = new Context();
            context.Text = "b2002 a2002 b2013 a2013";

            // 解譯器
            Expression expression = new ExpressionA();
            expression.Interpret(context);

        }

    }

    // 解譯器抽像類別
    abstract class Expression
    {
        public void Interpret(Context context)
        {
            if (context.Text.Length > 0)
            {
                var words = context.Text.Split(' ');
                
                foreach (var word in words)
                {
                    int num = (Convert.ToInt32(word.Substring(1)) + 9) % 12;
                    if (0 == num)
                    {
                        num = 12;
                    }
                    ((word.Substring(0, 1) == "a") ? (Expression)new ExpressionA() : new ExpressionB()).Excute(num);
                }
            }
        }

        public abstract void Excute(int num);
    }

    // 生肖解譯器
    internal class ExpressionA  : Expression
    {
        public override void Excute(int num)
        {
            string res = "";
            switch (num)
            {
                case 1:
                    res = "鼠";
                    break;
                case 2:
                    res = "牛";
                    break;
                case 3:
                    res = "虎";
                    break;
                case 4:
                    res = "兔";
                    break;
                case 5:
                    res = "龍";
                    break;
                case 6:
                    res = "蛇";
                    break;
                case 7:
                    res = "馬";
                    break;
                case 8:
                    res = "羊";
                    break;
                case 9:
                    res = "猴";
                    break;
                case 10:
                    res = "雞";
                    break;
                case 11:
                    res = "狗";
                    break;
                case 12:
                    res = "豬";
                    break;
            }

            Console.Write(res);
        }
    }

    // 地支解譯器
    internal class ExpressionB : Expression
    {
        public override void Excute(int num)
        {
            string res = "";
            switch (num)
            {
                case 1:
                    res = "子";
                    break;
                case 2:
                    res = "丑";
                    break;
                case 3:
                    res = "寅";
                    break;
                case 4:
                    res = "卯";
                    break;
                case 5:
                    res = "辰";
                    break;
                case 6:
                    res = "巳";
                    break;
                case 7:
                    res = "午";
                    break;
                case 8:
                    res = "未";
                    break;
                case 9:
                    res = "申";
                    break;
                case 10:
                    res = "酉";
                    break;
                case 11:
                    res = "戌";
                    break;
                case 12:
                    res = "亥";
                    break;
            }

            Console.Write(res);
        }
    }

    // 存放待解譯資料
    class Context
    {
        private string text;
        public string Text
        {
            get { return text; }
            set { text = value; }
        }
    }
}