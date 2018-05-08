using Boxes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Managers
{
    public class BoxManager : MonoBehaviour
    {
        [HideInInspector] public List<GameObject> boxGameObjects = new List<GameObject>();

        private Dictionary<int, GameObject> boxDictionary = new Dictionary<int, GameObject>();

        private void Awake()
        {
            for (int i = 0; i < boxGameObjects.Count; i++)
            {
                Box box = boxGameObjects[i].GetComponent<Box>();
                RegisterBox(box.boxType, box.boxArea, boxGameObjects[i]);
            }
        }

        private void RegisterBox(BoxType key1, BoxArea key2, GameObject boxGameObject)
        {
            int hash = 0;
            Hash(ref hash, key1, key2);

            boxDictionary.Add(hash, boxGameObject);
        }

        private void Hash(ref int hashValue, BoxType key1, BoxArea key2)
        {
            switch (key1)
            {
                case BoxType.Hitbox:
                    hashValue += (int)key1 * 1;
                    break;
                case BoxType.Hurtbox:
                    hashValue += (int)key1 * 200;
                    break;
                default:
                    break;
            }

            hashValue += (int)key2;
        }

        public GameObject GetGameBox(BoxType key1, BoxArea key2)
        {
            int hash = 0;
            Hash(ref hash, key1, key2);

            if (boxDictionary.ContainsKey(hash))
                return boxDictionary[hash];
            return null;
        }

        public Box GetBox(BoxType key1, BoxArea key2)
        {
            Box box = null;
            if (GetGameBox(key1, key2) != null)
                box = GetGameBox(key1, key2).GetComponent<Box>();
            return box;
        }
    }
}