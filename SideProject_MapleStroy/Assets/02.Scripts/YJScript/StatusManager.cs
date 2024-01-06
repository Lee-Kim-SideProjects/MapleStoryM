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
        public float currentEXP = 0; //현재 경험치
        private float exp; //총 경험치량

        public int stat_STR; //능력치
        public int stat_DEX;
        public int stat_LUK;
        public int stat_INT;

        public Image expBar; //경험치 바
        public TextMeshProUGUI LvText; //레벨 표시
        public TextMeshProUGUI LvPersent; //경험치바 %표시

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

        void CalculateEXP()// 필요 경험치 계산
        {
            if (level <= 10)
                exp = level * 15;
            else if (level > 10 && level <= 20)
                exp = level * 20;
        }

        public void SumEXP(float PlusEXP)// 경험치 들어오면 계산
        {
            UnityEngine.Debug.Log(PlusEXP);
            currentEXP += PlusEXP;
            UpdateExpBar();
        }

        void LevelUP()// 레벨업 계산
        {
            if(currentEXP >= exp)
            {
                currentEXP -= exp;
                level++;
                CalculateEXP();
            }

            LvText.text = level.ToString(); //레벨 동기화
        }

        void UpdateExpBar()// 경험치바 업데이트
        {
            LevelUP();
            expBar.fillAmount = currentEXP / exp;
        }
    }
}