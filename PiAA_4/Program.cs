using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace PiAA_4
{
    class Program
    {
        class LST
        {
            public Node head = null;
            public LST(int data)
            {
                head = new Node(data);
            }

            public void add(int i)
            {
                if(head == null)
                    head = new Node(i);
                else
                {
                    Node currentNode = head;
                    while (currentNode.next != null)
                    {
                        currentNode = currentNode.next;
                    }
                    currentNode.next = new Node(i);
                }
            }
        }
        class Node
        {
            public Node next = null;
            public int data;
            public Node(int val)
            {
                data = val;
            }
        }
        class UFF
        {
            public int[] mainsElems;
            public LST[] lsts;
            public UFF(int N)
            {
                lsts = new LST[N];
                mainsElems = new int[N];
                for (int i = 0; i < N; i++)
                {
                    mainsElems[i] = i;
                    lsts[i] = new LST(i);
                }
            }

            public void merge(int x, int y)
            {
                int prX = mainsElems[x], prY = mainsElems[y];
                LST lstFirst = lsts[prX];
                LST lstSecond = lsts[prY];
                Node currentNode = lstFirst.head;
                while (currentNode != null)
                {
                    int i = currentNode.data;
                    mainsElems[i] = prY;
                    lstSecond.add(i);
                    currentNode = currentNode.next;
                }

                lsts[prX] = null;
            }

            public bool check(int x, int y)
            {
                if (mainsElems[x] == mainsElems[y])
                    return true;
                else
                    return false;
            }

        }
        static void Main(string[] args)
        {
            FileStream inFileStream = new FileStream("./"+args[0],FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(inFileStream);
            FileStream outFileStream = new FileStream("./programm.out", FileMode.Open, FileAccess.Write);
            outFileStream.SetLength(0);
            StreamWriter writer = new StreamWriter(outFileStream);
            string[] inVar = reader.ReadLine().Split(' ');

            int N = int.Parse(inVar[0]);
            int M = int.Parse(inVar[1]);

            UFF uff = new UFF(N);
            for (int i = 0; i < M; i++)
            {
                string[] data = reader.ReadLine().Split(' ');
                if (!uff.check(int.Parse(data[0]), int.Parse(data[1])))
                {
                    writer.Write("NO"+"\n");
                    Console.Write("NO ");
                    uff.merge(int.Parse(data[0]), int.Parse(data[1]));
                }
                else
                {
                    Console.Write("YES ");
                    writer.Write("YES" + "\n");
                }
            }

            Console.ReadKey();
            reader.Close();
            writer.Close();
        }
    }
}
