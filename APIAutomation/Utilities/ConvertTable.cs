using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace APIAutomation.Utilities
{
    class ConvertTable
    {
        public static Dictionary<String, String> converttoDictionary(Table table)
        {
                    
            Dictionary<String, String> dDict = new Dictionary<String, String>();
            foreach(var row in table.Rows)
            {
               dDict.Add("email", row[0]);
               dDict.Add("password", row[1]);
            }
            return dDict;
        }
    }
}
