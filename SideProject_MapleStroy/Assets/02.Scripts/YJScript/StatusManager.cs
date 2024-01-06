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

        public float hp = 50;
        public float mp = 30;
        public int level = 1;
        public float currentEXP = 0; //���� ����ġ
        private float exp; //�� ����ġ��

        public int stat_STR; //�ɷ�ġ
        public int stat_DEX;
        public int stat_LUK;
        public int stat_INT;

        public Image expBar; //����ġ ��
        public TextMeshProUGUI LvText; //���� ǥ��
        public TextMeshProUGUI LvPersent; //����ġ�� %ǥ��

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
            CalculateEXP();
        }

        void Update()
        {

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
            UnityEngine.Debug.Log(PlusEXP);
            currentEXP += PlusEXP;
            UpdateExpBar();
        }

        void LevelUP()// ������ ���
        {
            if(currentEXP >= exp)
            {
                currentEXP -= exp;
                level++;
                CalculateEXP();
            }

            LvText.text = level.ToString(); //���� ����ȭ
        }

        void UpdateExpBar()// ����ġ�� ������Ʈ
        {
            LevelUP();
            expBar.fillAmount = currentEXP / exp;
        }
    }
}