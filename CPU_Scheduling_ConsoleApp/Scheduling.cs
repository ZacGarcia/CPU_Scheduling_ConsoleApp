using CustomLinkedList;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CPU_Scheduling_ConsoleApp
{
    public class Scheduling
    {
        //SWITCH CURRENT NODE AND NEXT NODE
        public static void SwitchNode(Node point)
        {
            
            Node nxtNode = point.Next;
            //Store current node data to temp var
            string tempP = point.Pnum;
            int tempAT = point.ATime;
            int tempBT = point.BTime;
            int tempCT = point.CTime;
            int tempTAT = point.TATime;
            int tempWT = point.WTime;

            //Transfer next node data to current node
            point.Pnum = nxtNode.Pnum;
            point.ATime = nxtNode.ATime;
            point.BTime = nxtNode.BTime;
            point.CTime = nxtNode.CTime;
            point.TATime = nxtNode.TATime;
            point.WTime = nxtNode.WTime;


            //Transfer current node data from temp var to next node
            nxtNode.Pnum = tempP;
            nxtNode.ATime = tempAT;
            nxtNode.BTime = tempBT;
            nxtNode.CTime = tempCT;
            nxtNode.WTime = tempWT;
            nxtNode.TATime = tempTAT;
        }

        //SORT BY ARRIVAL TIME
        public static void SortByArrival(SchedList l)
        {
            if (l.head != null && l.Count != 0)
            {
                Node node = l.head;
                Node nxtNode = null;
                while (node != null)
                {

                    nxtNode = node.Next;
                    while (nxtNode != null)
                    {
                        if (nxtNode.ATime < node.ATime)
                        {
                            SwitchNode(node);

                            Console.WriteLine("Sorted");
                        }
                        nxtNode = nxtNode.Next;
                    }
                    node = node.Next;
                }
            }
            else
            {
                Console.Write("List is empty");
            }
            PrintTable(l);
        }

        //COMPUTE TIME
        public static void CalculateTime(Node n, Node prev)
        {
            if (prev != null)
            {
                int prevAT = prev.ATime;
                int prevBT = prev.BTime;
                int prevWT = prev.WTime;
                //WAITING TIME
                n.WTime = (prevAT + prevBT + prevWT) - n.ATime;

            }
            else
            {
                //WAITING TIME
                n.WTime = 0;
            }
            //TURN AROUND TIME
            n.TATime = n.BTime + n.WTime;
            //COMPLETION TIME
            n.CTime = n.ATime + n.TATime;
        }

        //PRINT TABLE
        public static void PrintTable(SchedList l)
        {
            Node temp = l.head;
            if (temp != null)
            {
                Console.WriteLine(" \tArrival \tBurst \t\tWaiting \tCompletion \tTurn Around");
                Console.WriteLine("P.No \tTime(AT) \tTime(BT) \tTime(WT) \tTime(CT) \tTime(TAT)");

                while (temp != null)
                {
                    Console.WriteLine("{0} \t{1} \t\t{2} \t\t{3} \t\t{4} \t\t{5}", temp.Pnum, temp.ATime, temp.BTime, temp.WTime, temp.CTime, temp.TATime);
                    temp = temp.Next;
                }
            }
            else
            {
                Console.WriteLine("The table is empty!");
            }
        }
       

    }

    //FIRST COME FIRST SERVE
    public class FirstComeFirstServe : Scheduling 
    {
        public FirstComeFirstServe(SchedList l)
        {
            float totalWT = 0, totalTAT = 0;
            SortByArrival(l);
            Console.WriteLine("Calculated");
            Node node = l.head;
            Node prevnode = null;

            while (node != null)
            {
                CalculateTime(node, prevnode);
                
                totalWT += node.WTime;
                totalTAT += node.TATime;
                prevnode = node;
                node = node.Next;
            }
           
            float averageWT = totalWT / l.Count;
            float averageTAT = totalTAT / l.Count;
            PrintTable(l);
            Console.WriteLine("\nAverage Waiting time is: {0}" ,averageWT );
            Console.WriteLine("\nAverage Turn Around time is: {0}" ,averageTAT );
            

        }

       
    }

    //SHORTEST JOB FIRST NON-PREEMPTIVE
    public class ShortJobFrist_NonPreemptive : Scheduling
    {
        public ShortJobFrist_NonPreemptive(SchedList l)
        {
            Node node = l.head;
            Node prevnode = null;

            float totalWT = 0, totalTAT = 0;
            SortByArrival(l);
            Console.WriteLine("Calculated");
            while (node != null)
            {
                if (node.Next != null)
                {
                    if (node.ATime <= node.Next.ATime && node.BTime > node.Next.BTime)
                    {
                        SwitchNode(node);
                    }
                }
                    
                CalculateTime(node, prevnode);
                totalWT += node.WTime;
                totalTAT += node.TATime;
                prevnode = node;
                node = node.Next;
            }
            float averageWT = totalWT / l.Count;
            float averageTAT = totalTAT / l.Count;
            PrintTable(l);
            Console.WriteLine("\nAverage Waiting time is: {0}ms", averageWT);
            Console.WriteLine("\nAverage Turn Around time is: {0}ms", averageTAT);
        }
    }

    //LONGEST JOB FIRST NON-PREEMPTIVE
    public class LongestJobFirst_NonPreemptive : Scheduling
    {
        public LongestJobFirst_NonPreemptive(SchedList l)
        {
            Node node = l.head;
            Node prevnode = null;

            float totalWT = 0, totalTAT = 0;
            SortByArrival(l);
            Console.WriteLine("Calculated");
            while (node != null)
            {
                if (node.Next != null)
                {
                    if (node.ATime <= node.Next.ATime && node.BTime < node.Next.BTime)
                    {
                        SwitchNode(node);
                    }
                }

                CalculateTime(node, prevnode);
                totalWT += node.WTime;
                totalTAT += node.TATime;
                prevnode = node;
                node = node.Next;
            }
            float averageWT = totalWT / l.Count;
            float averageTAT = totalTAT / l.Count;
            PrintTable(l);
            Console.WriteLine("\nAverage Waiting time is: {0}ms", averageWT);
            Console.WriteLine("\nAverage Turn Around time is: {0}ms", averageTAT);
        }
    }
   
}
