using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Diagnostics.Eventing.Reader;
using static System.Net.WebRequestMethods;
using System.Runtime.InteropServices;

namespace Trade
{
    internal class Daily
    {
        private static HttpClient client = null;
        public Daily()
        {
            client = null;
        }

        public void ConsoleCancelEventHandler()
        {
        }

        public async Task runAsync()
        {
            Console.Clear();
            Console.Write("Please enter 'From Date' (XXXX-XX-XX) : ");
            string fromDate = Console.ReadLine();

            Console.Write("Please enter 'To Date' (XXXX-XX-XX) : ");
            string toDate = Console.ReadLine();

            string symbol = Info.getSymbol()[0];
            int segmentIndex = int.Parse(Info.getSegment()[0, 0].ToString());
            string segmentName = "";

            if (segmentIndex == 0) segmentName = "IDX_I";
            else if (segmentIndex == 1) segmentName = "NSE_EQ";
            else if (segmentIndex == 2) segmentName = "NSE_FNO";
            else if (segmentIndex == 3) segmentName = "NSE_CURRENCY";
            else if (segmentIndex == 4) segmentName = "BSE_EQ";
            else if (segmentIndex == 5) segmentName = "MCX_COMM";
            else if (segmentIndex == 7) segmentName = "BSE_CURRENCY";
            else if (segmentIndex == 8) segmentName = "BSE_FNO";

            string instrument = Info.getInstrument();

            var client = new HttpClient();
                
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(Info.getHttpUrl() + "historical"),
                Headers =
                {
                    { "access-token", Info.getToken() },
                    { "Accept", "application/json" },
                },
                Content = new StringContent($"{{\n  \"symbol\": \"{symbol}\",\n  \"exchangeSegment\": \"{segmentName}\",\n  \"instrument\": \"{instrument}\",\n  \"expiryCode\": 0,\n  \"fromDate\": \"{fromDate}\",\n  \"toDate\": \"{toDate}\"\n}}")
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/json")
                    }
                }
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
            }
        }

        public void getMsg()
        {
        }
    }
}
