using LottoCommon;
using System;

namespace LottoRunner
{
    class LottoRunner
    {
        static void Main(string[] args)
        {
            Write(Labels.WelcomeMessage);

            // Init the user's ticket inside the if statements. 
            LottoTicket userTicket = GetUserEnteredTicket();

            Write(Labels.YourNumbers, userTicket.ToString());
        }

        /// <summary>
        /// Get user entered lotto ticket, like 1 2 40 23 4 16
        /// </summary>
        /// <returns>Lotto ticket with the entered numbers</returns>
        /// <exception cref="LottoException">Exception if the user enters bad format</exception>
        private static LottoTicket GetUserEnteredTicket()
        {
            Write(Labels.EnterYourBalls, 
                Constants.minWhiteBall.ToString(), 
                Constants.maxWhiteBall.ToString(), 
                Constants.minRedBall.ToString(), 
                Constants.maxRedBall.ToString());

            string userText = Console.ReadLine();

            string[] splitText = userText.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            if (splitText.Length != Constants.whiteBallLimit + 1)
                throw new Exception(Labels.ErrorBadUserInput);

            return new LottoTicket(new int[]
            {
                int.Parse(splitText[0]),
                int.Parse(splitText[1]),
                int.Parse(splitText[2]),
                int.Parse(splitText[3]),
                int.Parse(splitText[4])
            },
            int.Parse(splitText[5]));
        }

        /// <summary>
        /// Write the text to the console 
        /// </summary>
        /// <param name="text"></param>
        public static void Write(string text)
        {
            Console.WriteLine(text);
        }

        /// <summary>
        /// Write the text to the console, with parameters to dynamically replace. E.g. if the label says "My text {0}", the {0} gets replaced with some other passed in text (like "is this")
        /// so in the end it'll says "My text is this"
        /// </summary>
        /// <param name="text"></param>
        /// <param name="parms"></param>
        public static void Write(string text, params string[] parms)
        {
            Write(string.Format(text, parms));
        }
    }
}
