using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace KMSABET.MyUtilities
{
    public class LogUtils
    {
        public static readonly ILog myLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}