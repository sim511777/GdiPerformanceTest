using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GdiPerformanceTest {
    internal class Util {
        public static double GetTime() {
            return Stopwatch.GetTimestamp() / (double)Stopwatch.Frequency;
        }

        public static double GetTimeMs() {
            return GetTime() * 1000;
        }
    }
}
