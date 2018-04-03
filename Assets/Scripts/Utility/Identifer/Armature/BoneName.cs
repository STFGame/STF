using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility.Enums;

namespace Utility.Identifer
{
    public class BoneName
    {
        private string[] headNames = { "head", "skull" };

        private string[] hipNames = { "hip" };
        private string[] thighNames = { "thigh" };
        private string[] kneeNames = { "knee" };
        private string[] calfNames = { "calf" };
        private string[] footNames = { "foot", "feet" };

        private string[] shoulderNames = { "shoulder" };
        private string[] bicepNames = { "bicep" };
        private string[] elbowNames = { "elbow" };
        private string[] forearmNames = { "forearm" };
        private string[] handNames = { "hand", "fist" };

        private string[] leftNames = { "left", "l" };
        private string[] rightNames = { "right", "r" };

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
                    for (int j = 0; j < rightNames.Length; j++)
                    {
                        if (boneName.Contains(rightNames[j]))
                            side = "Right";
                        else if (boneName.Contains(leftNames[j]))
                            side = "Left";
                    }
                    return true;
                }
            }
            return false;
        }

        private string[] Body(BoneType boneType)
        {
            string[] bodyArea = null;
            switch(boneType)
            {
                case BoneType.Head:
                    bodyArea = headNames;
                    break;
                case BoneType.Hip:
                    bodyArea = hipNames;
                    break;
                case BoneType.Thigh:
                    bodyArea = thighNames;
                    break;
                case BoneType.Knee:
                    bodyArea = kneeNames;
                    break;
                case BoneType.Calf:
                    bodyArea = calfNames;
                    break;
                case BoneType.Foot:
                    bodyArea = footNames;
                    break;
                case BoneType.Shoulder:
                    bodyArea = shoulderNames;
                    break;
                case BoneType.Elbow:
                    bodyArea = elbowNames;
                    break;
                case BoneType.Forearm:
                    bodyArea = forearmNames;
                    break;
                case BoneType.Hand:
                    bodyArea = handNames;
                    break;
                default:
                    break;
            }
            return bodyArea;
        }
    }
}
