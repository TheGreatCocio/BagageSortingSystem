using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagageSortingSystem
{
    public class BagageFactory
    {
        Random rnd = new Random();

        public Bagage CreateBagage()
        {
            int num = rnd.Next(1, 5);
            switch (num)
            {
                case 1:
                    if (SortingMachine.Instance.Terminals[(int)DestinationsEnum.Barcelona - 1].Open)
                    {
                        return new Bagage(DestinationsEnum.Barcelona.ToString());
                    }
                    return null;
                case 2:
                    if (SortingMachine.Instance.Terminals[(int)DestinationsEnum.Bangkok - 1].Open)
                    {
                        return new Bagage(DestinationsEnum.Bangkok.ToString());
                    }
                    return null; //asd
                case 3:
                    if (SortingMachine.Instance.Terminals[(int)DestinationsEnum.Danmark - 1].Open)
                    {
                        return new Bagage(DestinationsEnum.Danmark.ToString());
                    }
                    return null; 
                case 4:
                    if (SortingMachine.Instance.Terminals[(int)DestinationsEnum.Alanya - 1].Open)
                    {
                        return new Bagage(DestinationsEnum.Alanya.ToString());
                    }
                    return null; 
                default: return null;
            }
        }
    }
}
