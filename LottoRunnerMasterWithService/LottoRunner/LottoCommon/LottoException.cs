using System;

namespace LottoCommon
{
    public class LottoException : Exception
    {
        public LottoException(string exceptionText) : base(exceptionText) { }
    }
}
