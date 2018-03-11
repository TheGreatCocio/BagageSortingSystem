using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Terminal Class 
/// </summary>
namespace BagageSortingSystem
{
    public class Terminal
    {
        private static int terminalNumIncrementer = 1;
        private int terminalNum;
        private List<Bagage> bagageToBeBoarded = new List<Bagage>();
        private Queue<Bagage> terminalConveyor = new Queue<Bagage>();
        private bool open = true;
        private FlightPlan flightPlan;

        public List<Bagage> BagageToBeBoarded { get => bagageToBeBoarded; set => bagageToBeBoarded = value; }
        public Queue<Bagage> TerminalConveyor { get => terminalConveyor; set => terminalConveyor = value; }
        public int TerminalNum { get => terminalNum; set => terminalNum = value; }
        public bool Open { get => open; set => open = value; }
        public FlightPlan FlightPlan { get => flightPlan; set => flightPlan = value; }

        public Terminal()
        {
            terminalNum = terminalNumIncrementer++;
            FlightPlan = new FlightPlan(terminalNum);
        }
        // Each Terminal has its own Conveyor and is 
        //is responsible for dequeueing its own and throw it on the plane
        public void DequeueBagage()
        {            
            Bagage bagage = terminalConveyor.Dequeue();
            bagageToBeBoarded.Add(bagage);
            Console.WriteLine("-------Terminal[{0}] Dequeued Bagage ID: [{1}]", TerminalNum, bagage.BagageId);
            Console.WriteLine("##-------Terminal[{0}] Bagage count: [{1}]", TerminalNum, BagageToBeBoarded.Count);
        }                    
    }
}
