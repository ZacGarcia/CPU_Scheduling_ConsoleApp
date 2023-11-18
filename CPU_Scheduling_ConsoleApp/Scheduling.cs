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
        //SWITCH CURRENT NODE AND NEXT NODE FUNCTION
        public static void SwitchNode(Node point)
        {

            Node nxtNode = point.Next;
            //Store current node data to temp var
            string tempP = point.Pnum;
            int tempAT = point.ATime;
            int tempBT = point.BTime;
            int tempPrio = point.Priority;

            //Transfer next node data to current node
            point.Pnum = nxtNode.Pnum;
            point.ATime = nxtNode.ATime;
            point.BTime = nxtNode.BTime;
            point.Priority = nxtNode.Priority;

            //Transfer current node data from temp var to next node
            nxtNode.Pnum = tempP;
            nxtNode.ATime = tempAT;
            nxtNode.BTime = tempBT;
            nxtNode.Priority = tempPrio;
        }

        //SORT BY BURST TIME FUNCTION
        public static void SortByBurstTime(Node node)
        {
            if (node != null)
            {

                Node nxtNode = null;
                while (node != null)
                {

                    node.ResetData(node);
                    nxtNode = node.Next;
                    while (nxtNode != null)
                    {
                        if (nxtNode.BTime < node.BTime)
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
            Console.WriteLine("Sorted by burst time");
        }

        //SORT BY PRIOTIY FUNCTION
        public static void SortByPriority(Node node)
        {
            if (node != null )
            {
                
                Node nxtNode = null;
                while (node != null)
                {

                    
                    nxtNode = node.Next;
                    while (nxtNode != null)
                    {
                        if (nxtNode.Priority < node.Priority)
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
            Console.WriteLine("Sorted by priority");
            
        }

        //SORT BY ARRIVAL TIME FUNCTION
        public static void SortByArrival(Node node)
        {
            if (node != null)
            {
                
                Node nxtNode = null;
                while (node != null)
                {

                    node.ResetData(node);
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
            Console.WriteLine("Sorted by arrival");
        }

        //COMPUTE TIME FUNCTION
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

        //PRINT TABLE FUNCTION
        public static void PrintTable(SchedList l)
        {
            Node temp = l.head;
            if (temp != null)
            {
                Console.WriteLine(" \tArrival \tBurst \t\tWaiting \tCompletion \tTurn Around \t");
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
        public static void PrintTable2(SchedList l)
        {
            Node temp = l.head;
            if (temp != null)
            {
                Console.WriteLine(" \tArrival \t\t\tBurst \t\tWaiting \tCompletion \tTurn Around \tResponse");
                Console.WriteLine("P.No \tTime(AT) \tPriority \tTime(BT) \tTime(WT) \tTime(CT) \tTime(TAT) \tTime(RT)");

                while (temp != null)
                {
                    Console.WriteLine("{0} \t{1} \t\t{2} \t\t{3} \t\t{4} \t\t{5} \t\t{6} \t\t{7}", temp.Pnum, temp.ATime, temp.Priority, temp.BTime, temp.WTime, temp.CTime, temp.TATime, temp.RTime);
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
            
            Console.WriteLine("Calculated");
            Node node = l.head;
            Node prevnode = null;
            SortByArrival(node);
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
            SortByArrival(node);
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
            SortByArrival(node);
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

    //PRIORITY FIRST
    public class Priority : Scheduling
    {
        public Priority(SchedList p)
        {
            Node node = p.head;

            //INPUT PRIORITY NUMBER
            while (node != null)
            {
                //RESET DATA(WT,TAT,RT,CT,prevPRIORITY)
                node.ResetData(node);
                Console.Write("Input priority number for {0}:" , node.Pnum);
                node.Priority = Convert.ToInt32(Console.ReadLine());
                node = node.Next;
            }
            CalculatePrioTime(p);
        }

        public void CalculatePrioTime(SchedList l)
        {
            float totalWT = 0, totalTAT = 0;

            
            Console.WriteLine("Calculated");
            Node node = l.head;
            Node prevnode = null;
            int tempBtime = node.BTime;
            SortByPriority(node);
            while (node != null)
            {
                //WAITING TIME
                if (prevnode != null)
                {
                    int prevAT = prevnode.ATime;
                    int prevBT = prevnode.BTime;
                    int prevWT = prevnode.WTime;

                    if (prevBT > node.ATime)
                    {
                        tempBtime = tempBtime - node.ATime;
                    }
                    node.WTime = tempBtime;
                    tempBtime = tempBtime + node.BTime;

                    //RESPONSE TIME
                    if (node.ATime > prevnode.ATime)
                    {
                        node.RTime = node.WTime;
                    }

                }
                else
                {
                    node.WTime = 0;
                }

                

                //TURN AROUND TIME
                node.TATime = node.BTime + node.WTime;
                //COMPLETION TIME
                node.CTime = node.ATime + node.TATime;

                totalWT += node.WTime;
                totalTAT += node.TATime;
                prevnode = node;
                node = node.Next;
            }

            //AVERAGE WAITING TIME
            float averageWT = totalWT / l.Count;
            //AVERAGE TURN AROUND TIME
            float averageTAT = totalTAT / l.Count;

            //PRINT TABLE
            PrintTable2(l);
            Console.WriteLine("\nAverage Waiting time is: {0}", averageWT);
            Console.WriteLine("\nAverage Turn Around time is: {0}", averageTAT);
            
        }
    }

    //ROUND ROBIN
    public class RoundRobin : Scheduling
    {
        private int qTime;

        public RoundRobin(SchedList r)
        {
            Node node = r.head;
            Node prevnode = null;
            float totalWT = 0, totalTAT = 0;
            int existTime = 0;
            int remB, qoutB;

            //INPUT TIME QUANTUM
            Console.WriteLine("Input Time Quantum: ");
            qTime = Convert.ToInt32(Console.Read());

            Console.WriteLine("Calculated");
            
            //SORT BY BURST TIME
            SortByBurstTime(node);
            while (node != null)
            {
                //COMPUTE COMPLETE TIME
                if (node.BTime <= qTime)
                {
                    node.CTime = (node.ATime * qTime) + node.BTime;
                    existTime = existTime + (qTime - node.BTime);
                }
                else
                {
                    qoutB = node.BTime / qTime;
                    remB = node.BTime % qTime;
                    node.CTime = (((r.Count * qTime) * qoutB) - existTime) + remB;
                }

                //COMPUTE TURN AROUND TIME AND WAITINGT TIME
                node.TATime = node.CTime - node.ATime;
                node.WTime = node.TATime - node.BTime;

                //COMPUTE TOTAL
                totalWT += node.WTime;
                totalTAT += node.TATime;
                prevnode = node;
                node = node.Next;
            }
            //COMPUTE FOR THE AVERAGE
            float averageWT = totalWT / r.Count;
            float averageTAT = totalTAT / r.Count;
            PrintTable(r);
            Console.WriteLine("\nAverage Waiting time is: {0}ms", averageWT);
            Console.WriteLine("\nAverage Turn Around time is: {0}ms", averageTAT);

        }
    }
   
}
