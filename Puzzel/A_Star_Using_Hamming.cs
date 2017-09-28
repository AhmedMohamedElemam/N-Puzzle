using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzel
{
    class A_Star_Using_Hamming
    {
        public List<puzzel> path = new List<puzzel>();
        public List<puzzel> closed_list = new List<puzzel>();
        public PriorityQueue open_list = new PriorityQueue();
        public Dictionary<string, puzzel> map_open = new Dictionary<string, puzzel>();
        public Dictionary<string, puzzel> map_closed = new Dictionary<string, puzzel>();

        //*******************************************************
        public void A_Star_Algorithm(puzzel start)
        {
            map_open.Add(start.key,start);
            open_list.Enqueue(start);
            while (!open_list.is_empty())
            {
                puzzel current = new puzzel(open_list.Dequeue(), 0);
                if (!check_if_child_in_closed(current))
                {
                    map_closed.Add(current.key,current);
                    closed_list.Add(current);
                    // generate child from current
                    generate_child(current);
                }
            }

        }
        //*******************************************************
        public void generate_child(puzzel p)
        {
            // check L||R||U||D     
            if (p.check_left())
            {
                puzzel c = new puzzel(p);
                c.move_left();
                c.hamming();
                c.calc_min_cost_using_hamming();
                if (c.is_goal_H())
                {
                    c.direction = "Goal";
                    // Add Goal Board
                    path.Add(c);
                    Get_Path(c);
                }
                c.direction = "Left";
                //check if child in open list or not
                Boolean boo2;
                boo2 = check_if_child_in_open(c);
                if (boo2 == false)
                {
                    open_list.Enqueue(c); map_open.Add(c.key,c);
                }

            }
            if (p.check_right())
            {
                puzzel c = new puzzel(p);
                c.move_right();
                c.hamming();
                c.calc_min_cost_using_hamming();
                if (c.is_goal_H())
                {
                    c.direction = "Goal";
                    // Add Goal Board
                    path.Add(c);
                    Get_Path(c);
                }
                c.direction = "Right";
                //check if child in open list or not
                Boolean boo2;
                boo2 = check_if_child_in_open(c);
                if (boo2 == false) { open_list.Enqueue(c); map_open.Add(c.key, c); }

            }
            if (p.check_up())
            {
                puzzel c = new puzzel(p);
                c.move_up();
                c.hamming();
                c.calc_min_cost_using_hamming();
                if (c.is_goal_H())
                {
                    c.direction = "Goal";
                    // Add Goal Board
                    path.Add(c);
                    Get_Path(c);
                }
                c.direction = "Up";
                //check if child in open list or not
                Boolean boo2;
                boo2 = check_if_child_in_open(c);
                if (boo2 == false) { open_list.Enqueue(c); map_open.Add(c.key, c); }

            }
            if (p.check_down())
            {
                puzzel c = new puzzel(p);
                c.move_down();
                c.hamming();
                c.calc_min_cost_using_hamming();
                if (c.is_goal_H())
                {
                    c.direction = "Goal";
                    // Add Goal Board
                    path.Add(c);
                    Get_Path(c);
                }
                c.direction = "Down";
                //check if child in open list or not
                Boolean boo2;
                boo2 = check_if_child_in_open(c);
                if (boo2 == false) { open_list.Enqueue(c); map_open.Add(c.key, c); }

            }
        }
        //**********************************************
        public Boolean check_if_child_in_closed(puzzel c)
        {
            bool check = false;
            if (map_closed.ContainsKey(c.key))
            {
                puzzel _key = map_closed[c.key];
                if (_key.cost < c.cost) { open_list.Enqueue(_key); map_open.Add(_key.key, _key); }
                check = true;
            }
            return check;
        }
        //**********************************************
        public Boolean check_if_child_in_open(puzzel c)
        {
            bool check = map_open.ContainsKey(c.key);
         
            return check;
        }

        //**************************************************
        public void display_path()
        {

            int n = path.Count();
            int Min = n - 1;
            for (int i = n - 1; i >= 0; i--)
                path[i].display();
            Console.WriteLine("    __________________________     ");
            Console.WriteLine();
            Console.Write(" - # of Movements = " + Min);
            Console.WriteLine();
            Console.WriteLine("    __________________________     ");
            Console.WriteLine();
            Console.WriteLine(" - Open List Size : < " + open_list.size()+" >");
            Console.WriteLine(" - Closed List Size : < " + closed_list.Count()+" >");
            Console.WriteLine("    __________________________     ");
            Console.WriteLine();
            Environment.Exit(0);


        }
        //**************************************************
        public void Get_Path(puzzel goal)
        {
            // Get Path
            puzzel p = goal.parent;
            while (p.parent != null)
            {

                path.Add(p);
                p = p.parent;
            }
            // Add Start Board
            path.Add(p);
             // call Display path
            display_path();
        
        }
        //*****************************************************

    }
}
