using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatManager : MonoBehaviour
{
    public static StatManager Instance { get; private set; }

    public GameObject StatUI;
    public GameObject StatDetailUI;

    private string nickName;
    private string job = "�ʺ���";
    private string guild = "����";
    private int famous = 0;
    internal float minStatPower;
    internal float maxStatPower;
    private float hp;
    private float mp;
    private int abilityPoint = 0;
    internal int STR = 5;
    internal int DEX = 5;
    internal int INT = 5;
    internal int LUK = 5;

    public TextMeshProUGUI StatText;
    public TextMeshProUGUI AbilityPointText;
    public TextMeshProUGUI StatText2;

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
        // StatusManager�� �� ��������
        nickName = StatusManager.Instance.nickName;
        hp = StatusManager.Instance.maxHP;
        mp = StatusManager.Instance.maxMP;
    }

    void LateUpdate()
    {
        OpenCloseStatUI();
        UpdateStatText();
        CalculateStatPower();
    }

    void UpdateStatText() // Statâ�� �۾� ������Ʈ
    {
        if (StatUI.activeSelf)
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
    }

    void CalculateStatPower() //���� ���� (�ֽ���*4 + �ν���*1)     //���߿� ���� ���ݷ� �� �߰�
    {
        minStatPower = (LUK * 4 + DEX) * 0.8f;
        maxStatPower = (LUK * 4 + DEX) * 1.5f;
    }

    void OpenCloseStatUI() // UI �ѱ� ����
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (StatUI.activeSelf != true)
                StatUI.SetActive(true);
            else
                StatUI.SetActive(false);
        }
    }

    public void AbilityPointUP() //LevlController���� �������� �����Ƽ ����Ʈ �߰�
    {
        abilityPoint += 5;
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
        LUK += abilityPoint;
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