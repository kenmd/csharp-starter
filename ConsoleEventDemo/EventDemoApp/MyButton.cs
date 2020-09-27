using System;

namespace EventDemoApp
{
    public class MyButton
    {
        // public delegate void ClickHandler(object sender, string msg);
        // public event ClickHandler ClickCallback;

        // or same thing
        // public event Action<object, string> ClickCallback;

        // C# 2.0 have system defined EventHandler and EventArgs
        public event EventHandler<MyButtonEventArgs> ClickCallback;

        public void DummyClick()
        {
            if (ClickCallback != null)
            {
                ClickCallback(this, MyButtonEventArgs.of("Clicked!"));
            }
        }
    }

    public class MyButtonEventArgs : EventArgs
    {
        public string Message { get; set; }

        public static MyButtonEventArgs of(string message)
        {
            return new MyButtonEventArgs() { Message = message };
        }
    }
}
