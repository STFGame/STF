using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility.Enums;

namespace Utility.Identifer
{
    public class BoneName
    {
        private string[] head = { "head", "skull" };

        private string[] hip = { "hip" };
        private string[] knee = { "knee" };
        private string[] foot = { "foot", "feet" };

        private string[] elbow = { "elbow" };
        private string[] hand = { "hand", "fist" };

        private string[] left = { "left", "l" };
        private string[] right = { "right", "r" };

        public bool SearchBone(BoneType boneType, string name)
        {
            string boneName = name.ToLower();
            string[] compare = Body(boneType);

            for (int i = 0; i < compare.Length; i++)
            {
                if (boneName.Contains(compare[i]) && boneName.Contains("bone"))
                    return true;
            }
            return false;
        }

        public bool SearchBone(BoneType boneType, string name, ref string side)
        {
            string boneName = name.ToLower();
            string[] compare = Body(boneType);

            for (int i = 0; i < compare.Length; i++)
            {
                if (boneName.Contains(compare[i]) && boneName.Contains("bone"))
                {
                    for (int j = 0; j < right.Length; j++)
                    {
                        if (boneName.Contains(right[j]))
                            side = "Right";
                        else if (boneName.Contains(left[j]))
                            side = "Left";
                    }
                    return true;
                }
            }
            return false;
        }

        private string[] Body(BoneType boneType)
        {
            string[] body = null;
            switch(boneType)
            {
                case BoneType.Head:
                    body = head;
                    break;
                case BoneType.Hip:
                    body = hip;
                    break;
                case BoneType.Knee:
                    body = knee;
                    break;
                case BoneType.Foot:
                    body = foot;
                    break;
                case BoneType.Elbow:
                    body = elbow;
                    break;
                case BoneType.Hand:
                    body = hand;
                    break;
                default:
                    break;
            }
            return body;
        }
    }
}
