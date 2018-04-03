using Actor.Collisions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility.Enums;
using Utility.Identifer;

namespace Actor
{
    public class ActorArmature : MonoBehaviour
    {
        [SerializeField] BodyArea[] hitboxes;
        [SerializeField] BodyArea[] attackboxes;
        private BodyArea bodyArea;

        private void Awake()
        {
            foreach (Transform item in GetComponentsInChildren<Transform>())
            {
                if (Armature.Contains(item.name, ref bodyArea))
                {
                    if (hitboxes.Contains(bodyArea))
                    {
                        Bodybox<object> hitbox = new Bodybox<object>(new Hitbox(this.gameObject.layer, bodyArea));
                        hitbox.Add<SphereCollider>();
                        hitbox.Add<ActorTrigger<Hitbox>>();
                        hitbox.SetParent(item);
                        hitbox.SetTag(UnityTag.Hitbox.ToString());
                        hitbox.SetName(bodyArea.ToString() + " Hitbox");
                        hitbox.IsTrigger(true);
                    }

                    if (attackboxes.Contains(bodyArea))
                    {
                        Bodybox<object> attackbox = new Bodybox<object>(new Attackbox(bodyArea));
                        attackbox.Add<SphereCollider>();
                        attackbox.Add<ActorTrigger<Attackbox>>();
                        attackbox.SetParent(item);
                        attackbox.SetTag(UnityTag.Attackbox.ToString());
                        attackbox.SetName(bodyArea.ToString() + " Attackbox");
                        attackbox.IsTrigger(true);
                    }
                }
            }
        }
    }
}
