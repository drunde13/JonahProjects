namespace LottoCommon
{
    public struct CompareResult
    {
        public WhiteBallsMatched WhiteBallsMatched;
        public bool RedBallsMatched;
    }

    public enum WhiteBallsMatched
    {
        None,
        OneWhite,
        TwoWhite,
        ThreeWhite,
        FourWhite,
        AllWhite,
    }
}
