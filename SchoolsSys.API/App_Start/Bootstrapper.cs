using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SchoolSys.API.App_Start
{
    public class Bootstrapper
    {
        public static void Run()
        {
            IoCConfig.Init(GlobalConfiguration.Configuration);
        }
    }
}