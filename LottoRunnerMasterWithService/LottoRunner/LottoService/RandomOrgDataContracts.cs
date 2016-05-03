using System;
using System.Runtime.Serialization;

namespace LottoService
{
    [DataContract]
    internal class RandomOrgReceive
    {
        [DataMember]
        public string jsonrpc { get; set; }

        [DataMember]
        public RandomOrgResult result { get; set; }

    }

    internal class RandomOrgResult
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public Rand random { get; set; }
    }

    [DataContract]
    internal class Rand
    {
        [DataMember]
        public int[] data { get; set; }

        [DataMember]
        public DateTime completionTime { get; set; } //: "2016-04-22 23:35:01Z"},

        [DataMember]
        public int bitsUsed { get; set; } //":6,

        [DataMember]
        public int bitsLeft { get; set; } //":249988,
        [DataMember]
        public int requestsLeft { get; set; } //":998,
        [DataMember]
        public int advisoryDelay { get; set; }//":70
    }

    [DataContract]
    internal class RandomOrgSend
    {
        [DataMember]
        public string jsonrpc { get; set; }

        [DataMember]
        public string method { get; set; }

        [DataMember(Name = "params")]
        public RandomOrgParamsSend Params { get; set; }

        [DataMember]
        public int id { get; set; }
    }

    [DataContract]
    internal class RandomOrgParamsSend
    {
        [DataMember]
        public string apiKey { get; set; }

        [DataMember]
        public int n { get; set; }

        [DataMember]
        public int min { get; set; }

        [DataMember]
        public int max { get; set; }

        [DataMember]
        public bool replacement { get; set; }
    }

}
