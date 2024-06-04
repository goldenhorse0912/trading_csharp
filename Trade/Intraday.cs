using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocket4Net;

namespace Trade
{
    internal class Intraday
    {
        HttpClient client = null;
        public Intraday()
        {
            client = null;
        }

        public void ConsoleCancelEventHandler()
        {
        }

        public async Task runAsync()
        {
            string securityId = Info.getSegment()[0, 1].ToString();

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
                RequestUri = new Uri(Info.getHttpUrl() + "intraday"),
                Headers =
                {
                    { "access-token", Info.getToken()},
                    { "Accept", "application/json" },
                },
                Content = new StringContent($"{{\n  \"securityId\": \"{securityId}\",\n  \"exchangeSegment\": \"{segmentName}\",\n  \"instrument\": \"{instrument}\"\n}}")
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
    }
}
