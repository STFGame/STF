using System;
using UnityEngine;

namespace Player
{
    [Serializable]
    class PlayerManager
    {
        public enum PlayerType { None, Player1, Player2, Player3, Player4 }
        public PlayerType player;

        [SerializeField] private Health playerHealth;
        [SerializeField] private Movement playerMove;
        [SerializeField] private Attack playerAttack;

        private Damage playerDamage = new Damage();

        public PlayerManager()
        {
            playerHealth = new Health();
            playerMove = new Movement();
            playerAttack = new Attack();

            playerDamage.OnDamageTaken += CheckEvent;
        }

        public void CheckEvent()
        {

        }

        public void UpdateInternal(float delta, Transform position)
        {
            Vector3 movement = GetAxisMovement().normalized;
            playerMove.Move(movement);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerMove.UpdateState(position);
                playerMove.Jump();
            }
        }

        //public Movement playerMovement = new Movement();
        //public Health playerHealth = new Health();
        //public Attack playerAttack = new Attack();

        //private void Start()
        //{

        //}

        //private void FixedUpdate()
        //{
        //    playerMovement.UpdateState(this.transform);

        //    playerMovement.Move(GetAxisMovement());
        //    ButtonPresses();
        //}

        //private void Update()
        //{
        //}

        //private void ButtonPresses()
        //{
        //    if (Input.GetKeyDown(KeyCode.Space))
        //        playerMovement.Jump();
        //}

        private Vector3 GetAxisMovement()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(horizontal, 0f, vertical);

            return movement;
        }

    }
}