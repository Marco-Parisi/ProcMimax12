using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace ProcMimax12
{
    internal class Program
    {
        static void Main(string[] args)
         {
            string c_input;
            int perc_value, last_value=-1;
            bool notparsed;

            PowerCfg powercfg = new PowerCfg();
            List<Tuple<string, PowerCfg.ThrottleArgs>> setList = new List<Tuple<string, PowerCfg.ThrottleArgs>>();
            setList.Add(new Tuple<string, PowerCfg.ThrottleArgs>("E-Core Min", PowerCfg.ThrottleArgs.PROCTHROTTLEMIN));
            setList.Add(new Tuple<string, PowerCfg.ThrottleArgs>("E-Core Max", PowerCfg.ThrottleArgs.PROCTHROTTLEMAX));
            setList.Add(new Tuple<string, PowerCfg.ThrottleArgs>("P-Core Min", PowerCfg.ThrottleArgs.PROCTHROTTLEMIN1));
            setList.Add(new Tuple<string, PowerCfg.ThrottleArgs>("P-Core Max", PowerCfg.ThrottleArgs.PROCTHROTTLEMAX1));

            int i = 0;
            foreach (var setting in setList)
            {
                do
                {
                    Console.Write(setting.Item1 + " percentage: ");
                    c_input = Console.ReadLine();
                    notparsed = !int.TryParse(c_input, out perc_value);
                } while (notparsed || perc_value < last_value);

                powercfg.Set(setting.Item2, perc_value);

                if (i == 0)
                {
                    last_value = perc_value;
                    i++;
                }
                else
                {
                    last_value = -1;
                    i = 0;
                }
            }
        }
    }
}
