using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPJHF
{
    public class Work
    {
        // Private data members
        private readonly string name;
        private readonly int requiredTimeInMinutes;
        private readonly int materialCosts;

        // Constructor
        public Work(string name, int requiredTimeInMinutes, int materialCosts)
        {
            this.name = name;
            this.requiredTimeInMinutes = requiredTimeInMinutes;
            this.materialCosts = materialCosts;
        }

        // Properties to expose data members
        public string Name => name;
        public int RequiredTimeInMinutes => requiredTimeInMinutes;
        public int MaterialCosts => materialCosts;

        // Properties to calculate hour and minute of service time
        public int ServiceHours => RequiredTimeInMinutes / 60;
        public int ServiceMinutes => RequiredTimeInMinutes % 60;
    }
}

