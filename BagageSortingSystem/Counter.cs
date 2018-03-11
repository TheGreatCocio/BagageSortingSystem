using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagageSortingSystem
{
    class Counter
    {
        private static int counterIdIncrementer = 1;
        private int counterId;

        public int CounterId { get => counterId; set => counterId = value; }

        public Counter()
        {
            counterId = counterIdIncrementer++;
        }        
    }
}
