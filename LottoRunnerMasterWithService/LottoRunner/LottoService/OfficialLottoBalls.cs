using LottoCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoService
{
    /// <summary>
    /// Official lotto ball set
    /// </summary>
    internal class OfficialLottoBalls : LottoBallSet
    {
        /// <summary>
        /// How long is the official ticket lifetime
        /// </summary>
        const int expirationTimeInMinutes = 10;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="whiteBallSet"></param>
        /// <param name="redBall"></param>
        public OfficialLottoBalls(int[] whiteBallSet, int redBall)
            : base(whiteBallSet, redBall)
        {
            expiration = DateTime.Now.AddMinutes(expirationTimeInMinutes);
        }

        private DateTime expiration;

        /// <summary>
        /// Lotto ticket expiration
        /// </summary>
        public DateTime ExpirationDateTime
        {
            get
            {
                return expiration;
            }
        }
    }
}
