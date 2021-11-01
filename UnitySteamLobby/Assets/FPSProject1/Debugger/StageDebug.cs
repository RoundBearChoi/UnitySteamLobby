using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public class StageDebug : BaseDebugger
    {
        public static void Log(object message)
        {
            if (useLog)
            {
                WriteToText(message);
                _logger.Log(message);
            }
        }
    }
}