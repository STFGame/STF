using Survival;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Survival
{
    [CreateAssetMenu(fileName = "Life Token", menuName = "Life Token Regular", order = 6)]
    public class LifeTokenRegular : LifeTokenDisplay
    {
        [SerializeField] private string lifeTokenPath = null;

        private GameObject lifeToken = null;
        private Animator tokenAnimator = null;

        public override void Load(Transform parent)
        {
            lifeToken = (Instantiate(Resources.Load(lifeTokenPath), parent)) as GameObject;
            tokenAnimator = lifeToken.GetComponent<Animator>();
        }

        public override void Destroy(bool destroy)
        {
        }
    }
}
