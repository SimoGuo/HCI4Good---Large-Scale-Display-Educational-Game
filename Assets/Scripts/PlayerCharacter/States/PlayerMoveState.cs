using UnityEngine;

namespace PlayerCharacter.States {
    public class PlayerMoveState : PlayerState {
        public PlayerMoveState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine) {
            
        }
        public override void EnterState() {
            base.EnterState();
            player.PlayerMoveInstance.EnterState();
        }

        public override void ExitState() {
            base.ExitState();
            player.PlayerMoveInstance.ExitState();
        }

        public override void FrameUpdate() {
            base.FrameUpdate();
            player.Anim.Play("walk");
            player.PlayerMoveInstance.FrameUpdate();
        }
        
        private void Move() {
            if (player.NeedsTarget) return;
        
            player.transform.LookAt(new Vector3(player.Target.x, player.transform.position.y, player.Target.z));
            if (Vector3.Distance(player.transform.position, player.Target) < player.PlayerMoveInstance.StoppingDistance) {
                player.rb.velocity = Vector3.zero;
            }
            else {
                if (player.rb.velocity.magnitude <= player.PlayerMoveInstance.MaxSpeed) {
                    player.rb.AddForce(player.transform.forward * player.PlayerMoveInstance.Speed, ForceMode.Acceleration);
                }
            }

        }

        public override void PhysicsUpdate() {
            base.PhysicsUpdate();
            Move();
            player.PlayerMoveInstance.PhysicsUpdate();
        }

    }
}