using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RemoteDesktop.LIB;

namespace RemoteDesktop.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            RemoteDesktopService.SetAllowTSConnection("User", "Pass", "Domain", "Server", true);
        }
    }
}
