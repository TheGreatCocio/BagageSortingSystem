using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
/// <summary>
/// Sorting System is a Singleton
/// who is responsible for sorting
/// the bagage to the right terminal
/// </summary>
namespace BagageSortingSystem
{
    public class SortingMachine
    {
        private static SortingMachine instance;
        private List<Terminal> terminals = new List<Terminal>();

        public List<Terminal> Terminals { get => terminals; set => terminals = value; }

        public static SortingMachine Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SortingMachine();
                }
                return instance;
            }
        }

        private SortingMachine()
        {

        }

        public void SortBagage(Bagage bagage)
        {   //Foreach "Destinaion" in the destination enum
            foreach (DestinationsEnum item in Enum.GetValues(typeof(DestinationsEnum)))
            {
                //Thread.Sleep(1000);
                //If the bagage destination equals a destination in in the enum
                if (item.ToString() == bagage.Destination)
                {   // If the terminal to that destinaion is open
                    if (terminals[(int)item - 1].Open == true)
                    {   // Enqueue the conveyor and send it to the terminal
                        terminals[(int)item - 1].TerminalConveyor.Enqueue(bagage);
                        Console.WriteLine("------Send Bagage[{0}] To Terminal[{1}]", bagage.BagageId, terminals[(int)item - 1].TerminalNum);
                    }   //If not
                    else
                    {   // Send it to Lost and Found
                        LostAndFound.Instance.LostAndFoundContainer.Add(bagage);
                        Console.WriteLine("Terminal[{0}] Is closed and the plane has taken off! Bagage[{1}] has been thrown in Lost And Found!", terminals[(int)item - 1].TerminalNum, bagage.BagageId);
                    }
                }
            }
        }
    }
}
