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
            //����Ʈ �ִϸ��̼�
            anim.SetTrigger("isAttack");
            //���� ���� Ű��
            attackCollider.enabled = true;
            Invoke("OffAttack", 0.1f);
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                //������ �ֱ�
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
