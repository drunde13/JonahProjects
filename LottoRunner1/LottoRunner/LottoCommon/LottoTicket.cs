using System.Collections.Generic;
using System.Linq;

namespace LottoCommon
{
    public class LottoTicket
    {
        private int[] whiteBalls;
        private int redBall;

        private bool BallCheckerAndStorage(int[] whiteBallArray, int redBall)
        {
            if (whiteBallArray.Length != Constants.whiteBallLimit)
            {
                throw new System.Exception(Labels.ErrorNotEnoughBallsWhite);
            }

            List<int> whiteBallList = whiteBallArray.ToList();
            whiteBallList.Sort();

            for (int i = 0; i < Constants.whiteBallLimit; i++)
            {
                int potentialWhiteBall = whiteBallList[i];

                if (potentialWhiteBall < Constants.minWhiteBall || potentialWhiteBall > Constants.maxWhiteBall)
                    throw new System.Exception(string.Format(Labels.ErrorBallOutOfRange, Constants.minWhiteBall, Constants.maxWhiteBall));

                if (whiteBalls.Contains(potentialWhiteBall))
                    throw new System.Exception(Labels.ErrorDuplicateBallDetected);

                whiteBalls[i] = potentialWhiteBall;
            }

            if (redBall < Constants.minRedBall || redBall > Constants.maxRedBall)
                throw new System.Exception(string.Format(Labels.ErrorBallOutOfRange, Constants.minRedBall, Constants.maxRedBall));

            this.redBall = redBall;

            // All checks passed 
            return true;
        }

        public int WhiteBall1 { get { return whiteBalls[0]; } }
        public int WhiteBall2 { get { return whiteBalls[1]; } }
        public int WhiteBall3 { get { return whiteBalls[2]; } }
        public int WhiteBall4 { get { return whiteBalls[3]; } }
        public int WhiteBall5 { get { return whiteBalls[4]; } }
        public int RedBall { get { return redBall; } }

        public LottoTicket(int[] whiteBalls, int redBall)
        {
            this.whiteBalls = new int[Constants.whiteBallLimit];

            BallCheckerAndStorage(whiteBalls, redBall);
        }

        public override string ToString()
        {
            return string.Format(Labels.BallSet,
                BallNumberWithExtraSpace(this.WhiteBall1),
                BallNumberWithExtraSpace(this.WhiteBall2),
                BallNumberWithExtraSpace(this.WhiteBall3),
                BallNumberWithExtraSpace(this.WhiteBall4),
                BallNumberWithExtraSpace(this.WhiteBall5),
                BallNumberWithExtraSpace(this.RedBall));
        }

        private string BallNumberWithExtraSpace(int ballNumber)
        {
            return ballNumber < 10 ? ballNumber + " " : ballNumber.ToString();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            LottoTicket passedSet = (LottoTicket)obj;

            return passedSet.WhiteBall1 == this.WhiteBall1 &&
                passedSet.WhiteBall2 == this.WhiteBall2 &&
                passedSet.WhiteBall3 == this.WhiteBall3 &&
                passedSet.WhiteBall4 == this.WhiteBall4 &&
                passedSet.WhiteBall5 == this.WhiteBall5 &&
                passedSet.RedBall == this.RedBall;
        }

        public static bool operator ==(LottoTicket a, LottoTicket b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(LottoTicket a, LottoTicket b)
        {
            return !a.Equals(b);
        }
    }
}
