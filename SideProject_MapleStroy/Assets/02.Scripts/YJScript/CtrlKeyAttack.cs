using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XEntity.InventoryItemSystem
{
    public class CtrlKeyAttack : MonoBehaviour
    {
        public Animator anim;
        private int damage;
        private BoxCollider2D attackCollider;

        void Awake()
        {
            attackCollider = gameObject.GetComponent<BoxCollider2D>();
        }

        private void OnEnable()
        {
            //이펙트 애니메이션
            anim.SetTrigger("isAttack");
            //공격 범위 키기
            attackCollider.enabled = true;
            Invoke("OffAttack", 0.1f);
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                //데미지 주기
                damage = Random.Range(40, 60);
                col.GetComponent<MonsterController>().TakeDamage(damage);
            }
        }

        void OffAttack()
        {
            attackCollider.enabled = false;
        }
    }
}
