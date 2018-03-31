using Entity.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Utility.Identifer;

namespace Entity.Encounters
{
    public class Box
    {
        public Box()
        {

        }

        public static GameObject Create(string tag, string name, Transform parent)
        {
            GameObject obj = new GameObject();
            obj.tag = tag;
            obj.name = name;

            obj.AddComponent<SphereCollider>();
            obj.AddComponent<EntityBox>();

            obj.GetComponent<SphereCollider>().isTrigger = true;
            obj.layer = parent.gameObject.layer;

            return obj;
        }
    }
}
