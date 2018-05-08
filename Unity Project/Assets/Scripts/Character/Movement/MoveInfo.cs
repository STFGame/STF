using UnityEngine;

[CreateAssetMenu(fileName = "MovementInfo", menuName = "Movement", order = 1)]
public class MoveInfo : ScriptableObject
{
    public ForceMode forceMode = ForceMode.VelocityChange;
    public Speed slow = new Speed(5f, 10f, 5f, 5f);
    public Speed normal = new Speed(10f, 15f, 10f, 5f);
    public Speed fast = new Speed(15f, 20f, 15, 5f);
    public Speed aerial = new Speed(20f, 20f, 15f, 5f);
}
