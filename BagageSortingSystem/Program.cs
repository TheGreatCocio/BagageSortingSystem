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
            // Starts as many counter Treads as the central server has been configed to
            for (int i = 0; i < CentralServer.Instance.Counters; i++)
            {
                Thread _counter = new Thread(Counting);
                _counter.Start();
            }
            // Starts as many Terminal Treads as the central server has been configed to
            for (int i = 0; i < CentralServer.Instance.Terminals; i++)
            {
                Thread _terminal = new Thread(Terminaling);
                _terminal.Start();
            }

            sortingThread.Start();
            reader.Start();

            // Keeps Creating new Bagage!
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
        // Counter Method
        static void Counting()
        {
            Counter counter = new Counter();
            while (true)
            {
                lock (bagageToCheckIn)
                {
                    if (bagageToCheckIn.Count != 0)
                    {   // Dequeues from BagageToCheckIn and Enqueues The Conveyor to the sorting machine
                        Bagage bagage = bagageToCheckIn.Dequeue();
                        counterConveyor.Enqueue(bagage);
                        Console.WriteLine("--Counter[{0}] Checked Bagage [{1}] in", counter.CounterId, bagage.BagageId);
                        Thread.Sleep(1000);
                    }
                }                
            }
            
        }
        // Sorting Method
        static void Sorting()
        {
            while (true)
            {
                if (counterConveyor.Count != 0)
                {   // Dequeues the conveyor and Begin Sorting
                    Bagage bagage = counterConveyor.Dequeue();

                    Console.WriteLine("----Sorting Machine Got Bagage ID:[{0}]", bagage.BagageId);

                    SortingMachine.Instance.SortBagage(bagage);
                }
            }
        }
        // Terminal Method
        static void Terminaling()
        {   // Creates a new terminal
            Terminal terminal = new Terminal(); 
            //Adds the terminal to the Sorting Machine "Memory"
            SortingMachine.Instance.Terminals.Add(terminal);
            // starts a new FlightPlan pr. Terminal
            Thread flightPlanning = new Thread(() => FlightPlaning(terminal));
            flightPlanning.Start();
            while (true)
            {
                while (terminal.Open)
                {   // If the terminal is open. Keep Terminaling
                    if (terminal.TerminalConveyor.Count != 0)
                    {
                        terminal.DequeueBagage();
                    }
                }
                Console.WriteLine("Terminal {0} is closed!", terminal.TerminalNum);
                Thread.Sleep(10000);
            }
        }
        // Reader Method
        static void Reader()
        {
            while (true)
            {   // Reads the next keystroke                              
                switch (Console.ReadKey().Key)
                {   // If the key is "1" close/open terminal 1
                    case ConsoleKey.D1:
                        SortingMachine.Instance.Terminals[0].Open = !SortingMachine.Instance.Terminals[0].Open;
                        break;
                    // If the key is "2" close/open terminal 2
                    case ConsoleKey.D2:
                        SortingMachine.Instance.Terminals[1].Open = !SortingMachine.Instance.Terminals[1].Open;
                        break;
                    // If the key is "3" close/open terminal 3
                    case ConsoleKey.D3:
                        SortingMachine.Instance.Terminals[2].Open = !SortingMachine.Instance.Terminals[2].Open;
                        break;
                    // If the key is "4" close/open terminal 4
                    case ConsoleKey.D4:
                        SortingMachine.Instance.Terminals[3].Open = !SortingMachine.Instance.Terminals[3].Open;
                        break;
                    default:
                        break;
                }               
            }
        }
        // Flight Planning Method
        static void FlightPlaning(Terminal terminal)
        {   // Starts with a new plane (With a new timer)
            terminal.FlightPlan.NewPlane();
            while (true)
            {   // When the new planes liftoff time is less then the current time then "Take OFF"
                if (DateTime.UtcNow > terminal.FlightPlan.TimeBeforeLiftoff)
                {   // Close the terminal
                    SortingMachine.Instance.Terminals[terminal.FlightPlan.TerminalNumber - 1].Open = false;
                    Console.WriteLine("Flyet til [{0}] er lige taget afsted, et nyt kommer om lidt!", (DestinationsEnum)terminal.FlightPlan.TerminalNumber);
                    // Wait 10 seconds
                    Thread.Sleep(10000);
                    // Open the terminal
                    SortingMachine.Instance.Terminals[terminal.FlightPlan.TerminalNumber - 1].Open = true;
                    SortingMachine.Instance.Terminals[terminal.FlightPlan.TerminalNumber - 1].BagageToBeBoarded.Clear();
                    Console.WriteLine("Nyt fly er ankommet til [{0}]!", (DestinationsEnum)terminal.FlightPlan.TerminalNumber);
                    Console.WriteLine("Terminal [{0}] is Open!", (DestinationsEnum)terminal.FlightPlan.TerminalNumber);
                    // Set a new plane!
                    terminal.FlightPlan.NewPlane();
                }                              
            }
        }
    }
}
