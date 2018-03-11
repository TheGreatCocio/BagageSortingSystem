using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

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

        public void NewPlane()
        {
            timeBeforeLiftoff = DateTime.UtcNow.AddSeconds(rnd.Next(50,300)); 
        }
    }
}
