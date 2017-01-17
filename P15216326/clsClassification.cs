using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P15216326
{
    [Serializable()]
    public class clsClassification
    {
        private string name;
        //not used yet
        private bool isWarmBlooded;
        //other properties would go here



        public clsClassification(string name, bool isWarmBlooded)
        {
            this.name = name;
            this.isWarmBlooded = isWarmBlooded;

        }


        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public bool IsWarmBlooded
        {
            get { return false; }
            set { isWarmBlooded = value; }
        }

    }
}
