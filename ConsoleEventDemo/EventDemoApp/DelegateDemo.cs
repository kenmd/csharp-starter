using System;

namespace EventDemoApp
{
    public class DelegateDemo
    {
        // C# 2.0
        // public delegate void f1Delegate();
        // public f1Delegate f1;

        // C# 3.0 (no need to define delegate type)
        public Action f1 { get; set; }
        public Action<int, int> f2 { get; set; }
        public Func<int, int, int> f3 { get; set; }

        public void Test()
        {
            if (f1 != null) { f1(); }
            if (f2 != null) { f2(1, 2); }
            if (f3 != null) { Console.WriteLine(f3(3, 4)); }
        }
    }
}
