using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcMimax12
{
    internal class PowerCfg
    {
        public enum ThrottleArgs
        {
            PROCTHROTTLEMIN,
            PROCTHROTTLEMIN1,
            PROCTHROTTLEMAX,
            PROCTHROTTLEMAX1
        }

        public bool Set(ThrottleArgs arg, int perc)
        {
            string s_arg = arg.ToString();
            bool status;
            bool setactive = true;

            ProcessStartInfo start_info = new ProcessStartInfo()
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "powercfg",
                Arguments = "-setacvalueindex scheme_current sub_processor " + s_arg + perc.ToString(" 0"),
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                Verb = "runas"
            };

            do
            {
                Process cmd = new Process() { StartInfo = start_info };

                status = cmd.Start();

                if (status)
                {
                    cmd.WaitForExit();
                    cmd.Dispose();
                    if (setactive)
                    {
                        start_info.Arguments = "-setactive scheme_current";
                        setactive = false;
                    }
                    else
                        start_info.Arguments = string.Empty;

                }

            } while (start_info.Arguments != string.Empty);

            return status;
        }
    }
}
