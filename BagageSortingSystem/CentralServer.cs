using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Central Server decides how many Terminals and Counters there is
/// </summary>
namespace BagageSortingSystem
{
    public class CentralServer
    {
        private static CentralServer instance;

        private int counters = 3;
        private int terminals = Enum.GetNames(typeof(DestinationsEnum)).Length;

        public int Counters { get => counters; set => counters = value; }
        public int Terminals { get => terminals; set => terminals = value; }


        public static CentralServer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CentralServer();
                }
                return instance;
            }
        }

        private CentralServer()
        {

        }
    }
}
