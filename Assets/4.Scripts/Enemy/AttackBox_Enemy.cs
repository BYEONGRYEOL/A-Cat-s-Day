using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Isometric
{
    public class AttackBox_Enemy : MonoBehaviour
    {
        [SerializeField] private float attackDamage = 3f;
        // Start is called before the first frame update
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerHitBox"))
            {
                Player_HP player_HP = collision.gameObject.GetComponentInParent<Player_HP>();
                player_HP.HP_Changed(-attackDamage);
                Debug.Log("enemy Attack Box ÀÎ½Ä");
                

            }

        }
    }

}