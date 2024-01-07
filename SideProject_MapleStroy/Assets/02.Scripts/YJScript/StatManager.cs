using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace XEntity.InventoryItemSystem
{
    public class StatManager : MonoBehaviour
    {
        public GameObject StatUI;
        public GameObject StatDetailUI;

        private string nickName;
        private string job = "�ʺ���";
        private string guild = "����";
        private int famous=0;
        private float minStatPower;
        private float maxStatPower;
        private float hp;
        private float mp;
        private int abilityPoint = 0;
        private int STR = 5;
        private int DEX = 5;
        private int INT = 5;
        private int LUK = 5;

        public TextMeshProUGUI StatText;
        public TextMeshProUGUI AbilityPointText;
        public TextMeshProUGUI StatText2;

        private int currentLevel; //StatusManagerLevel�� �� ���
        private int StatusManagerLevel; //StatusManager���� ������ level

        void Start()
        {
            // StatusManager�� �� ��������
            nickName = StatusManager.Instance.nickName;
            hp = StatusManager.Instance.maxHP;
            mp = StatusManager.Instance.maxMP;
            currentLevel = StatusManager.Instance.level;
        }

        void LateUpdate()
        {
            OpenCloseStatUI();
            UpdateStatText();
            CalculateStatPower();
            CheckLevelUp();
        }

        void UpdateStatText() // Statâ�� �۾� ������Ʈ
        {
            StatText.text = 
                nickName + "\n" + 
                job + "\n" + 
                guild + "\n" + 
                famous.ToString() + "\n" + 
                minStatPower.ToString() + " ~ " +
                maxStatPower.ToString() + "\n\n" + 
                hp.ToString() + "\n" + 
                mp.ToString();

            AbilityPointText.text = abilityPoint.ToString();

            StatText2.text = 
                STR.ToString() + "\n" + 
                DEX.ToString() + "\n" + 
                INT.ToString() + "\n" + 
                LUK.ToString();
        }

        void CalculateStatPower() //���� ���� (�ֽ���*4 + �ν���*1)     //���߿� ���� ���ݷ� �� �߰�
        {
            minStatPower = (STR * 4 + DEX) * 0.8f;
            maxStatPower = (STR * 4 + DEX) * 1.5f;
        }

        void OpenCloseStatUI() // UI �ѱ� ����
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                if(StatUI.activeSelf != true)
                    StatUI.SetActive(true);
                else
                    StatUI.SetActive(false);
            }
        }

        void CheckLevelUp() //������ �ߴ��� üũ�Ͽ� �����Ƽ ����Ʈ �߰�
        {
            StatusManagerLevel = StatusManager.Instance.level; //���� ���� ��������
            if (currentLevel != StatusManagerLevel)
            {
                abilityPoint += 5;
                currentLevel = StatusManagerLevel; //���� ���� �����ֱ�
            } 
        }

        #region Ŭ�� ��ư �Լ�
        public void OnClickCloseBtn() // X��ư���� ����
        {
            StatUI.SetActive(false);
        }

        public void OnClickViewStatDetail() // StatDetailâ �ѱ� ����
        {
            if (StatDetailUI.activeSelf != true)
                StatDetailUI.SetActive(true);
            else
                StatDetailUI.SetActive(false);
        }

        public void OnClickAutoUP() // �ڵ��й� Ű
        {
            STR += abilityPoint;
            abilityPoint = 0;
        }
        public void OnClickSTRUp() // STR ���� UPŰ
        {
            if (abilityPoint > 0)
            {
                STR += 1;
                abilityPoint -= 1;
            }
        }
        public void OnClickDEXUp() // DEX ���� UPŰ
        {
            if (abilityPoint > 0)
            {
                DEX += 1;
                abilityPoint -= 1;
            }
        }
        public void OnClickINTUp() // INT ���� UPŰ
        {
            if (abilityPoint > 0)
            {
                INT += 1;
                abilityPoint -= 1;
            }
        }
        public void OnClickLUKUp() // LUK ���� UPŰ
        {
            if (abilityPoint > 0)
            {
                LUK += 1;
                abilityPoint -= 1;
            }
        }
        #endregion
    }
}