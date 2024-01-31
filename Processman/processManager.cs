using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;

using System.Management;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Processman
{
    public class RunningApplicationInfo
    {
        public string ProcessName { get; set; }
        public string MainWindowTitle { get; set; }
        public int ProcessId { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan TotalProcessTime { get; set; }
        public TimeSpan TotalUserTime { get; set; }
        public int HandleCount { get; set; }
        public int ThreadCount { get; set; }
        public float CPUsagePercent { get; set; }
        public float RAMUsagePercent { get; set; }
        public float RAMUsageValue { get; set; }

        public RunningApplicationInfo(string processName, string mainWindowTitle, int processId, DateTime startTime, TimeSpan totalProcessTime, TimeSpan totalUserTime, int handleCount, int threadCount, float cpuPercent, float ramPercent, float ramValue)
        {
            ProcessName = processName;
            MainWindowTitle = mainWindowTitle;
            ProcessId = processId;
            StartTime = startTime;
            TotalProcessTime = totalProcessTime;
            TotalUserTime = totalUserTime;
            HandleCount = handleCount;
            ThreadCount = threadCount;
            CPUsagePercent = cpuPercent;
            RAMUsagePercent = ramPercent;
            RAMUsageValue = ramValue;
        }


    }

    public class RunningApplicationNode
    {
        public RunningApplicationInfo Data { get; set; }
        public RunningApplicationNode Next { get; set; }
        public RunningApplicationNode Previous { get; set; }

        public RunningApplicationNode(RunningApplicationInfo data)
        {
            Data = data;
            Next = null;
            Previous = null;
        }
    }

    public class RunningApplicationLinkedList
    {
        public  RunningApplicationNode head;
         public RunningApplicationNode tail;

        public void AddFirst(RunningApplicationInfo data)
        {
            RunningApplicationNode newHead = new RunningApplicationNode(data);
            if (head == null)
            {
                head = newHead;
                tail = newHead;
            }
            else
            {
                newHead.Next = head;
                head.Previous = newHead;
                head = newHead;
            }
        }

        public RunningApplicationNode GetHead()
        {
            return head;
        }

        public RunningApplicationNode GetTail()
        {
            return tail;
        }
    }

    public class ProcessManager
    {
        public RunningApplicationLinkedList GetRunningUserApplicationsInfo()
        {
            RunningApplicationLinkedList runningAppsList = new RunningApplicationLinkedList();

            PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available MBytes"); 
            float totalPhysicalMemory = ramCounter.NextValue();  

            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                if (!string.IsNullOrEmpty(process.MainWindowTitle))  
                {


                    string name = process.ProcessName;
                    string mainwindowtitle = process.MainWindowTitle;
                    int id = process.Id;
                    DateTime time = process.StartTime;
                    TimeSpan totalprocessTime = process.TotalProcessorTime;
                    TimeSpan totaluserTime = process.UserProcessorTime;
                    int handleCount = process.HandleCount;
                    int theardcount = process.Threads.Count;
                    float cpuPercent = CalculateCpuUsage(process);
                    float ramValue = process.WorkingSet64 / (1024.0f * 1024.0f);
                    float ramPercent = (ramValue / totalPhysicalMemory) * 100;

                    RunningApplicationInfo appInfo = new RunningApplicationInfo(name, mainwindowtitle, id, time, totalprocessTime, totaluserTime, handleCount, theardcount, cpuPercent, ramPercent, ramValue);

                    runningAppsList.AddFirst(appInfo);
                }
            }

            return runningAppsList;
        }

        private float CalculateCpuUsage(Process process)
        {
            TimeSpan cpuTime = process.TotalProcessorTime;
            TimeSpan wallTime = DateTime.Now - process.StartTime;
            float cpuUsage = (float)cpuTime.TotalSeconds / (float)wallTime.TotalSeconds / Environment.ProcessorCount * 100.0f;
            return cpuUsage;
        }

        public RunningApplicationNode FindByName(string name  , RunningApplicationLinkedList list)
        {
            RunningApplicationNode current = list.GetHead();
            if(current != null)
            {
                while(current != null)
                {
                    if(current.Data.ProcessName.Equals(name))
                    {
                        return current;
                    }
                    else
                    {
                        current = current.Next;
                    }
                }
            }
            return null;
        }
        public void BubbleSortByRam(RunningApplicationLinkedList list)
        {
            bool swapped;
            RunningApplicationNode current;
            RunningApplicationNode temp = null;

            if (list.GetHead() == null)
                return;

            do
            {
                swapped = false;
                current = list.GetHead();

                while (current.Next != temp)
                {
                    if (current.Data.RAMUsageValue > current.Next.Data.RAMUsageValue)
                    {
                      
                        RunningApplicationInfo tempData = current.Data;
                        current.Data = current.Next.Data;
                        current.Next.Data = tempData;
                        swapped = true;
                    }
                    current = current.Next;
                }
                temp = current;
            }
            while (swapped);
        }


       public RunningApplicationNode findmaxCpuUsr( RunningApplicationLinkedList List)
        {
            RunningApplicationNode  current = List.GetHead();

            RunningApplicationNode maximum = null;
            float max = 0;
            while(current != null)
            {
                if(current.Data.CPUsagePercent > max)
                {
                    max = current.Data.CPUsagePercent;
                    maximum = current;
                }

                current = current.Next;

                
            }



            return maximum;



        }

        public RunningApplicationNode findminCpuUsr(RunningApplicationLinkedList List)
        {
            RunningApplicationNode current = List.GetHead();

            RunningApplicationNode minimum = null;
            float min = 100000000;
            while (current != null)
            {
                if (current.Data.CPUsagePercent < min)
                {
                    min = current.Data.CPUsagePercent;
                    minimum = current;
                }

                current = current.Next;


            }



            return minimum;



        }

        public RunningApplicationNode findminRamUsr(RunningApplicationLinkedList List)
        {
            RunningApplicationNode current = List.GetHead();

            RunningApplicationNode minimum = null;
            float min = 10000000;
            while (current != null)
            {
                if (current.Data.RAMUsageValue < min)
                {
                    min = current.Data.RAMUsageValue;
                    minimum = current;
                }

                current = current.Next;


            }



            return minimum;



        }





        public RunningApplicationNode findmaxRamUsr(RunningApplicationLinkedList List)
        {
            RunningApplicationNode current = List.GetHead();

            RunningApplicationNode maximum = null;
            float max = 0;
            while (current != null)
            {
                if (current.Data.RAMUsageValue > max)
                {
                    max = current.Data.RAMUsageValue;
                    maximum = current;
                }

                current = current.Next;


            }



            return maximum;



        }


        public void KillprocessByName(string name)
        {
            Process[] processes = Process.GetProcessesByName(name);

           
            foreach (Process process in processes)
            {
                try
                {
                    process.Kill();
                }
                catch
                {
                    MessageBox.Show("Unable to Kill Process : " + name);
                    return;
                }
            }
        }

        public void DeleteProcess(RunningApplicationNode processToDelete, RunningApplicationLinkedList list)
        {
            if (processToDelete == null || list == null || list.GetHead() == null)
            {
                return;
            }

            RunningApplicationNode current = list.GetHead();

            if (processToDelete == current)
            {
                list.head = current.Next;
                if (list.head != null)
                {
                    list.head.Previous = null;
                }
                else
                {
                   
                    list.tail = null;
                }
                return;
            }

        
            while (current != null && current != processToDelete)
            {
                current = current.Next;
            }

            if (current == null)
            {
                
                return;
            }

            if (current.Next != null)
            {
                current.Next.Previous = current.Previous;
            }
            else
            {
              
                list.tail = current.Previous;
            }

            if (current.Previous != null)
            {
                current.Previous.Next = current.Next;
            }

          
            current = null;
        }

        public void FullRefresh(RunningApplicationLinkedList list)
        {

            RunningApplicationNode current = list.GetHead();
            if(current != null)
            {
                while(current!= null)
                {
                   
                        KillprocessByName(current.Data.ProcessName);
                        DeleteProcess(current, list);
                  
                  
                   
                }
            }

        }



        public void BubbleSortBypu(RunningApplicationLinkedList list)
        {
            bool swapped;
            RunningApplicationNode current;
            RunningApplicationNode temp = null;

            if (list.GetHead() == null)
                return;

            do
            {
                swapped = false;
                current = list.GetHead();

                while (current.Next != temp)
                {
                    if (current.Data.CPUsagePercent > current.Next.Data.CPUsagePercent)
                    {

                        RunningApplicationInfo tempData = current.Data;
                        current.Data = current.Next.Data;
                        current.Next.Data = tempData;
                        swapped = true;
                    }
                    current = current.Next;
                }
                temp = current;
            }
            while (swapped);
        }

        public void BubbleSortID(RunningApplicationLinkedList list)
        {
            bool swapped;
            RunningApplicationNode current;
            RunningApplicationNode temp = null;

            if (list.GetHead() == null)
                return;

            do
            {
                swapped = false;
                current = list.GetHead();

                while (current.Next != temp)
                {
                    if (current.Data.ProcessId > current.Next.Data.ProcessId)
                    {

                        RunningApplicationInfo tempData = current.Data;
                        current.Data = current.Next.Data;
                        current.Next.Data = tempData;
                        swapped = true;
                    }
                    current = current.Next;
                }
                temp = current;
            }
            while (swapped);
        }


    }






}