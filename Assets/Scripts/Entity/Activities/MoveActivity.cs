using System;

/* MOVEMENT
 * Sean Ryan
 * March 26, 2018
 * 
 * This is the base class for movement options. There are other classes that derive from this one.
 */

namespace Actor.Activities
{
    [Serializable]
    public class MoveActivity : Activity
    {
        public MoveActivity() { }

        //Function for updating the class
        public override void OnUpdate()
        {
            base.OnUpdate();
        }

        #region Properties
        public override float Speed { get { return speed; } }

        public override float RotationSpeed { get { return rotationSpeed; } }
        #endregion
    }
}
