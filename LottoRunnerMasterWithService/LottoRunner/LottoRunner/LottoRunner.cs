using LottoCommon;
using LottoService;
using System;

namespace LottoRunner
{
    class LottoRunner
    {
        static void Main(string[] args)
        {


            Write(Labels.WelcomeMessage);

            LottoTicket userTicket;
            FancyConsole.ConsoleSpiner spinner = new FancyConsole.ConsoleSpiner();

            int runs = 0;

            //The 'do-while' makes the code go into the loop first, without any check, then does the evaluation at the end of the loop.
            do
            {
                runs++;

                userTicket = LottoTicket.GenerateTicket();
                bool isWinner = OfficialLottoService.Current.CheckForWinner(userTicket);

                if (!isWinner) 
                {
                    if (runs % 100000 == 0)
                        spinner.Turn();

                    if (runs % 10000000 == 0)
                        Write(Labels.RunsAndBallSet, FormatInt(runs), userTicket.ToString()); 
                }
                else
                {
                    Write("You won, holy crap!");
                    break;
                }
            } while (true);

            Write(Labels.TotalRuns, FormatInt(runs));
        }

        /// <summary>
        /// Format an int with thousands seperator 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private static string FormatInt(int number)
        {
            //{0:n0} means a thousands format 
            // http://stackoverflow.com/questions/105770/net-string-format-to-add-commas-in-thousands-place-for-a-number 
            return string.Format("{0:n0}", number);
        }

        /// <summary>
        /// Evaluate the results of the lotto, and print victory message if so
        /// </summary>
        /// <param name="result">Result object</param>
        /// <param name="whiteBallsMatchedToWin">How many are required to win</param>
        /// <param name="redBallMustMatchToWin">Is the red ball needed to win</param>
        /// <returns>True if won</returns>
        private static bool CompareAndPrintResults(CompareResult result, int whiteBallsMatchedToWin = Constants.whiteBallLimit, bool redBallMustMatchToWin = true)
        {
            if((int)result.WhiteBallsMatched >= whiteBallsMatchedToWin && ((redBallMustMatchToWin && result.RedBallsMatched) || !redBallMustMatchToWin))
            {
                Write("You won with " + (int)result.WhiteBallsMatched + " white balls, and you " + (result.RedBallsMatched ? "matched" : "didn't match") + " the red ball"); //TODO label
                return true; 
            }

            return false;
        }

        /// <summary>
        /// Get user entered lotto ticket, like 1 2 40 23 4 16
        /// </summary>
        /// <returns>Lotto ticket with the entered numbers</returns>
        /// <exception cref="LottoException">Exception if the user enters bad format</exception>
        private static LottoTicket GetUserEnteredTicket()
        {
            Write(Labels.EnterYourBalls);

            string userText = Console.ReadLine();

            string[] splitText = userText.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            if (splitText.Length != Constants.whiteBallLimit + 1)
                throw new LottoException(Labels.ErrorBadUserInput);

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

        /// <summary>
        /// Write text as a question, with a (y/n) at the end. Enter without anything mean no
        /// </summary>
        /// <param name="text">Question to ask, without (y/n) at the end. That is added later</param>
        /// <returns>Y = true, N or nothing = false</returns>
        public static bool WriteWithConfirmation(string text)
        {
            Write(string.Format(text, YesNoText()));
            string input = Console.ReadLine();

            return input.ToLower().StartsWith(Labels.YesOneLetterLower);
        }

        /// <summary>
        /// Get (y/n) label value
        /// </summary>
        /// <returns>(y/n)</returns>
        private static string YesNoText()
        {
            return string.Format(Labels.YesOrNo, Labels.YesOneLetterLower, Labels.NoOneLetterLower);
        }
    }
}
