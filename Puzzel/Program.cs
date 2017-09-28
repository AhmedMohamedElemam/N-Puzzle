using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Puzzel
{
    class Program
    {
        //*************************************************************************
        static public Boolean check_solvable(int[] puzzel1d, int N, int blank_i)
        {

            int num_of_Inversions = 0;
            int index = blank_i + 1; // blank index from up
            int _index;// blank index from down
            for (int i = 0; i < N * N - 1; i++)
            {
                for (int j = i + 1; j < N * N; j++)
                {
                    if (puzzel1d[i] > puzzel1d[j] && puzzel1d[j] != 0)
                        num_of_Inversions++;
                }
            }
            _index = N - index + 1;// index of blank space from down
            if ((N % 2 != 0 && num_of_Inversions % 2 == 0) || (N % 2 == 0 && num_of_Inversions % 2 != 0 && _index % 2 == 0) || (N % 2 == 0 && num_of_Inversions % 2 == 0 && _index % 2 != 0))
                return true;
            else return false;

        }
        //*************************************************************************
        static int blank_i;
        static int blank_j;

        static void Main(string[] args)
        {
            // Read Board From File
            FileStream fs = new FileStream("8 Puzzle (1).txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            while (sr.Peek() != -1)
            {
                String s = sr.ReadLine();
                String[] fields;
                fields = s.Split(' ');
                int N;
                N = int.Parse(fields[0]);
                int val;
                int[] puzzel1d = new int[N * N];
                int[,] puzzel2d = new int[N, N];
                int counter = 0;
                for (int i = 0; i < N; i++)
                {
                    s = sr.ReadLine();
                    fields = s.Split(' ');
                    for (int j = 0; j < N; j++)
                    {
                        val = int.Parse(fields[j]);
                        if (val == 0) { blank_i = i; blank_j = j; }
                        puzzel2d[i, j] = val;
                        puzzel1d[counter] = val;
                        counter++;
                    }
                }
                //*****************************************************

                char ch;
                Console.WriteLine(" - Press [1] To Using A_Star With Hamming .");
                Console.WriteLine(" - Press [2] To Using A_Star With Manhattan .");
                Console.Write(" - Enter Your Choice : ");
                ch = char.Parse(Console.ReadLine());

                switch (ch)
                {
                    //-----------------------------------------------------------------------------------
                    case '1':
                        {
                            // Check If Board Is Solvable Or Not 
                            if (check_solvable(puzzel1d, N, blank_i))
                            {
                                Console.WriteLine();
                                Console.WriteLine(" - The Given Board Is < Solvable >");
                                Console.WriteLine("    __________________________     ");
                                Console.WriteLine();
                                puzzel start = new puzzel(N, puzzel2d, blank_i, blank_j);
                                A_Star_Using_Hamming A = new A_Star_Using_Hamming();
                                A.A_Star_Algorithm(start);
                                A.display_path();
                            }
                            else
                                Console.WriteLine("No Feasible Solution For The Given Board ");

                        }
                        break;
                    //-----------------------------------------------------------------------------------
                    case '2':
                        {

                            // Check If Board Is Solvable Or Not 
                            if (check_solvable(puzzel1d, N, blank_i))
                            {
                                Console.WriteLine();
                                Console.WriteLine(" - The Given Board Is < Solvable >");
                                Console.WriteLine("    __________________________     ");
                                Console.WriteLine();
                                puzzel start = new puzzel(N, puzzel2d, blank_i, blank_j);
                                A_Star_Using_Manhattan A = new A_Star_Using_Manhattan();
                                A.A_Star_Algorithm(start);
                            }
                            else
                                Console.WriteLine(" - No Feasible Solution For The Given Board ");

                        }
                        break;
                    //-----------------------------------------------------------------------------------                        
                    default:
                        break;
                    //-----------------------------------------------------------------------------------                

                }
            }
        }
    }
}
