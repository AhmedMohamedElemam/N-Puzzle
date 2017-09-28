using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzel
{
    class PriorityQueue
    {
        //********************************************************************
        public List<puzzel> data;
        //********************************************************************
      
        public PriorityQueue()
        {
            this.data = new List<puzzel>();
        }
        //********************************************************************
      
        public void Enqueue(puzzel item)
        {
            data.Add(item);
            heapifyup();
         
        }
        //********************************************************************
        public void heapifyup()
        {
            int ci = data.Count - 1; // child index; start at end
            while (ci > 0)
            {
                int pi = (ci - 1) / 2; // parent index

                // If child item is larger than (or equal) parent so we're done
                if (data[ci].cost.CompareTo(data[pi].cost) >= 0) break;
                // Else swap parent & child 
                puzzel tmp = data[ci]; data[ci] = data[pi]; data[pi] = tmp;
                ci = pi;
            }
        }
        //********************************************************************
        public puzzel Dequeue()
        {
            // assumes pq is not empty; up to calling code
            int li = data.Count - 1; // last index (before removal)
            puzzel frontItem = data[0];   // fetch the front
            data[0] = data[li]; // last item be the root
            data.RemoveAt(li);
            heapifydown();
            return frontItem;
        }
        //********************************************************************
        public void heapifydown()
        {
            int li = data.Count - 1; //last index (after removal)

            int pi = 0; // parent index. start at front of pq
            while (true)
            {
                int ci = pi * 2 + 1; // left child index of parent
                if (ci > li) break;  // no children so done
                int rc = ci + 1;     // right child
                // if there is a right child , and it is smaller than left child, use the rc instead
                if (rc <= li && data[rc].cost.CompareTo(data[ci].cost) < 0)
                    ci = rc;
                // If parent is smaller than (or equal to) smallest child so done
                if (data[pi].cost.CompareTo(data[ci].cost) <= 0) break;
                // Else swap parent and child
                puzzel tmp = data[pi]; data[pi] = data[ci]; data[ci] = tmp;
                pi = ci;
            }        
        }
        //********************************************************************
        public int size()
        {
            return data.Count;
        }
        //********************************************************************
        public Boolean is_empty()
        {
            if (data.Count == 0) return true;
            else return false;
        }
        //********************************************************************

    }
}
