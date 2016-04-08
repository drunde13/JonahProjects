using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LottoService
{
    public class RandomOrgService
    {
        private const string apikey = "bdbcb26e-7ebd-4d66-9074-e407045909a1";
        private const string urlRandomOrg = "https://api.random.org/json-rpc/1/invoke";

        public static int GetRandomInt()
        {
            WebRequest request = HttpWebRequest.Create(urlRandomOrg);

            throw new NotImplementedException("this is not implimented ... yet");

           // return 0;
        }
    }
}
