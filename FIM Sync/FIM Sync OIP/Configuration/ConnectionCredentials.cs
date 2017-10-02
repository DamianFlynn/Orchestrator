using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Management;
using System.Management.Instrumentation;
using FIM_Sync_LIB;

namespace FIM_Sync_OIP
{
    [ActivityData("FIM Sync Connection Settings")]
    public class FIMConnectionCredentials : ConnectionCredentials
    {
        //ConnectionCredentials credentials = new ConnectionCredentials();

        [ActivityInput]
        public new String UserName
        {
            get { return base.UserName; }
            set { base.UserName = value; }
        }

        [ActivityInput]
        public new String Domain
        {
            get { return base.Domain; }
            set { base.Domain = value; }
        }

        [ActivityInput("FIM Server")]
        public new String FIMServer
        {
            get { return base.FIMServer; }
            set { base.FIMServer = value; }
        }

        [ActivityInput(PasswordProtected = true)]
        public new String Password
        {
            get { return base.Password; }
            set { base.Password = value; }
        }   
    }
}

