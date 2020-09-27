using System;

namespace EventDemoApp
{
    class Program
    {
        static void Main()
        {
            DemoDelegate();
            DemoEvent();
        }

        static void DemoDelegate()
        {
            DelegateDemo c1 = new DelegateDemo();

            // C# 1.0 (delegate is type)
            // c1.f1 = new DelegateDemo.f1Delegate(a);

            c1.f1 = a;              // C# 2.0 (simplified)
            c1.f1 += b;             // multi cast
            c1.f1 += delegate ()    // anonymous methods
            {
                Console.WriteLine("Hello, I'm anonymous.");
            };

            c1.f2 = delegate (int x, int y) { Console.WriteLine($"{x} {y}"); };
            c1.f3 = delegate (int x, int y) { return x + y; };

            c1.Test();
        }

        static void a()
        {
            Console.WriteLine("I'm a().");
        }

        static void b()
        {
            Console.WriteLine("I'm b().");
        }

        static void DemoEvent()
        {
            MyButton button = new MyButton();

            // NOTE: event is special type of delegate as [1] [2]

            // [1] event always use +=/-= (not =)
            button.ClickCallback += Button_Click1;

            // [2] event cannot call from outside
            // button.ClickCallback(null, "hello");
            button.DummyClick();
        }

        static void Button_Click1(object sender, MyButtonEventArgs e)
        {
            Console.WriteLine($"{sender.ToString()} {e.Message}");
        }
    }
}
