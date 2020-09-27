using System;
using System.Diagnostics;
using System.Net;
using System.Net.Cache;
using System.Threading.Tasks;

namespace AsyncDemoApp
{
    class Program
    {
        private static string url = "https://google.com/";

        static void Main()
        {
            // SyncDownload();

            AsyncDownload().Wait();     // Wait() synchronously blocks
            // await AsyncDownload();   // Main cannot be async

            // AsyncDownloadResult();

            // Task<string> t = AsyncDownloadReturnValue();
            // Debug.WriteLine(t.Result);   // Result synchronously blocks
        }

        static void SyncDownload()
        {
            using (var client = new WebClient())
            {
                client.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

                string s = client.DownloadString(url);

                Debug.WriteLine(s);
            }
        }

        static async Task AsyncDownload()
        {
            using (var client = new WebClient())
            {
                client.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

                Task<string> t = client.DownloadStringTaskAsync(url);

                string s = await t.ConfigureAwait(false); // CA2007 to prevent freeze

                // NOTE: await yields control back to the caller Main
                // (therefore return type is Task even though not returning t)
                // and Main immediately terminate and close the console.
                // to reach to string s, Main need to Task.Wait()

                Debug.WriteLine(s);

                // one line version
                // string s2 = await client.DownloadStringTaskAsync(url)
                //     .ConfigureAwait(false);
            }
        }

        static void AsyncDownloadResult()
        {
            using (var client = new WebClient())
            {
                client.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

                Task<string> t = client.DownloadStringTaskAsync(url);

                string s = t.Result;
                // NOTE: Result synchronously blocks

                Debug.WriteLine(s);
            }
        }

        static async Task<string> AsyncDownloadReturnValue()
        {
            using (var client = new WebClient())
            {
                client.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

                Task<string> t = client.DownloadStringTaskAsync(url);

                string result = await t.ConfigureAwait(false);

                return result;
            }
        }
    }
}
