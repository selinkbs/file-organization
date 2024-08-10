using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CENG307_191180005_HW1
{
    class Program
    {
        static Random rnd = new Random();
        static List<int> Datas = new List<int>();

        static REISCH reisch = new REISCH();

        static void Main(string[] args)
        {


            add();

            int controlProcessing = -1;
            bool loopProcessing = true;
            while (loopProcessing)
            {
                Console.Write("\nExit: -1\nPrint: 0 \nSearch data: 1 \nProbes Required: 2 \nClear Screen: 3 \nSelect the processing: ");
                controlProcessing = Convert.ToInt32(Console.ReadLine());
                switch (controlProcessing)
                {
                    case -1:
                        loopProcessing = false;
                        break;
                    case 0:
                        print();
                        break;
                    case 1:
                        search();
                        break;
                    case 2:
                        calculateProbesRequired();
                        break;
                    case 3:
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Invalid entry");
                        break;
                }
            }


            Console.ReadKey();
        }
        static void add()
        {
            reisch.addAllBlankIndexes();

            for (int i = 0; i < 900; i++)
            {
                int data = rnd.Next(0, 1000000);
                Datas.Add(data);
                reisch.add(data);
            }
        }
        static void print()
        {
            int controlPrint = -1;
            Console.Write("\n\nREISCH: 0 \nSelect the processing:");
            controlPrint = Convert.ToInt32(Console.ReadLine());
            switch (controlPrint)
            {
                case 0:
                    reisch.print();
                    break;

                default:
                    Console.WriteLine("Invalid entry");
                    break;
            }

        }
        static void search()
        {
            int read = 0;
            int controlSearch = -1;

            Console.Write("\n\nREISCH: 0 \nSelect the processing:");
            controlSearch = Convert.ToInt32(Console.ReadLine());
            switch (controlSearch)
            {
                case 0:
                    while (true)
                    {
                        Console.Write("Searched data (exit: -1): ");
                        read = Convert.ToInt32(Console.ReadLine());
                        if (read == -1) break;
                        int index = reisch.search(read);
                        if (index == -1) Console.WriteLine("there isn't this data");
                        else Console.WriteLine("Index: {1} Number of probes: {2} ", read, index, reisch.Probe);
                    }
                    break;
                default:
                    Console.WriteLine("Invalid entry");
                    break;
            }


        }

        static void calculateProbesRequired()
        {
            double reischProbesRequired = 0.0;

            for (int i = 0; i < Datas.Count; i++)
            {
                reisch.search(Datas[i]);

                reischProbesRequired += reisch.Probe;
            }

            reischProbesRequired /= 1000;

            Console.WriteLine("\n\nPROBES REQUIRED: \nREISCH: {0}"
                , reischProbesRequired);
        }


    }
}
