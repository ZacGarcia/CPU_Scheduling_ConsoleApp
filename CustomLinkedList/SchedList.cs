namespace CustomLinkedList
{
    public class SchedList
    {
        public Node head;
        public Node tail;
        public int Count { get; set; }


        public SchedList()
        {
            Count = 0;
            head = null;
            tail = null;

        }


        //ADD NEW LIST
        public void AddNewList(int ATime, int Btime)
        {
            

            if (this.head == null && Count == 0)
            {
                Node newnode = new Node();
                string proc = "P" + Convert.ToString(Count + 1);
                newnode.setData(proc, ATime, Btime);
                this.head = newnode;
                this.tail = newnode;
                newnode.Next = null;

                this.Count++;
            }
            else
            {
                Node newnode = new Node();

                string proc ="P" + Convert.ToString(Count + 1);
                newnode.setData(proc, ATime, Btime);
                this.tail.Next = newnode;
                this.tail = newnode;
                newnode.Next = null;
                this.Count++;
            }


        }

       

       
        //DISPLAY INPUTED DATA TABULAR
        public void DisplayTable()
        {
            Node temp = new Node();
            temp = this.head;
            if (temp != null)
            {
                Console.WriteLine(" \tArrival \tBurst");
                Console.WriteLine("P.No \tTime(AT) \tTime(BT)");

                while (temp != null)
                {
                    Console.WriteLine("{0} \t{1} \t\t{2}", temp.Pnum, temp.ATime, temp.BTime);
                    temp = temp.Next;
                }
            }
            else
            {
                Console.WriteLine("The table is empty!");
            }
        }
    }

}