using PlayerCharacter.PlayerStateMachine.ScriptableObjectsBase;
using Projectile;
using UnityEngine;

namespace PlayerCharacter.PlayerStateMachine.AttackSO {
    [CreateAssetMenu(fileName = "Magic Bullet", menuName = "Player Logic/Attack Logic/Bullet")]
    public class MagicBullet : PlayerAttackSO {
        [SerializeField] private Transform bullet;
        [SerializeField] private float speed;
        [SerializeField] private float damageAmount;
        [SerializeField] private float attackRange;
        [SerializeField] private float coolDown;
        private float _lastAttacked = -999f;
        public override void Initialize(GameObject gameObject, Player player) {
            base.Initialize(gameObject, player);
            player.attackZone.radius = attackRange;
            player.attackZone.isTrigger = true;
        }

        public override void EnterState() {
            base.EnterState();
        }

         public override void FrameUpdate() {
            base.FrameUpdate();
            if (Player.InAttackRange && Time.time > (_lastAttacked + coolDown)) {
                _lastAttacked = Time.time;
                Transform handle = Instantiate(bullet, GameObject.transform.position, Quaternion.identity);
                if (Player.TargetedEnemy != null) {
                    handle.LookAt(Player.TargetedEnemy.transform);
                }
                //handle.GetComponent<Bullet>().DamageAmount = damageAmount;
                //handle.GetComponent<Bullet>().BulletSpeed = speed;
            }
        }

        
    }
}