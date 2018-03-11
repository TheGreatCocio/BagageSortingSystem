using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
/// <summary>
/// Flight Plan Class 
/// </summary>
namespace BagageSortingSystem
{
    public class FlightPlan
    {
        private Random rnd = new Random();
        private int terminalNumber;
        private DateTime timeBeforeLiftoff;

        public int TerminalNumber { get => terminalNumber; set => terminalNumber = value; }
        public DateTime TimeBeforeLiftoff { get => timeBeforeLiftoff;private set => timeBeforeLiftoff = value; }

        public FlightPlan(int terminalNumber)
        {
            TerminalNumber = terminalNumber;
        }
        // Sets the timer before the next plane leaves
        public void NewPlane()
        {
            timeBeforeLiftoff = DateTime.UtcNow.AddSeconds(rnd.Next(20,50)); 
        }
    }
}
