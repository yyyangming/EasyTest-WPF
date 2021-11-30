using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class TrajectoryFile
    {
        public class Ghost
        {
            public string Point_StartX { get; set; }
            public string Point_StartY { get; set; }
            public string Point_StartZ { get; set; }
            public string Point_StartW { get; set; }
            public string Point_StartU { get; set; }
            public string Point_EndX { get; set; }
            public string Point_EndY { get; set; }
            public string Point_EndZ { get; set; }
            public string Point_EndW { get; set; }
            public string Point_EndU { get; set; }
            public string trajectory { get; set; }
            public double OpenCoat { get; set; }
        }
    }
}
