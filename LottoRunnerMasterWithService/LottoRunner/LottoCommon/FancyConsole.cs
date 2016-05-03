using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LottoCommon
{
    /// <summary>
    /// This doesn't have anything to do with the rest of the code :) 
    /// Super dirty 
    /// </summary>
    public class FancyConsole
    {
        public class ConsoleSpiner
        {
            int counter;
            public ConsoleSpiner()
            {
                counter = 0;
            }
            public void Turn()
            {
                counter++;
                switch (counter % 4)
                {
                    case 0: Console.Write("/"); break;
                    case 1: Console.Write("-"); break;
                    case 2: Console.Write("\\"); break;
                    case 3: Console.Write("|"); break;
                }
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            }
        }

        public class PullBalls
        {
            const int delay = 250;

            const int startCol = 6;
            const int startRow = 4;
            const string ball = "*";
            const string JAR = 
@"


   |     | 
  /       \
 /*** ** * \
|* *** **** |    <- these are supposed to be lotto balls :) 
| *** * ** *|
\___________/
";

            int leftPosition;
            int topPosition;
            public void Go()
            {
                Console.Write(JAR);

                Thread.Sleep(1000);

                leftPosition = Console.CursorLeft;
                topPosition = Console.CursorTop;

                MoveBall(1);
                MoveBall(2);
                MoveBall(3);
                MoveBall(4);
                MoveBall(5);
                MoveBall(6);
                MoveBall(7);

                Console.SetCursorPosition(leftPosition, topPosition);
                Console.WriteLine();
            }

            private void MoveBall(int whichBall)
            {
                Console.SetCursorPosition(Console.CursorLeft - 1 + startCol, Console.CursorTop - startRow);

                MoveBall(1, 0);
                MoveBall(1, 0);
                MoveBall(1, 1);
                MoveBall(1, 2);
                MoveBall(0, 2 - whichBall * 2);

                Console.SetCursorPosition(leftPosition, topPosition);
            }

            private void MoveBall(int howUp, int howLeft)
            {
                Console.Write(".");
                Console.SetCursorPosition(Console.CursorLeft - 1 - howLeft, Console.CursorTop - howUp);
                Console.Write(ball);
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                Thread.Sleep(delay);
            }
        }
    }
}
