using LottoCommon;
using System;
using System.Collections.Generic;

namespace LottoService
{
    public class OfficialLottoService
    {
        #region Private Variables 
        private OfficialLottoBalls officalBallSet;
        private Random rand;
        private static OfficialLottoService current;
        #endregion

        #region Public 

        /// <summary>
        /// Current Official lotto service
        /// </summary>
        public static OfficialLottoService Current
        {
            get
            {
                if (current == null)
                {
                    current = new OfficialLottoService();
                }
                return current;
            }
        }

        /// <summary>
        /// Check if the given lotto ticket is a winner 
        /// </summary>
        /// <param name="usersBalls"></param>
        /// <returns></returns>
        public bool CheckForWinner(LottoBallSet usersBalls)
        {
            if (officalBallSet == null || officalBallSet.ExpirationDateTime <= DateTime.Now)
            {
                Write("FYI Offical lotto ticket expired ... new ticket generated");

                FancyConsole.PullBalls pull = new FancyConsole.PullBalls();
                pull.Go();

                officalBallSet = Current.DrawOfficialBallSet();
            }

            return officalBallSet == usersBalls;
        }

        #endregion 

        #region Internal

        /// <summary>
        /// Generate a simple random ticket
        /// </summary>
        /// <returns></returns>
        internal static LottoTicket DrawRandomUserTicket()
        {
            var ballz = Current.DrawBalls(false);
            return new LottoTicket(ballz.white, ballz.red);
        }

        /// <summary>
        /// Draw official ticket 
        /// </summary>
        /// <returns></returns>
        internal OfficialLottoBalls DrawOfficialBallSet()
        {
            var ballz = DrawBalls(true);
            return new OfficialLottoBalls(ballz.white, ballz.red);
        }

        #endregion

        #region Private

        /// <summary>
        /// Make the Ctor private so that no one else can call it. Someone who wants to use this service must use 'Current' 
        /// </summary>
        private OfficialLottoService() { }

        private Random Rand
        {
            get
            {
                if (rand == null)
                    return rand = new Random();

                return rand;
            }
        }

        private int GetRandomNumber(bool isRandomService, int min, int max)
        {
            if (isRandomService) 
                return RandomOrgService.Current.GetRandomInt(min, max);
            else 
                return Rand.Next(min, max + 1);
        }

        /// <summary>
        /// Structure used for Draw Balls return 
        /// </summary>
        private struct TempBallSet
        {
            public int[] white;
            public int red;
        }

        /// <summary>
        /// Draw random ball set. 
        /// </summary>
        /// <param name="useRandomService">true = use true random service, false = use pseudo random service</param>
        /// <returns>Temp ball set</returns>
        private TempBallSet DrawBalls(bool useRandomService)
        {
            List<int> ballsAreadyDrawn = new List<int>();

            for(int i = Constants.minWhiteBall; i <= Constants.whiteBallLimit; i++)
            {
                int potentailBall = GetRandomNumber(
                    isRandomService: useRandomService, 
                    max: Constants.maxWhiteBall, 
                    min: Constants.minWhiteBall);

                if (ballsAreadyDrawn.Contains(potentailBall))
                {
                    // Bug left on purpose if this is removed 
                    i--;
                    continue;
                }

                ballsAreadyDrawn.Add(potentailBall);
            }

            int redBall = GetRandomNumber(useRandomService, Constants.minRedBall, Constants.maxRedBall);

            return new TempBallSet() { red = redBall, white = ballsAreadyDrawn.ToArray() };
        }

        /// <summary>
        /// Temp writter method
        /// </summary>
        /// <param name="text"></param>
        private static void Write(string text)
        {
            try
            {
                Console.WriteLine(text);
            }
            catch (Exception) { }
        }

        #endregion
    }
}
