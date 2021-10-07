using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace Problem01
{
    class Program
    {
        static byte[] Data_Global = new byte[1000000000];
        static long Sum_Global = 0;
        //static int G_index = 0;

        static int ReadData()
        {
            int returnData = 0;
            FileStream fs = new FileStream("Problem01.dat", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();

            try
            {
                Data_Global = (byte[])bf.Deserialize(fs);
            }
            catch (SerializationException se)
            {
                Console.WriteLine("Read Failed:" + se.Message);
                returnData = 1;
            }
            finally
            {
                fs.Close();
            }

            return returnData;
        }
        static void sum(int start, int end)
        {
           // Console.Write("I'm in Loop now {0}",start);
            for (int G_index = start; G_index <= end; G_index += 1)
            {
                if (Data_Global[G_index] % 2 == 0)
                {
                    Sum_Global -= Data_Global[G_index];
                }
                else if (Data_Global[G_index] % 3 == 0)
                {
                    Sum_Global += (Data_Global[G_index] * 2);
                }
                else if (Data_Global[G_index] % 5 == 0)
                {
                    Sum_Global += (Data_Global[G_index] / 2);
                }
                else if (Data_Global[G_index] % 7 == 0)
                {
                    Sum_Global += (Data_Global[G_index] / 3);
                }
                Data_Global[G_index] = 0;
                G_index++;
            }
        }
        // static void sum()
        // {
            
        //         if (Data_Global[G_index] % 2 == 0)
        //         {
        //             Sum_Global -= Data_Global[G_index];
        //         }
        //         else if (Data_Global[G_index] % 3 == 0)
        //         {
        //             Sum_Global += (Data_Global[G_index] * 2);
        //         }
        //         else if (Data_Global[G_index] % 5 == 0)
        //         {
        //             Sum_Global += (Data_Global[G_index] / 2);
        //         }
        //         else if (Data_Global[G_index] % 7 == 0)
        //         {
        //             Sum_Global += (Data_Global[G_index] / 3);
        //         }
        //         Data_Global[G_index] = 0;
        //         G_index++;
            
        // }
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            int i, y;
            const int N = 1000000000;
            int cores = 10 ; 
            int range =  N/cores;
            const int  approxDefaultRuntime =43000 ; 

            /* Read data from file */
            Console.Write("Data read...");
            y = ReadData();
            if (y == 0)
            {
                Console.WriteLine("Complete.");
            }
            else
            {
                Console.WriteLine("Read Failed!");
            }

            /* Start */
            Thread[] threads = new Thread[cores];
            Console.Write("\n\nWorking...");
            sw.Start();
            //for (i=0; i < N;i++)
            //    sum();
            for ( i = 0; i < cores; i++){
                //Console.WriteLine(i);
               // Console.WriteLine("new Thread Start");
               // Console.WriteLine(i);
                Thread t = new Thread( ()=> sum(i*range,i*range+range-1)) ;
                t.Start();
                threads[i] = t;
               // Console.WriteLine("Out of function now ");

            }
                
            sw.Stop();
            Console.WriteLine("Done.");

            /* Result */
            
            Console.WriteLine("Summation result: {0}", Sum_Global);
            Console.WriteLine("Time used: " + sw.ElapsedMilliseconds.ToString() + "ms");
            Console.WriteLine("Approximate Defualt Time before Thread: {0} ms",approxDefaultRuntime);
        }
    }
}
