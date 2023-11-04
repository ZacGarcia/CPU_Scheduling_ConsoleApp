using CustomLinkedList;

namespace CPU_Scheduling_ConsoleApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            SchedList list = new SchedList();
            int at, bt, numOfInputs;
            bool schedFlag = true;

            Console.WriteLine("Input number of inputs: ");
            numOfInputs = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < numOfInputs; i++)
            {
                Console.Write("Input Arrival time: ");
                at = Convert.ToInt32(Console.ReadLine());
                Console.Write("Input Burst time: ");
                bt = Convert.ToInt32(Console.ReadLine());
                list.AddNewList(at, bt);
            }
            list.DisplayTable();
            
            while (schedFlag)
            {
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("0. Exit");
                Console.WriteLine("1. First Come, First Served");
                Console.WriteLine("2. Shortest Job First Non-Preemptive");
                Console.WriteLine("3. Longest Job Frist Non-Preemptive");
                Console.WriteLine("4. Priority");
                Console.WriteLine("5. Round Robin");
                Console.WriteLine("--------------------------------------");
                Console.Write("Select an option: ");


                char choice = Convert.ToChar(Console.ReadLine());


                switch (choice)
                {
                    case '1':
                        _ = new FirstComeFirstServe(list);
                        break;
                    case '2':
                        ShortJobFrist_NonPreemptive s = new ShortJobFrist_NonPreemptive(list);
                        break;
                    case '3':
                        LongestJobFirst_NonPreemptive l = new LongestJobFirst_NonPreemptive(list);
                        break;
                    case '4':
                        _ = new Priorty(list);
                        break;
                    case '5':
                       
                        break;
                    case '0':
                        schedFlag = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Input!");
                        break;
                }


            }

        }

    }
}