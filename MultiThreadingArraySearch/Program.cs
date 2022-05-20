using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace MultiThreadingArraySearch
{
    class Program
    {
        static void Main(string[] args)
        {
            Sample ob = new Sample();
            
            int[] arr = new int[50];
            
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = i+1;
            }

            Console.Write("Enter the number b/w 1 to 50 that you want to search: ");
            int searchNumber = Convert.ToInt32(Console.ReadLine());
            
            int c = 5;
            int dist = arr.Length / c;
            
            for (int i = 0; i < c; i++)
            {
                int s = i * dist;
                int end = s + dist - 1;
                int id = i;
                Thread n = new Thread(() => ob.smallSearch(id, s, end, arr, searchNumber));
                n.Start();
            }
            
            Thread n2 = new Thread(ob.monitor);
            n2.Start();

        }

    }

    public class Sample
    {
        public int[] status = new int[5];
        public string[] searchResult = new string[5];
        
        public void smallSearch(int id, int s, int end, int[] arr, int sNo)
        {
            bool flag = false;
            
            Console.WriteLine("\nThread id: " + id + " Starting number: " + s + " Ending number: " + end);
            
            for (int i = s; i <= end; i++)
            {
                if (arr[i] == sNo)
                {
                    searchResult[id] = "found";
                    flag = true;
                    break;
                }
                else
                {
                    flag = false;
                }
            }

            if (flag != true)
            {
                searchResult[id] = "not found";
            }
            
            status[id] = 1;
        }
        public void monitor()
        {
            bool flag = false;

            while (status.Sum() != 5)
            {
                Thread.Sleep(1);
            }

            Console.WriteLine("\n---------------- THREAD INFO ----------------\n");

            for (int i = 0; i < searchResult.Length; i++)
            {
                if (searchResult[i] == "found")
                {
                    flag = true;
                    break;
                }
                else
                {
                    flag = false;
                }
            }

            if (flag != true)
            {
                Console.WriteLine("\nYour number is out of range");
            }
            else
            {
                for (int i = 0; i < searchResult.Length; i++)
                {
                    Console.WriteLine("\nYour number is " + searchResult[i] + " in thread: " + (i));
                }
            }
        }
    }
}
