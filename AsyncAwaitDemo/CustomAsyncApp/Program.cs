using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace CustomAsyncApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var custom = new CustomAsync();

            custom.ButtonClick(null, EventArgs.Empty).Wait();

            // assume click twice and both run in parallel
            var task = custom.BarButtonClick(null, EventArgs.Empty);
            var task2 = custom.BarButtonClick(null, EventArgs.Empty);

            Task.WaitAll(task, task2);  // WaitAll synchronously blocks
        }
    }

    class CustomAsync
    {
        public async Task ButtonClick(object sender, EventArgs e)
        {
            Task t = FooAsync();

            Debug.WriteLine("Await Start");
            await t;
            Debug.WriteLine("Await End");
        }

        public async Task BarButtonClick(object sender, EventArgs e)
        {
            Task<int> t = BarAsync();

            Debug.WriteLine("Await Start 2");
            int r = await t;
            Debug.WriteLine($"Await End 2: {r}");
        }

        private Task FooAsync()
        {
            return Task.Run(() =>
            {
                Debug.WriteLine("Sleep Start in Foo");
                Thread.Sleep(5000);
                Debug.WriteLine("Sleep End in Foo");
            });
        }

        private Task<int> BarAsync()
        {
            return Task.Run(() =>
            {
                Debug.WriteLine("Sleep Start in Bar");
                Thread.Sleep(5000);
                Debug.WriteLine("Sleep End in Bar");
                return 123;
            });
        }
    }
}
