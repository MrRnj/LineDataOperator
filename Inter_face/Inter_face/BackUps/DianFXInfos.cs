using Inter_face.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inter_face.BackUps
{
    public class DianFXInfos
    {
        public string OtherInfos;

        public List<DianFXModel> DfxS;

        public List<DianFXModel> DfxX;

        public DianFXInfos()
        {
            DfxS = new List<DianFXModel>();
            DfxX = new List<DianFXModel>();
        }
    }
}
