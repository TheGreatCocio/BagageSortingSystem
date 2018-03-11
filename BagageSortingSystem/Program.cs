using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BagageSortingSystem
{
    class Program
    {
        static Queue<Bagage> counterConveyor = new Queue<Bagage>();
        static Queue<Bagage> bagageToCheckIn = new Queue<Bagage>();

        static void Main(string[] args)
        {
            BagageFactory bagageFactory = new BagageFactory();
            Thread sortingThread = new Thread(Sorting);
            Thread reader = new Thread(Reader);

            for (int i = 0; i < CentralServer.Instance.Counters; i++)
            {
                Thread _counter = new Thread(Counting);
                _counter.Start();
            }
            for (int i = 0; i < CentralServer.Instance.Terminals; i++)
            {
                Thread _terminal = new Thread(Terminaling);
                _terminal.Start();
            }

            sortingThread.Start();
            reader.Start();

            while (true)
            {
                for (int i = 0; i < 10; i++)
                {
                    Bagage bagage = bagageFactory.CreateBagage();
                    if (bagage != null)
                    {
                        bagageToCheckIn.Enqueue(bagage);
                        Console.WriteLine("Bagage incomming! ID:[{0}]", bagage.BagageId);
                    }
                    Thread.Sleep(1000);
                }
                Thread.Sleep(1000);            
            }
        }

        static void Counting()
        {
            Counter counter = new Counter();
            while (true)
            {
                lock (bagageToCheckIn)
                {
                    if (bagageToCheckIn.Count != 0)
                    {
                        Bagage bagage = bagageToCheckIn.Dequeue();
                        counterConveyor.Enqueue(bagage);
                        Console.WriteLine("--Counter[{0}] Checked Bagage [{1}] in", counter.CounterId, bagage.BagageId);
                        Thread.Sleep(1000);
                    }
                }                
            }
            
        }

        static void Sorting()
        {
            while (true)
            {
                if (counterConveyor.Count != 0)
                {
                    Bagage bagage = counterConveyor.Dequeue();

                    Console.WriteLine("----Sorting Machine Got Bagage ID:[{0}]", bagage.BagageId);

                    SortingMachine.Instance.SortBagage(bagage);
                }
            }
        }

        static void Terminaling()
        {
            Terminal terminal = new Terminal();            
            SortingMachine.Instance.Terminals.Add(terminal);
            Thread flightPlanning = new Thread(() => FlightPlaning(terminal));
            flightPlanning.Start();
            while (true)
            {
                while (terminal.Open)
                {
                    if (terminal.TerminalConveyor.Count != 0)
                    {
                        terminal.DequeueBagage();
                    }
                }
                Console.WriteLine("Terminal {0} is closed!", terminal.TerminalNum);
                Thread.Sleep(10000);
            }
        }

        static void Reader()
        {
            while (true)
            {                                
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        SortingMachine.Instance.Terminals[0].Open = !SortingMachine.Instance.Terminals[0].Open;
                        break;
                    case ConsoleKey.D2:
                        SortingMachine.Instance.Terminals[1].Open = !SortingMachine.Instance.Terminals[1].Open;
                        break;
                    case ConsoleKey.D3:
                        SortingMachine.Instance.Terminals[2].Open = !SortingMachine.Instance.Terminals[2].Open;
                        break;
                    case ConsoleKey.D4:
                        SortingMachine.Instance.Terminals[3].Open = !SortingMachine.Instance.Terminals[3].Open;
                        break;
                    default:
                        break;
                }               
            }
        }

        static void FlightPlaning(Terminal terminal)
        {            
            while (true)
            {                
                if (DateTime.UtcNow > terminal.FlightPlan.TimeBeforeLiftoff)
                {
                    SortingMachine.Instance.Terminals[terminal.FlightPlan.TerminalNumber - 1].Open = false;
                    Console.WriteLine("Flyet til [{0}] er lige taget afsted, et nyt kommer om lidt!", (DestinationsEnum)terminal.FlightPlan.TerminalNumber);
                    Thread.Sleep(10000);
                    SortingMachine.Instance.Terminals[terminal.FlightPlan.TerminalNumber - 1].Open = true;
                    SortingMachine.Instance.Terminals[terminal.FlightPlan.TerminalNumber - 1].BagageToBeBoarded.Clear();
                    Console.WriteLine("Nyt fly er ankommet til [{0}]!", (DestinationsEnum)terminal.FlightPlan.TerminalNumber);
                    Console.WriteLine("Terminal [{0}] is Open!", (DestinationsEnum)terminal.FlightPlan.TerminalNumber);
                    terminal.FlightPlan.NewPlane();
                }                              
            }
        }
    }
}
