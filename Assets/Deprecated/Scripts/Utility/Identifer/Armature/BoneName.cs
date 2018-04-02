using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility.Identifer
{
    public class BoneName
    {
        string[] foot = { "foot", "feet" };
        string[] hand = { "hand", "fist" };
        string[] head = { "head", "skull" };
        string[] knee = { "knee" };
        string[] elbow = { "elbow" };
        string[] hip = { "hip" };

        public bool SearchFoot(string name)
        {
            string boneName = name.ToLower();
            for (int i = 0; i < foot.Length; i++)
                if(boneName.Contains(foot[i]) && boneName.Contains("bone"))
                    return true;
            return false;
        }

        public bool SearchHand(string name)
        {
            string boneName = name.ToLower();
            for (int i = 0; i < hand.Length; i++)
                if (boneName.Contains(hand[i]) && boneName.Contains("bone"))
                    return true;
            return false;
        }

        public bool SearchHead(string name)
        {
            string boneName = name.ToLower();
            for (int i = 0; i < head.Length; i++)
                if (boneName.Contains(head[i]) && boneName.Contains("bone"))
                    return true;
            return false;
        }

        public bool SearchKnee(string name)
        {
            string boneName = name.ToLower();
            for (int i = 0; i < knee.Length; i++)
                if (boneName.Contains(knee[i]) && boneName.Contains("bone"))
                    return true;
            return false;
        }

        public bool SearchElbow(string name)
        {
            string boneName = name.ToLower();
            for (int i = 0; i < knee.Length; i++)
                if (boneName.Contains(elbow[i]) && boneName.Contains("bone"))
                    return true;
            return false;
        }

        public bool SearchHip(string name)
        {
            string boneName = name.ToLower();
            for (int i = 0; i < knee.Length; i++)
                if (boneName.Contains(hip[i]) && boneName.Contains("bone"))
                    return true;
            return false;
        }
    }
}
