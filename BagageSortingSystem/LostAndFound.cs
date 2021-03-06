﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// If a terminal closes before all bagage is boarded it lands here
/// </summary>
namespace BagageSortingSystem
{
    public class LostAndFound
    {
        private static LostAndFound instance;

        private List<Bagage> lostAndFoundContainer = new List<Bagage>();

        public List<Bagage> LostAndFoundContainer { get => lostAndFoundContainer; set => lostAndFoundContainer = value; }

        public static LostAndFound Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LostAndFound();
                }
                return instance;
            }
        }

        private LostAndFound()
        {

        }
    }
}
