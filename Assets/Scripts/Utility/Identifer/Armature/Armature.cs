using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility.Identifer
{
    public class Armature
    {
        private static BoneName boneName = new BoneName();

        public static bool Search(string name)
        {
            if (boneName.SearchHead(name))
                return true;
            else if (boneName.SearchHip(name))
                return true;
            else if (boneName.SearchElbow(name))
                return true;
            else if (boneName.SearchHand(name))
                return true;
            else if (boneName.SearchKnee(name))
                return true;
            else if (boneName.SearchFoot(name))
                return true;
            else
                return false;
        }

        public static bool Search(string name, out string side)
        {
            side = "NULL";
            if (boneName.SearchHead(name))
                return true;
            else if (boneName.SearchHip(name))
                return true;
            else if (boneName.SearchElbow(name))
                return true;
            else if (boneName.SearchHand(name))
                return true;
            else if (boneName.SearchKnee(name))
                return true;
            else if (boneName.SearchFoot(name))
                return true;
            else
                return false;
        }
    }
}
