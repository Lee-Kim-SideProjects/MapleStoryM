using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace XEntity.InventoryItemSystem
{
    public class StatusManager : MonoBehaviour
    {
        public static StatusManager Instance { get; private set; }

        //�������ͽ�
        public string nickName;
        internal float maxHP = 50; // internal = ���� ����������� ���� ����
        public float currentHP = 50;
        internal float maxMP = 30;
        public float currentMP = 30;
        public int level = 1;
        public float currentEXP = 0; //���� ����ġ
        private float exp; //�� ����ġ��

        //�ɷ�ġ
        public int stat_STR;
        public int stat_DEX;
        public int stat_LUK;
        public int stat_INT;
        
        private float LvPercent; //����ġ ����� ���

        //�ؽ�Ʈ
        private TextMeshProUGUI nickNameText;
        private TextMeshProUGUI HPText;
        private TextMeshProUGUI MPText;
        private TextMeshProUGUI LvText;
        private TextMeshProUGUI LvPercentText; //����ġ�� %ǥ��

        //�̹��� ��
        public Image hpBar;
        public Image mpBar;
        public Image expBar;

        //������ ����Ʈ
        public Animator anim;

        void Awake()
        {
            #region Singleton
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }
            #endregion
        }

        void Start()
        {
            //StatusManager�� �г������� ��ü
            nickNameText = GameObject.Find("NickNameText").GetComponent<TextMeshProUGUI>();
            nickNameText.text = nickName;
            HPText = GameObject.Find("HPText").GetComponent<TextMeshProUGUI>();
            MPText = GameObject.Find("MPText").GetComponent<TextMeshProUGUI>();
            LvText = GameObject.Find("LvText").GetComponent<TextMeshProUGUI>();
            LvPercentText = GameObject.Find("LvPercentText").GetComponent<TextMeshProUGUI>();

            CalculateEXP();
            StatusUP();
            Die();
        }

        void Update()
        {
            UpdateStatus();
        }

        void CalculateEXP()// �ʿ� ����ġ ���
        {
            if (level <= 10)
                exp = level * 15;
            else if (level > 10 && level <= 20)
                exp = level * 20;
        }

        public void SumEXP(float PlusEXP)// ����ġ ������ ���
        {
            //UnityEngine.Debug.Log(PlusEXP);
            currentEXP += PlusEXP;
            UpdateExpBar();
        }

        void LevelUP()// ������ ���
        {
            if(currentEXP >= exp)
            {
                currentEXP -= exp;
                level++;
                anim.SetTrigger("isLevelUp");
                CalculateEXP();
            }

            LvText.text = level.ToString(); //���� ����ȭ
            LvPercent = currentEXP / exp * 100f; //����ġ ����� ���
            LvPercentText.text = currentEXP.ToString() + " [ " + LvPercent.ToString() + "% ]"; //����ġ�� %ǥ��

            StatusUP();
        }

        void UpdateExpBar()// ����ġ�� ������Ʈ
        {
            LevelUP();
            expBar.fillAmount = currentEXP / exp;
        }

        void UpdateStatus() //���߿� HP,MP �����ϴ� �Լ� ������ Update������ ���� �� �Լ��� �̵� ����
        {
            hpBar.fillAmount = currentHP / maxHP;
            mpBar.fillAmount = currentMP / maxMP;
            HPText.text = currentHP.ToString() + " / " + maxHP.ToString();
            MPText.text = currentMP.ToString() + " / " + maxMP.ToString();
        }

        void StatusUP() //HP, MP ���� ���� �� �ʱ�ȭ
        {
            maxHP += level * 50;
            currentHP = maxHP;

            maxMP += level * 40;
            currentMP = maxMP;
        }

        public void MonsterDamage(int damage)
        {
            currentHP -= damage;
        }

        void Die()
        {
            if(currentHP <= 0)
            {
                currentHP = 0;
                // ����ġ 30% ����
                currentEXP = currentEXP * 0.5f;
            }
        }
    }
}