using LottoCommon;
using System;
using System.Collections.Generic;

namespace LottoService
{
    public class LottoBallSpinner
    {
        private static Random rand; 
        private static int GetRandomNumber(int min, int max)
        {
            // TODO use random.org
            if (rand == null)
                rand = new Random();

            return rand.Next(min, max + 1);
        }

        public static LottoCommon.LottoTicket DrawBalls()
        {
            List<int> ballsAreadyDrawn = new List<int>();

            for(int i = Constants.minWhiteBall; i <= Constants.whiteBallLimit; i++)
            {
                int potentailBall = GetRandomNumber(max: Constants.maxWhiteBall, min: Constants.minWhiteBall);

                if (ballsAreadyDrawn.Contains(potentailBall))
                {
                    // Bug left on purpose if this is removed 
                    i--;
                    continue;
                }

                ballsAreadyDrawn.Add(potentailBall);
            }

            int redBall = GetRandomNumber(Constants.minRedBall, Constants.maxRedBall);

            var newBallSet = new LottoTicket(ballsAreadyDrawn.ToArray(), redBall);

            return newBallSet;
        }
    }
}
