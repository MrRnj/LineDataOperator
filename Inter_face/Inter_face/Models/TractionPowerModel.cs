using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inter_face.Models
{
    public class TractionPowerModel
    {       
        //速度
        public string Speed { get; set; }
        //牵引力
        public string Power { get; set; }
        //是否为拐点
        public bool IsinflectionPoint { get; set; }
    }
}
