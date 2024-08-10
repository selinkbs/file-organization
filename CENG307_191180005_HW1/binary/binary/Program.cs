using System;

namespace binary
{
  
    public class Node
    {
        public int data;
        public Node next;
        public Node(int d)
        {
            data = d;
            next = null;
        }
    }
    class Bidirectional
    {
        public int begin;
        public int end;
        public int[] probe;
        public Node[] records;

        public void put_array(Node[] records, Node node, int d)
        {
            records[d] = node;
        }
        public void linking(Node first, Node sec)
        {
            Node node = first.next;
            first.next = sec;
            sec.next = node;
        }
        public Node find(int d, int size, Node[] records)
        {
            int loc = d % size;
            Node node = records[loc];
            while (node.data != d)
            {
                node = node.next;
            }
            return node;
        }
        public int find_probe(int d, int size, Node[] records)
        {
            int probe = 1;
            int loc = d % size;
            Node node = records[loc];
            while (node.data != d)
            {
                node = node.next;
                probe++;
            }
            return probe;
        }
        public void createProbe(int[] probe, Node[] records, int size)
        {
            for (int i = 0; i < probe.Length; i++)
            {
                if (probe[i] != 1)
                {
                    if (records[i] != null)
                        probe[i] = find_probe(records[i].data, size, records);
                }
            }
        }
        public void writeConsole(Node[] records, int size, int[] probe)
        {
          
            Console.WriteLine("\nBEISCH\n");
            Console.WriteLine("{0}\t{1,-15}\t{2,10}\t{3,-10}", "i", "Key", "Next Index", "Probe");
            for (int i = 0; i < records.Length; i++)
            {
                Console.Write("{0}\t", i);
                if (records[i] == null)
                {
                    Console.Write("{0,-15}\t{1,-10}", "Null", "Null");
                }
                else
                {
                    Console.Write("{0,-15}", records[i].data);
                    if (records[i].next != null)
                    {
                        
                        Console.Write("\t{0,-10}", Array.IndexOf<Node>(records, records[i].next));

                    }
                    else
                        Console.Write("\t{0,-10}", "Null");
                }

                Console.WriteLine("\t{0,-10}", probe[i]);
            }
        }
        public void createBidirectional(int[] keysArray, int _size)
        {
            Node node;
            bool begin_or_end = true;
            int sayi;
            int size = _size;
            records = new Node[size];
            probe = new int[size];
            begin = 0;
            end = size - 1;

            for (int i = 0; i < keysArray.Length; i++)
            {
                sayi = keysArray[i];
                int loc = sayi % size;

                if (records[loc] == null)
                {
                    node = new Node(sayi);
                    put_array(records, node, loc);
                    probe[loc] = 1;
                }

                else
                {
                    node = new Node(sayi);
                    if (begin_or_end)
                    {
                        Node is_empty = records[end];
                        while (is_empty != null)
                        {
                            end--;
                            is_empty = records[end];
                        }
                        put_array(records, node, end);
                      
                        linking(records[loc], node);
                        begin_or_end = false;
                        end--;
                    }
                    else
                    {
                        Node is_empty = records[begin];
                        while (is_empty != null)
                        {
                            begin++;
                            is_empty = records[begin];
                        }
                        put_array(records, node, begin);
                        linking(records[loc], node);
                        begin_or_end = true;
                        begin++;
                    }
                }
            }
            
            createProbe(probe, records, size);
            writeConsole(records, size, probe);
        }
    }
    class Heap
    {
        public int[] heapArray;
        public int maxSize;
        public int currentSize;
        public int[] probes;
        public int[] records;
        public void CreateHeap(int size)
        {
            maxSize = size;
            heapArray = new int[maxSize];
            currentSize = 0;
        }
        public int Parent(int i)
        {
            if ((i - 1) / 2 >= 0)
                return (i - 1) / 2;
            else
                return -1;
        }
        public bool isRightChild(int i)
        {
            if (i % 2 == 0)
            {
                return true;
            }
            else
                return false;
        }
        public void createBinary(int[] keysArray, int _size)
        {
            int key;
            int size = _size;
            records = new int[size];
            probes = new int[size];

            CreateHeap(32000);//

            for (int i = 0; i < keysArray.Length; i++)
            {
                key = keysArray[i];
                int loc = key % size;
                if (records[loc] == 0)
                {
                    records[loc] = key;
                }
                else
                {
                    currentSize = 0;
                    heapArray[currentSize] = loc;
                    while (records[loc] != 0)
                    {
                        currentSize++;
                        if (!(isRightChild(currentSize)))
                        {
                            
                            int parent_index = Parent(currentSize);
                            while (!(isRightChild(parent_index)))
                            {
                                parent_index = Parent(parent_index);
                            }
                            if (isRightChild(parent_index) && parent_index != 0)
                            {
                                int parent_parent_index = Parent(parent_index);
                                

                                int index = heapArray[parent_parent_index];
                                int parent_key = records[index];
                                int step_size = parent_key / size;
                                
                                int current_parent_index = Parent(currentSize);
                                loc = (heapArray[current_parent_index] + step_size) % size;
                                heapArray[currentSize] = loc;
                                                       
                            }
                            if (parent_index == 0)
                            {
                                int current_parent_index = Parent(currentSize);//
                                int step_size = key / size;
                                loc = (heapArray[current_parent_index] + step_size) % size;
                                heapArray[currentSize] = loc;
                             

                            }
                       
                        }
                        else
                        {
                            int parent_index = Parent(currentSize);
                            int index = heapArray[parent_index];
                            int parent_key = records[index];
                            int step_size = parent_key / size;
                            
                            loc = (index + step_size) % size;
                            heapArray[currentSize] = loc;

                          
                        }
                    }
                    
                    if (records[loc] == 0)
                    {
                        while (currentSize != 0)
                        {
                          
                            if (currentSize % 2 == 0)
                            {
                                while (currentSize != 0 && currentSize % 2 == 0)
                                {
                                    
                                    int parent_index = Parent(currentSize);
                                    int index = heapArray[parent_index];
                                    int parent_key = records[index];
                                                                 
                                    records[loc] = parent_key;
                                    records[index] = 0;
                                    loc = index;
                                    currentSize = parent_index;
                                }
                                if (currentSize == 0)
                                {
                                    int index = heapArray[currentSize];
                                    records[index] = key;
                                }
                            }
                            else
                            {
                                int parent_index = Parent(currentSize);
                                while (!(isRightChild(parent_index)))
                                {
                                    parent_index = Parent(parent_index);
                                }

                                if (parent_index != 0)
                                {
                                    int parent_parent_index = Parent(parent_index);
                                                                                  
                                    int index = heapArray[parent_parent_index];
                                    int parent_key = records[index];

                                    records[loc] = parent_key;
                                    records[index] = 0;
                                    loc = index;
                                    currentSize = parent_parent_index;
                                }
                                else 
                                {
                                    records[loc] = key;
                                    currentSize = 0;
                                }
                            }
                        }
                    }
                }
            }
            probes = findProbe(keysArray, records);
            Console.WriteLine("\nBINARY TREE METHOD\n");
            Console.WriteLine("{0}\t{1,-10}\t\t{2,-10}", "i", "Key", "Probe");
            for (int i = 0; i < records.Length; i++)
            {
                if (records[i] == 0)
                    Console.WriteLine("{0}\t{1,-10}\t\t{2,-10}", i, "Null", probes[i]);
                else
                    Console.WriteLine("{0}\t{1,-10}\t\t{2,-10}", i, records[i], probes[i]);
            }
        }
        public int[] findProbe(int[] dizi, int[] records)
        {
            int[] probes = new int[records.Length];
            int loc;
            int step_size;
            int size = records.Length;
            bool c = false;
            for (int i = 0; i < dizi.Length; i++)
            {
                c = false;
                loc = dizi[i] % size;
                step_size = dizi[i] / size;
                int k;
                for (k = 0; k < records.Length; k++)
                {
                    if (records[loc] == dizi[i])
                    {
                        c = true;
                        break;
                    }

                    else
                    {
                        loc = (loc + step_size) % size;
                    }
                }
                if (c == true)
                    probes[loc] = k + 1;

            }
            return probes;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {

            //int[] dizi = new int[10] { 27, 18, 29, 28, 39, 13, 16, 41, 17, 19 };
            Bidirectional bidirectional = new Bidirectional();
            Heap heap = new Heap();
            Console.WriteLine("What is the number of keys?(U can change the table size manually in line 364)");
            int keyNumber = Convert.ToInt32(Console.ReadLine());
            int size = 11;
            int[] dizi = createKeyArray(keyNumber, size);
            bidirectional.createBidirectional(dizi, size);
            heap.createBinary(dizi, size);

            double packingFactor = (double)keyNumber / size;
            packingFactor *= 100;
            Console.WriteLine("Packing Factor --> %" + packingFactor);

            Console.WriteLine("For BEISCH average probe -->" + averageProbe(bidirectional.probe, dizi.Length));

            Console.WriteLine("For Binary Tree Method average probe -->" + averageProbe(heap.probes, dizi.Length));

            Console.WriteLine("Which key do you want to search?");
            int searchKey = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("BEISCH");
            int loc1 = searchKeyForBidirect(searchKey, bidirectional.records);
            if (loc1 == -1)
            {
                Console.WriteLine("Not exist");
            }
            else
            {
                Console.WriteLine("{0} key is in index {1}", searchKey, loc1);
            }

            Console.WriteLine("BINARY TREE");
            int loc2 = searchKeyForBinary(searchKey, heap.records);
            if (loc2 == -1)
            {
                Console.WriteLine("Not exist");
            }
            else
            {
                Console.WriteLine("{0} key is in index {1}", searchKey, loc2);
            }

        }
        static int searchKeyForBidirect(int key, Node[] records)
        {
            bool c = false;
            int index;
            int size = records.Length;
            int loc = key % size;
            Node node = records[loc];
            while (node != null)
            {
                if (node.data == key)
                {
                    c = true;
                    break;
                }
                if (node.next != null)
                    node = node.next;
                else
                    node = null;
            }
            if (c == true)
            {
                index = Array.IndexOf<Node>(records, node);
                return index;
            }
            else
                return -1;

        }
        static int searchKeyForBinary(int key, int[] records)
        {
            int loc = -1;
            int size = records.Length;
            int step_size = key / size;
            loc = key % size;
            int i;
            for (i = 0; i < records.Length; i++)
            {
                if (key == records[loc])
                    break;

                else
                {
                    loc = (loc + step_size) % size;
                }

            }
            if (i == records.Length)
                return -1;
            else
                return loc;

        }
        static float averageProbe(int[] probe, int numberOfKeys)
        {
            float average;
            float toplam = 0;
            for (int i = 0; i < probe.Length; i++)
            {
                toplam += probe[i];
            }
            Console.WriteLine("Total number of probes = " + toplam);
            average = toplam / numberOfKeys;
            return average;
        }

        static int[] createKeyArray(int numberOfKeys, int size)
        {
            Random rnd = new Random();
            Console.WriteLine("Table Size:" + size);
            Console.WriteLine("Number of keys:" + numberOfKeys);
            int[] dizi = new int[numberOfKeys];
            int sayi;
       
            for (int i = 0; i < dizi.Length; i++)
            {
                sayi = rnd.Next(1, int.MaxValue);
                if (Array.IndexOf(dizi, sayi) == -1)
                    dizi[i] = sayi;
                else
                    i--;
            }
            Console.WriteLine("\nKEYS\n");
            for (int i = 0; i < dizi.Length; i++)
            {
                Console.WriteLine("{0}\t{1}", i, dizi[i]);
            }
            Console.WriteLine();
            Console.WriteLine();
            return dizi;
        }
    }
}
