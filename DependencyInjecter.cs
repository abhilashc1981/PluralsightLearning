using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationPractice
{
    class DependencyInjecter
    {
        private Driver driver;
        public Driver Driver
        {
            get
            {
                if (driver == null)
                {
                    driver = new Driver();
                }
                return driver;
            }
        }
    }
}
