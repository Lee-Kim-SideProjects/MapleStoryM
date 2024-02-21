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

        //스테이터스
        public string nickName;
        internal float maxHP = 50; // internal = 같은 어셈블리에서만 접근 가능
        public float currentHP = 50;
        internal float maxMP = 30;
        public float currentMP = 30;
        public int level = 1;
        public float currentEXP = 0; //현재 경험치
        private float exp; //총 경험치량

        //능력치
        public int stat_STR;
        public int stat_DEX;
        public int stat_LUK;
        public int stat_INT;
        
        private float LvPercent; //경험치 백분율 계산

        //텍스트
        private TextMeshProUGUI nickNameText;
        private TextMeshProUGUI HPText;
        private TextMeshProUGUI MPText;
        private TextMeshProUGUI LvText;
        private TextMeshProUGUI LvPercentText; //경험치바 %표시

        //이미지 바
        public Image hpBar;
        public Image mpBar;
        public Image expBar;

        //레벨업 이펙트
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
            //StatusManager의 닉네임으로 교체
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

        void CalculateEXP()// 필요 경험치 계산
        {
            if (level <= 10)
                exp = level * 15;
            else if (level > 10 && level <= 20)
                exp = level * 20;
        }

        public void SumEXP(float PlusEXP)// 경험치 들어오면 계산
        {
            //UnityEngine.Debug.Log(PlusEXP);
            currentEXP += PlusEXP;
            UpdateExpBar();
        }

        void LevelUP()// 레벨업 계산
        {
            if(currentEXP >= exp)
            {
                currentEXP -= exp;
                level++;
                anim.SetTrigger("isLevelUp");
                CalculateEXP();
            }

            LvText.text = level.ToString(); //레벨 동기화
            LvPercent = currentEXP / exp * 100f; //경험치 백분율 계산
            LvPercentText.text = currentEXP.ToString() + " [ " + LvPercent.ToString() + "% ]"; //경험치바 %표시

            StatusUP();
        }

        void UpdateExpBar()// 경험치바 업데이트
        {
            LevelUP();
            expBar.fillAmount = currentEXP / exp;
        }

        void UpdateStatus() //나중에 HP,MP 감소하는 함수 구현시 Update문에서 빼고 그 함수로 이동 예정
        {
            hpBar.fillAmount = currentHP / maxHP;
            mpBar.fillAmount = currentMP / maxMP;
            HPText.text = currentHP.ToString() + " / " + maxHP.ToString();
            MPText.text = currentMP.ToString() + " / " + maxMP.ToString();
        }

        void StatusUP() //HP, MP 레벨 증가 및 초기화
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
                // 경험치 30% 감소
                currentEXP = currentEXP * 0.5f;
            }
        }
    }
}