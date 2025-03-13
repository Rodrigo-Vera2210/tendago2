using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace TendaGo.Common
{
    public static class Logger
    {
        private static ILog Log { get; set; }

        static Logger()
        {
            Log = LogManager.GetLogger(typeof(Logger));
        }

        public static void Error(this object me, object msg)
        {
            LogManager.GetLogger(me.GetType()).Error(msg);
        }

        public static void Error(this object me, object msg, Exception ex)
        {
            LogManager.GetLogger(me.GetType()).Error(msg);
        }

        public static void Error(this object me, Exception ex)
        {
            LogManager.GetLogger(me.GetType()).Error(ex.Message, ex);
        }

        public static void Info(this object me, object msg)
        {
            LogManager.GetLogger(me.GetType()).Info(msg);
        } 

    }
}
