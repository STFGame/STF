using Actor.Collisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility.Enums;

namespace Utility.Identifer
{
    public class Armature
    {
        private static BoneName boneName = new BoneName();

        public static bool Contains(string name)
        {
            if (boneName.SearchBone(BoneType.Head, name))
                return true;
            else if (boneName.SearchBone(BoneType.Hip, name))
                return true;
            else if (boneName.SearchBone(BoneType.Knee, name))
                return true;
            else if (boneName.SearchBone(BoneType.Foot, name))
                return true;
            else if (boneName.SearchBone(BoneType.Elbow, name))
                return true;
            else if (boneName.SearchBone(BoneType.Hand, name))
                return true;
            else
                return false;
        }

        public static bool Contains(string name, ref BodyArea bodyArea)
        {
            string side = "";
            if (boneName.SearchBone(BoneType.Head, name, ref side))
            {
                bodyArea = BodyArea.Head;
                return true;
            }
            else if (boneName.SearchBone(BoneType.Hip, name, ref side))
            {
                bodyArea = BodyArea.Hip;
                return true;
            }
            else if (boneName.SearchBone(BoneType.Knee, name, ref side))
            {
                bodyArea = (side == "Right") ? BodyArea.KneeRight : BodyArea.KneeLeft;
                return true;
            }
            else if (boneName.SearchBone(BoneType.Foot, name, ref side))
            {
                bodyArea = (side == "Right") ? BodyArea.FootRight : BodyArea.FootLeft;
                return true;
            }
            else if (boneName.SearchBone(BoneType.Elbow, name, ref side))
            {
                bodyArea = (side == "Right") ? BodyArea.ElbowRight : BodyArea.ElbowLeft;
                return true;
            }
            else if (boneName.SearchBone(BoneType.Hand, name, ref side))
            {
                bodyArea = (side == "Right") ? BodyArea.HandRight : BodyArea.HandLeft;
                return true;
            }
            //else if (boneName.SearchBone(BoneType.Bicep, name, ref side))
            //{
                //bodyArea = (side == "Right") ? BodyArea.BicepRight : BodyArea.BicepLeft;
                //return true;
            //}
            else
                return false;
        }
    }
}
