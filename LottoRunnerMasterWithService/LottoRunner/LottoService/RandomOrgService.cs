using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LottoService
{
    public class RandomOrgService
    {
        private const string apikey = "bdbcb26e-7ebd-4d66-9074-e407045909a1";
        private const string urlRandomOrg = "https://api.random.org/json-rpc/1/invoke";

        private int delayInMs = 100; //TODO set later
        private DateTime lastCall;

        private static RandomOrgService current;

        /// <summary>
        /// Get current random org service
        /// </summary>
        public static RandomOrgService Current
        {
            get
            {
                if(current == null)
                {
                    current = new RandomOrgService();
                }
                return current;
            }
        }

        /// <summary>
        /// Get true random integer
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public int GetRandomInt(int min, int max)
        {
            return GetRandomInts(min, max, 1)[0];
        }

        /// <summary>
        /// Get true random integers 
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="howMany"></param>
        /// <returns></returns>
        public int[] GetRandomInts(int min, int max, int howMany)
        {
            if(lastCall != null)
            {
                long timeToWaitTicks = lastCall.AddMilliseconds(delayInMs).Ticks - DateTime.Now.Ticks;

                if (timeToWaitTicks > 0)
                {
                    System.Threading.Thread.Sleep((int)(TimeSpan.TicksPerMillisecond / timeToWaitTicks));
                }
            }

            string requestContent = RequestObject(min, max, howMany);
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] byte1 = encoding.GetBytes(requestContent);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlRandomOrg);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byte1.Length;
            request.Method = "POST";

            using (Stream s = request.GetRequestStream())
                s.Write(byte1, 0, byte1.Length);

            //TODO debug
            //Console.WriteLine("FYI service call happening...");

            WebResponse response = request.GetResponse();

            string reply = null;
            using (Stream st = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(st))
                {
                    reply = reader.ReadToEnd();
                }
            }

            RandomOrgReceive repl = JsonConvert.DeserializeObject<RandomOrgReceive>(reply);

            delayInMs = 100; //TODO deserialization not working for advisory delay. 
            lastCall = DateTime.Now;

            return repl.result.random.data;

            /*
             * { "jsonrpc":"2.0",
             * "result":{ 
             * "random":{ 
             * "data":[45],
             * "completionTime": "2016-04-22 23:35:01Z"},
             * "bitsUsed":6,
             * "bitsLeft":249988,
             * "requestsLeft":998,
             * "advisoryDelay":70},
             * "id":42}
            */
        }


        private static string RequestObject(int min, int max, int howMany)
        {
            return JsonConvert.SerializeObject(new RandomOrgSend()
            {
                jsonrpc = "2.0",
                method = "generateIntegers",
                Params = new RandomOrgParamsSend()
                {
                    apiKey = apikey,
                    n = howMany,
                    min = min,
                    max = max
                }
            });

//            return @"{
//                ""jsonrpc"": ""2.0"",
//    ""method"": ""generateIntegers"",
//    ""params"": {
//                    ""apiKey"": """ + apikey + @""",
//        ""n"": " + howMany + @",
//        ""min"": " + min + @",
//        ""max"": " + max + @",
//        ""replacement"": false
//    },
//    ""id"": 42
//}";
        }
    }
}
