using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace AutomationPractice
{
    class Driver
    {
        public string ChromeDriverPath
        {
            get
            {
                return ConfigurationManager.AppSettings["ChromeDriverPath"];
            }
        }
    }
}
