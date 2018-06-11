using Characters;
using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour
{
    public Attack characterAttack;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        characterAttack.AttackID = stateInfo.tagHash;
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }
}
