using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        {
            foreach (DestinationsEnum item in Enum.GetValues(typeof(DestinationsEnum)))
            {
                //Thread.Sleep(1000);
                if (item.ToString() == bagage.Destination)
                {
                    if (terminals[(int)item - 1].Open == true)
                    {
                        terminals[(int)item - 1].TerminalConveyor.Enqueue(bagage);
                        Console.WriteLine("------Send Bagage[{0}] To Terminal[{1}]", bagage.BagageId, terminals[(int)item - 1].TerminalNum);
                    }
                    else
                    {
                        LostAndFound.Instance.LostAndFoundContainer.Add(bagage);
                        Console.WriteLine("Terminal[{0}] Is closed and the plane has taken off! Bagage[{1}] has been thrown in Lost And Found!", terminals[(int)item - 1].TerminalNum, bagage.BagageId);
                    }
                }
            }
        }
    }
}
