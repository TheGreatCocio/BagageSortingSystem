using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagageSortingSystem
{
    public class Bagage
    {
        private static List<int> usedIds = new List<int>();        
        private int bagageId;
        private string destination;
        
        public int BagageId { get => bagageId; set => bagageId = value; }
        public string Destination { get => destination; set => destination = value; }

        public Bagage(string destination)
        {
            Destination = destination;
            BagageId = BagageIdGenerator();
        }

        private int BagageIdGenerator()
        {
            Random rnd = new Random();
            int id = rnd.Next(100000, 999999);
            bool jackpot = true;
            do
            {
                if (!usedIds.Contains(id))
                {                    
                    jackpot = false;
                }
                else
                {
                    id = rnd.Next(100000, 999999);
                }

            } while (jackpot);
            usedIds.Add(id);
            return id;
        }
    }
}
