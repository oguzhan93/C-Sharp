using System;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            info();
        }
        static void info()
        {
            Console.WriteLine("[1] Single player");
            Console.WriteLine("[2] Multiplayer");
            Console.WriteLine("[0] Exit");
            int number = Convert.ToInt32(Console.ReadLine());
            if (number == 1)
                singlePlayer();
            else if (number == 2)
                multiplayer();
        }
        static void multiplayer()
        {
            char[,] arr = new char[3, 3];
            while (true)
            {
                print(arr);
                Console.WriteLine("X's turn!");
                userMove(arr, 'X');
                if (whoWins(arr) != -1)
                    break;
                print(arr);
                Console.WriteLine("O's turn!");
                userMove(arr, 'O');
                if (whoWins(arr) != -1)
                    break;
            }
            print(arr);
            int result = whoWins(arr);
            if (result == 1)
                Console.WriteLine("X Win!");
            else if (result == 2)
                Console.WriteLine("O Win!");
            else
                Console.WriteLine("Draw!");
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("Directing main menu!");
            System.Threading.Thread.Sleep(1500);
            Console.Clear();
            info();
        }
        static void wait(char[,] arr)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write("Thinking");
                for (int i = 0; i < 3; i++)
                {
                    Console.Write(".");
                    System.Threading.Thread.Sleep(150);
                }
                Console.Clear();
                print(arr);
            }
            
        }
        static void singlePlayer()
        {
            char[,] arr = new char[3, 3];
            while (true)
            {
                print(arr);
                userMove(arr, 'X');
                if (whoWins(arr) != -1)
                    break;
                print(arr);
                wait(arr);
                computerMove(arr);
                if (whoWins(arr) != -1)
                    break;
            }
            print(arr);
            int result = whoWins(arr);
            if (result == 1)
                Console.WriteLine("You Win!");
            else if (result == 2)
                Console.WriteLine("Computer Wins!");
            else
                Console.WriteLine("Draw!");
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("Directing main menu!");
            System.Threading.Thread.Sleep(1500);
            Console.Clear();
            info();
        }
        static bool isFull(char[,] arr)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (arr[i, j] == '\0')
                        return false;
                }
            }
            return true;
        }
        static void print(char[,] arr)
        {
            Console.Clear();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (arr[i, j] != '\0')
                        Console.Write("| " + arr[i, j]+" ");
                    else
                        Console.Write("|   ");
                }
                Console.Write("|");
                Console.WriteLine();
            }
        }
        static void userMove(char[,] arr, char letter)
        {
            int move = -1;
            while (true)
            {
                int number = Convert.ToInt32(Console.ReadLine());
                move = number;
                if (move > 0 && move < 10)
                    break;
                Console.WriteLine("Please enter 1-9 number!");
            }
            putUser(arr, move, letter);
        }
        static void putUser(char[,] arr, int move, char letter)
        {
            int temp = 1;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (temp == move)
                        if (arr[i, j] == '\0')
                            arr[i, j] = letter;
                        else
                        {
                            Console.WriteLine("This cell is already full");
                            userMove(arr, letter);
                        }
                    temp++;
                }
            }
        }
        static void computerMove(char[,] arr)
        {
            if (isFull(arr))
                return;
            else if (!bestMove(arr, 'O'))
            {
                if (!bestMove(arr, 'X'))
                {
                    if (!adjacentmove(arr))
                    {
                        if (arr[1, 1] == '\0')
                            arr[1, 1] = 'O';
                        else
                        {
                            bool move = false;
                            while (!move)
                            {
                                Random random = new Random();
                                int i = random.Next(3);
                                int j = random.Next(3);
                                if (arr[i, j] == '\0')
                                {
                                    arr[i, j] = 'O';
                                    move = true;
                                }
                            }
                        }
                    }
                }
            }
        }
        static bool put(char[,] arr, int i, int j)
        {
            bool result = false;
            if (i - 1 >= 0 && arr[i - 1, j] == '\0')
            {
                arr[i - 1, j] = 'O';
                return true;
            }
            else if (i + 1 < 3 && arr[i + 1, j] == '\0')
            {
                arr[i + 1, j] = 'O';
                return true;
            }
            else if (j - 1 >= 0 && arr[i, j - 1] == '\0')
            {
                arr[i, j - 1] = 'O';
                return true;
            }
            else if (j + 1 < 3 && arr[i, j + 1] == '\0')
            {
                arr[i, j + 1] = 'O';
                return true;
            }
            else if (i + 1 < 3 && j + 1 < 3 && arr[i + 1, j + 1] == '\0')
            {
                arr[i + 1, j + 1] = 'O';
                return true;
            }
            else if (i - 1 >= 0 && j - 1 >= 0 && arr[i - 1, j - 1] == '\0')
            {
                arr[i - 1, j - 1] = 'O';
                return true;
            }
            return result;
        }
        static bool adjacentmove(char[,] arr)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (arr[i, j] == 'O')
                    {
                        if (!put(arr, i, j))
                            continue;
                        else
                            return true;
                    }
                }
            }
            return false;
        }
        static bool bestMove(char[,] arr, char parameter)
        {
            for (int i = 0; i < 3; i++)
            {
                int space1 = 0, counter1 = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (arr[i, j] == parameter)
                        counter1++;
                    else if (arr[i, j] == '\0')
                        space1++;

                }
                if (space1 == 1 && counter1 == 2)
                {
                    for (int index = 0; index < 3; index++)
                    {
                        if (arr[i, index] == '\0')
                        {
                            arr[i, index] = 'O';
                            return true;
                        }
                    }
                }

            }
            for (int x = 0; x < 3; x++)
            {
                int counter2 = 0, space2 = 0;
                for (int y = 0; y < 3; y++)
                {
                    if (arr[y, x] == parameter)
                        counter2++;
                    else if (arr[y, x] == '\0')
                        space2++;
                }
                if (space2 == 1 && counter2 == 2)
                {
                    for (int index = 0; index < 3; index++)
                    {
                        if (arr[index, x] == '\0')
                        {
                            arr[index, x] = 'O';
                            return true;
                        }
                    }
                }
            }
            int counter = 0, space = 0;
            if (arr[0, 0] == '\0')
                space++;
            if (arr[0, 0] == parameter)
                counter++;
            if (arr[1, 1] == '\0')
                space++;
            if (arr[1, 1] == parameter)
                counter++;
            if (arr[2, 2] == '\0')
                space++;
            if (arr[2, 2] == parameter)
                counter++;
            if (counter == 2 && space == 1)
            {
                if (arr[0, 0] == '\0')
                    arr[0, 0] = 'O';
                else if (arr[1, 1] == '\0')
                    arr[1, 1] = 'O';
                else
                    arr[2, 2] = 'O';
                return true;
            }
            space = 0;
            counter = 0;
            if (arr[0, 2] == '\0')
                space++;
            if (arr[0, 2] == parameter)
                counter++;
            if (arr[1, 1] == '\0')
                space++;
            if (arr[1, 1] == parameter)
                counter++;
            if (arr[2, 0] == '\0')
                space++;
            if (arr[2, 0] == parameter)
                counter++;
            if (counter == 2 && space == 1)
            {
                if (arr[0, 2] == '\0')
                    arr[0, 2] = 'O';
                else if (arr[1, 1] == '\0')
                    arr[1, 1] = 'O';
                else
                    arr[2, 0] = 'O';
                return true;
            }
            return false;
        }
        static int whoWins(char[,] arr)
        {
            for (int i = 0; i < 3; i++)
            {
                if (arr[i, 0] != '\0' && arr[i, 0] == arr[i, 1] && arr[i, 0] == arr[i, 2])
                {
                    if (arr[i, 0] == 'X')
                        return 1;
                    else
                        return 2;
                }
                if (arr[0, i] != '\0' && arr[0, i] == arr[1, i] && arr[0, i] == arr[2, i])
                {
                    if (arr[0, i] == 'X')
                        return 1;
                    else
                        return 2;
                }
            }
            if (arr[0, 0] != '\0' && arr[0, 0] == arr[1, 1] && arr[2, 2] == arr[0, 0])
            {
                if (arr[0, 0] == 'X')
                    return 1;
                else
                    return 2;
            }
            if (arr[0, 2] != '\0' && arr[0, 2] == arr[1, 1] && arr[0, 2] == arr[2, 0])
            {
                if (arr[0, 2] == 'X')
                    return 1;
                else
                    return 2;
            }
            if (isFull(arr))
                return 3;
            else
                return -1;
        }
    }
}
