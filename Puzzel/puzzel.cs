using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzel
{
    class puzzel
    {
        private int N;
        public int[,] puzzel2d;
        private int num_of_level;
        private int blank_index_i;
        private int blank_index_j;
        private int hamming_val;
        private int manhattan_val;
        public String direction;
        public int cost;
        public puzzel parent;
        public String key="";
        //***************************************************************
        //initial constructor 
        public puzzel(int size, int[,] puzzel_array, int blank_tile_i, int blank_tile_j)
        {
            this.N = size;
            this.puzzel2d = new int[N, N];
            for (int i = 0; i < this.N; i++)
            {
                for (int j = 0; j < this.N; j++)
                {

                    this.puzzel2d[i, j] = puzzel_array[i, j];
                    this.key += puzzel_array[i, j];
                }
            }
            this.direction = "Start";
            this.blank_index_i = blank_tile_i;
            this.blank_index_j = blank_tile_j;
            this.num_of_level = 0;
            this.hamming();
            this.manhattan();
            this.parent = null;
        }
        //*******************************************************************
        //child constructor 
        public puzzel(puzzel p)
        {
            this.N = p.N;
            this.puzzel2d = new int[N, N];
            for (int i = 0; i < this.N; i++)
            {
                for (int j = 0; j < this.N; j++)
                {

                    this.puzzel2d[i, j] = p.puzzel2d[i, j];
                }
            }
            this.blank_index_i = p.blank_index_i;
            this.blank_index_j = p.blank_index_j;
            this.num_of_level = p.num_of_level + 1;
            this.parent = p.parent;

        }
        //*******************************************************************
        //Equal constructor 
        public puzzel(puzzel p, int _default)
        {
            this.N = p.N;
            this.puzzel2d = new int[N, N];
            for (int i = 0; i < this.N; i++)
            {
                for (int j = 0; j < this.N; j++)
                {

                    this.puzzel2d[i, j] = p.puzzel2d[i, j];
                }
            }
            this.blank_index_i = p.blank_index_i;
            this.blank_index_j = p.blank_index_j;
            this.num_of_level = p.num_of_level;
            this.hamming_val = p.hamming_val;
            this.manhattan_val = p.manhattan_val;
            this.cost = p.cost;
            this.parent = p;
            this.key = p.key;

        }

        //*******************************************************************
        //calc min cost using hamming 
        public void calc_min_cost_using_hamming()
        {
            this.cost = this.num_of_level + this.hamming_val;
        }
        //*******************************************************************
        //calc min cost using manhattan 
        public void calc_min_cost_using_manhattan()
        {
            this.cost = this.num_of_level + this.manhattan_val;
        }
        //*******************************************************************
        public puzzel move_left()
        {


            this.puzzel2d[this.blank_index_i, this.blank_index_j] = this.puzzel2d[this.blank_index_i, this.blank_index_j - 1];
            this.puzzel2d[this.blank_index_i, this.blank_index_j - 1] = 0;
            //catch blank_tile
            this.blank_index_j = this.blank_index_j - 1;
            return this;
        }
        public puzzel move_right()
        {
            this.puzzel2d[this.blank_index_i, this.blank_index_j] = this.puzzel2d[this.blank_index_i, this.blank_index_j + 1];
            this.puzzel2d[this.blank_index_i, this.blank_index_j + 1] = 0;
            //catch blank_tile
            this.blank_index_j = this.blank_index_j + 1;

            return this;
        }

        public puzzel move_down()
        {

            this.puzzel2d[this.blank_index_i, this.blank_index_j] = this.puzzel2d[this.blank_index_i + 1, this.blank_index_j];
            this.puzzel2d[this.blank_index_i + 1, this.blank_index_j] = 0;
            //catch blank_tile
            this.blank_index_i = this.blank_index_i + 1;
            return this;

        }
        public puzzel move_up()
        {

            this.puzzel2d[this.blank_index_i, this.blank_index_j] = this.puzzel2d[this.blank_index_i - 1, this.blank_index_j];
            this.puzzel2d[this.blank_index_i - 1, this.blank_index_j] = 0;
            //catch blank_tile
            this.blank_index_i = this.blank_index_i - 1;
            return this;

        }

        public bool check_left()
        {
            if (this.blank_index_j != 0)
                return true;
            return false;
        }
        public bool check_right()
        {
            if (this.blank_index_j != this.N - 1)
                return true;
            return false;

        }
        public bool check_up()
        {
            if (this.blank_index_i != 0)
                return true;
            return false;
        }
        public bool check_down()
        {
            if (this.blank_index_i != this.N - 1)
                return true;
            return false;
        }
        //********************************************************************
        public void display()
        {
            for (int i = 0; i < this.N; i++)
            {
                for (int j = 0; j < this.N; j++)
                {
                    Console.Write(this.puzzel2d[i, j]);
                    Console.Write(" ");
                }
                if (i == 1) { Console.WriteLine(" ---> " + this.direction); }
                else { Console.WriteLine(); }
            }
            Console.WriteLine();
        }

        //**********************************************************************
        public void hamming()
        {
            int counter = 1;
            int miss_place = 0;
            for (int i = 0; i < this.N; i++)
            {
                for (int j = 0; j < this.N; j++)
                {
                    this.key += this.puzzel2d[i, j];
                    if (this.puzzel2d[i, j] != counter && this.puzzel2d[i, j] != 0) { miss_place++; }
                    counter++;
                }
            }

            this.hamming_val = miss_place;
        }
        //**********************************************************************
        public void manhattan()
        {
            int count = 0;
            int expected = 0;

            for (int row = 0; row < this.N; row++)
            {

                for (int col = 0; col < this.N; col++)
                {
                    this.key += this.puzzel2d[row, col];
                    int value = this.puzzel2d[row, col];
                    expected++;

                    if (value != 0 && value != expected)
                    {

                        count += Math.Abs(row - ((value - 1) / this.N))
                                + Math.Abs(col - ((value - 1) % this.N));
                    }
                }
            }

            this.manhattan_val = count;
        }
        //**********************************************************************
        public Boolean is_goal_H()
        {
            if (this.hamming_val == 0) return true;
            else return false;
        }
        //**********************************************************************
        public Boolean is_goal_M()
        {
            if (this.manhattan_val == 0) return true;
            else return false;
        }
        //**********************************************************************
    }
}
