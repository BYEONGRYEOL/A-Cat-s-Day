using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isometric
{
    public class AttackBox_Player : MonoBehaviour
    {
        public float attackDamage = 3f;
        // Start is called before the first frame update
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyHitBox"))
            {
                Enemy_HP enemy_hp = collision.gameObject.GetComponentInParent<Enemy_HP>();
                enemy_hp.HP_Changed(-attackDamage);
                Debug.Log("Attack Box ÀÎ½Ä");
                
                
            }

        }
    } 
}
