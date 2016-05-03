using LottoCommon;
using System;
using System.Text;
namespace LottoService
{
    /// <summary>
    /// User lotto ticket
    /// </summary>
    public class LottoTicket : LottoBallSet
    {
        /// <summary>
        /// Auto generate a ticket
        /// </summary>
        /// <returns></returns>
        public static LottoTicket GenerateTicket()
        {
            return OfficialLottoService.DrawRandomUserTicket();
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="whiteBallSet"></param>
        /// <param name="redBall"></param>
        public LottoTicket(int[] whiteBallSet, int redBall)
            : base(whiteBallSet, redBall)
        { }
    }
}
