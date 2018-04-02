using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Player.Animation
{
    public static class AnimaHash
    {
        public static readonly int IdleHash = Animator.StringToHash("Base Layer.Idle");
        public static readonly int JumpHash = Animator.StringToHash("Base Layer.Jump");
        public static readonly int LocoHash = Animator.StringToHash("Base Layer.Locomotion");
        public static readonly int SlideForwardHash = Animator.StringToHash("Base Layer.Slide Forward");
        public static readonly int SlideBackwardsHash = Animator.StringToHash("Base Layer.Slide Backwards");
        public static readonly int TurnHash = Animator.StringToHash("Base Layer.Turn");

    }
}
