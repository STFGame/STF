using Controller.Mechanism;
using Entity.Animation;
using Entity.Components;
using System;
using UnityEngine;

/* MOVEMENT
 * Sean Ryan
 * March 26, 2018
 * 
 * This is the base class for movement options. There are other classes that derive from this one.
 */

namespace Entity.Motions
{
    [Serializable]
    public class Movement
    {
        [SerializeField] protected Vivacity vivacity = new Vivacity();

        [SerializeField] protected float speed;
        [SerializeField] protected float aerialSpeed;
        [SerializeField] protected float rotationSpeed;

        protected bool isTurning;

        protected IUnit unit;
        protected IControl control;

        public Movement()
        {
            vivacity.ParameterName = "Speed";
        }

        public virtual void Initialize(IUnit unit, IControl control)
        {
            this.unit = unit;
            this.control = control;

            vivacity.Initialize(unit.Animator);
        }

        //Function for updating the class
        public virtual void OnUpdate()
        {
            isTurning = HasTurned(unit.Transform.forward.x, control.Lever.Horizontal);

            vivacity.SetFloat(control.Lever.AbsoluteHorizontal);
        }

        //Used to determine if the character is turning
        protected bool HasTurned(float forwardDirection, float inputDirection)
        {
            return (forwardDirection * inputDirection < 0f);
        }
        #region Properties
        public virtual float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public virtual bool IsTurning
        {
            get { return isTurning; }
        }

        public virtual float AerialSpeed
        {
            get { return aerialSpeed; }
            set { aerialSpeed = value; }
        }

        public virtual float RotationSpeed
        {
            get { return rotationSpeed; }
            set { rotationSpeed = value; }
        }
        #endregion
    }
}
