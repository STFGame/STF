using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpInfo", menuName = "Jump", order = 3)]
public class JumpInfo : ScriptableObject
{
    public float jumpHeight = 5f; 
    public float jumpDelay = 0.1f;
    public int maxJumps = 2; 
}
