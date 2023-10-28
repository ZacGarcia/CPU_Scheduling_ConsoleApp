using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomLinkedList
{
    public class Node
    {
        public string Pnum;
        public int ATime;
        public int BTime;
        public int CTime;   
        public int TATime;
        public int WTime;

        public Node Next;
        
    
       
        public void setData(string p, int at, int bt)
        {
            this.Pnum = p;
            this.ATime = at;
            this.BTime = bt;
            this.CTime = 0;
            this.TATime = 0;
            this.WTime = 0;

            Console.WriteLine("Stored");
        }
        public void UpdateData(string p, int at, int bt, int ct, int tat, int wt)
        {
            this.Pnum = p;
            this.ATime = at;
            this.BTime = bt;
            this.CTime = ct;
            this.TATime = tat;
            this.WTime = wt;
        }
       
    }
}
