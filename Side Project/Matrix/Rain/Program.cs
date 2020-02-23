using System;
using System.Collections.Generic;
using System.Threading;
using Rain.Classes;

namespace Rain
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            List<Drops> dropList = new List<Drops>();
            for (int i = 0; i < 1000; i++)
            {
                if(random.Next(1,3) == 1)
                {
                    dropList.Add(new Drops());
                }
                foreach(Drops drop in dropList)
                {
                    if (drop.Active)
                    {
                        drop.MoveDrop();
                    }
                }
                Thread.Sleep(40);
            }
        }
    }
}
