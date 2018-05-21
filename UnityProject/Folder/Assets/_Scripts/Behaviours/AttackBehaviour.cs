using Character;
using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour
{
    public CharacterAttack characterAttack;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        characterAttack.AttackID = stateInfo.tagHash;
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }
}
