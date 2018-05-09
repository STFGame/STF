using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class Attack
{
    public AttackInfo[] attackInfos = null;

    public void CheckAttack(int attackID)
    {
        //if (attackID == 0)
            //return;

        for (int i = 0; i < attackInfos.Length; i++)
        {
            attackInfos[i].EnableAttack(attackID == attackInfos[i].AttackID);
        }
    }
}
