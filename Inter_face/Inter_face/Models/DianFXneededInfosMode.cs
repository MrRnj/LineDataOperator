using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inter_face.Models
{
    public class DianFXneededInfosMode
    {
        private List<string> cdlList;

        public List<string> CdlListProperty
        {
            get { return cdlList; }
            set { cdlList = value; }
        }

        private string dfxInfos;

        public string DfxInfosProperty
        {
            get { return dfxInfos; }
            set { dfxInfos = value; }
        }
        /// <summary>
        /// secnum+pos
        /// </summary>
        private string leftpos;

        public string LeftPosProperty
        {
            get { return leftpos; }
            set { leftpos = value; }
        }

        private string rightpos;

        public string RightPosProperty
        {
            get { return rightpos; }
            set { rightpos = value; }
        }       
    }
}
