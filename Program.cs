using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIeze_Hanoi
{
    /*
     * All comments added later, they may not proper describe methods. 
     * Make an issue if you have found a bug.
     */
    class Program
    {
        static void Main(string[] args)
        {
            // Starts Hanoi Towers.
            Hanoi.MainTask();
        }
    }

    /// <summary>
    /// Additional class to call method again if repeat is needed.
    /// </summary>
    public class Hanoi
    {
        /// <summary>
        /// Body containing hanoi tower methods and algorithms.
        /// </summary>
        public static void MainTask()
        {
            try
            {
                Console.WriteLine("Program rozwiązujący problem znany jako problem Wieży Hanoi dla [n] klocków na 3 wieżach.");
                int n,x;
                Stack<string> stackA = new Stack<string>();
                Stack<string> stackB = new Stack<string>();
                Stack<string> stackC = new Stack<string>();
                Stack<string> copyStack = new Stack<string>();

                Console.Write("\nPodaj dla ilu klocków chcesz wykonać algorytm: ");
                n = Convert.ToInt32(Console.ReadLine());
                if (n == 1)
                {
                    Console.WriteLine("Jedna wieża to nie problem ;)");
                    ReplyTask();
                }
                else if (n < 1)
                {
                    Console.WriteLine("Podałeś zły zakres.");
                    ReplyTask();
                }
                else
                {
                    Console.Write("Podaj prędkość wyświetlania w milisekundach [1000 ms = 1 s]: ");
                    int t = Convert.ToInt32(Console.ReadLine());
                    if (t < 0)
                    {
                        Console.WriteLine("Czas się nie cofnie... :(");
                    }
                    else
                    {
                        x = n;
                        string[] arrayA = new string[n + 1];
                        string[] arrayB = new string[n + 1];
                        string[] arrayC = new string[n + 1];
                        arrayA[n] = EmptyList(n, "A");
                        arrayB[n] = EmptyList(n, "B");
                        arrayC[n] = EmptyList(n, "C");

                        GenerateBlocks(n, stackA);

                        EmptyList(n, copyStack);

                        stackA.CopyTo(arrayA, 0);
                        copyStack.CopyTo(arrayB, 0);
                        copyStack.CopyTo(arrayC, 0);

                        Console.Clear();
                        for (int j = 0; j < x + 1; j++)
                        {
                            Console.Write("\n{0}\t\t{1}\t\t{2}", arrayA[j], arrayB[j], arrayC[j]);
                        }
                        System.Threading.Thread.Sleep(1000);
                        Console.Clear();

                        HanoiMove(n, stackA, stackB, stackC, arrayA, arrayB, arrayC, copyStack, x, t);

                        int max = stackA.Count >= stackB.Count ? (stackA.Count >= stackC.Count ? stackA.Count : (stackC.Count >= stackB.Count ? stackC.Count : stackB.Count)) : stackB.Count >= stackC.Count ? stackB.Count : stackC.Count;
                        for (int j = 0; j < x+1; j++)
                        {
                            Console.Write("\n{0}\t\t{1}\t\t{2}", arrayA[j], arrayB[j], arrayC[j]);
                        }

                        ReplyTask();
                    }                  
                }
            }
            catch (FormatException fEx) //wyjątek gdy wpisane będą litery
            {
                //Console.WriteLine(fEx.Message);
                Console.WriteLine("Podany znak nie jest cyfrą!");
                Console.ReadLine();
                ReplyTask();
            }
            catch (OverflowException OverEx) //gdy wartość podana w konsoli będzie poza zakresem liczbowym wartości typu int
            {
                //Console.WriteLine(OverEx.Message);
                Console.WriteLine("Podana liczba jest z poza zakresu!");
                Console.ReadLine();
                ReplyTask();
            }
            catch (ArithmeticException ArgEx) //wyjątek przy dzieleniu przez 0
            {
                //Console.WriteLine(ArgEx.Message);
                Console.WriteLine("Nie dzielimy przez 0!");
                Console.ReadLine();
                ReplyTask();
            }
            catch (Exception Ex) //inne wyjątki
            {
                Console.WriteLine("Coś poszło nie tak");
                Console.ReadLine();
                ReplyTask();
            }
        }

        /// <summary>
        /// Move block on block with higher radius.
        /// </summary>
        /// <param name="n">Number of blocks.</param>
        /// <param name="stackA">First stack</param>
        /// <param name="stackB">Second stack</param>
        /// <param name="stackC">Third stack</param>
        /// <param name="arrayA">Additional array to display towers.</param>
        /// <param name="arrayB">Additional array to display towers.</param>
        /// <param name="arrayC">Additional array to display towers.</param>
        /// <param name="copyStack">Copy of the stack.</param>
        /// <param name="x"></param>
        /// <param name="t"></param>
        public static void HanoiMove(int n, Stack<string> stackA, Stack<string> stackB, Stack<string> stackC, string[] arrayA, string[] arrayB, string[] arrayC, Stack<string> copyStack, int x, int t)
        {
            if (n > 0)
            {
                HanoiMove(n - 1, stackA, stackC, stackB, arrayA, arrayB,arrayC,copyStack, x, t);

                Console.Clear();
                for (int j = 0; j < x + 1; j++)
                {
                    Console.Write("\n{0}\t\t{1}\t\t{2}", arrayA[j], arrayB[j], arrayC[j]);
                }

                System.Threading.Thread.Sleep(t);
                Console.Clear();

                stackC.Push(stackA.Pop()); // A -> C

                copyStack.CopyTo(arrayA, 0);
                copyStack.CopyTo(arrayB, 0);
                copyStack.CopyTo(arrayC, 0);
                stackA.CopyTo(arrayA, arrayA.Length - stackA.Count -1);
                stackB.CopyTo(arrayB, arrayB.Length - stackB.Count-1);
                stackC.CopyTo(arrayC, arrayC.Length - stackC.Count-1);
                HanoiMove(n - 1, stackB, stackA, stackC, arrayA, arrayB, arrayC,copyStack, x, t);
            } 
        }

        /// <summary>
        /// Generate display of towers.
        /// </summary>
        /// <param name="n">Number of blocks.</param>
        /// <param name="stackA">For which stack is display generated.</param>
        public static void GenerateBlocks(int n, Stack<string> stackA)
        {
            string block = "█";
            string blockLong = "█";
            string blockResult = "";
            Stack<string> helpingStack = new Stack<string>();
            for (int i = 0; i < n; i++)
            {
                blockLong = block + blockLong + block;
                blockResult = GenerateEmpty(n-i-1)+ ""+(i+1)+"" + blockLong + GenerateEmpty(n-i);
                helpingStack.Push(blockResult);
            }
            for (int i = 0; i<n;i++)
            {
                stackA.Push(helpingStack.Pop());
            }
        }
        /// <summary>
        /// Generates empty rows.
        /// </summary>
        /// <param name="i">Number of rows</param>
        /// <returns>String with empty rows</returns>
        public static string GenerateEmpty(int i)
        {
            string empty = " ";
            string emptyLong = "";
            for (int j = 0; j<i; j++)
            {
                emptyLong = emptyLong + empty;
            }
            return emptyLong;
        }
        /// <summary>
        /// Generates empty list.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="stack"></param>
        public static void EmptyList(int n, Stack<string> stack)
        {
            string block = " ";
            string blockLong = "|";
            string blockResult = "";
            for (int i = 0; i < n; i++)
            {
                blockLong = block + blockLong + block;
                blockResult = GenerateEmpty(n - i) + blockLong + GenerateEmpty(n - i);
                stack.Push(blockResult);
            }
        }
        /// <summary>
        /// Generates empty list.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="letter"></param>
        /// <returns></returns>
        public static string EmptyList(int n, string letter)
        {
            string block = " ";
            string blockLong = letter;
            string blockResult = "";
            for (int i = 0; i < n; i++)
            {
                blockLong = block + blockLong + block;
                blockResult = GenerateEmpty(n - i) + blockLong + GenerateEmpty(n - i);
            }
            return blockResult;
        }

        /// <summary>
        /// Horizontal Line.
        /// </summary>
        public static void HorizontalLine()
        {
            Console.WriteLine("\n————————————————————————————————————————————");
        }

        /// <summary>
        /// Method asks user if task should be finished or repeat it.
        /// </summary>
        public static void ReplyTask()
        {
            char answer = 'x';
            HorizontalLine();
            Console.WriteLine("\nCzy chcesz powótrzyć wykonywanie zadania? [T/N]");
            answer = Convert.ToChar(Console.Read());
            switch (answer)
            {
                case 'n':
                case 'N':
                    HorizontalLine();
                    Console.WriteLine("Koniec programu. Naciśnij dowolny przycisk..");
                    Console.Read();
                    Environment.Exit(0);
                    break;
                case 't':
                case 'T':
                    Console.WriteLine("Poczekaj chwilę..");
                    Console.ReadLine();
                    Console.Clear();
                    MainTask();
                    break;
                default:
                    HorizontalLine();
                    Console.WriteLine("Wprowadzono błędną odpowiedź.");
                    Console.WriteLine("Poczekaj chwilę..");
                    Console.ReadLine();
                    ReplyTask();
                    break;
            }
        }
    }
}
