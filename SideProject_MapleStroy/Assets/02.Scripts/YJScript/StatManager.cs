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
    private string job = "초보자";
    private string guild = "없음";
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
        // StatusManager의 값 가져오기
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

    void UpdateStatText() // Stat창의 글씨 업데이트
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

    void CalculateStatPower() //스공 계산법 (주스텟*4 + 부스텟*1)     //나중에 무기 공격력 등 추가
    {
        minStatPower = (LUK * 4 + DEX) * 0.8f;
        maxStatPower = (LUK * 4 + DEX) * 1.5f;
    }

    void OpenCloseStatUI() // UI 켜기 끄기
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (StatUI.activeSelf != true)
                StatUI.SetActive(true);
            else
                StatUI.SetActive(false);
        }
    }

    public void AbilityPointUP() //LevlController에서 레벨업시 어빌리티 포인트 추가
    {
        abilityPoint += 5;
    }

    #region 클릭 버튼 함수
    public void OnClickCloseBtn() // X버튼으로 끄기
    {
        StatUI.SetActive(false);
    }

    public void OnClickViewStatDetail() // StatDetail창 켜기 끄기
    {
        if (StatDetailUI.activeSelf != true)
            StatDetailUI.SetActive(true);
        else
            StatDetailUI.SetActive(false);
    }

    public void OnClickAutoUP() // 자동분배 키
    {
        LUK += abilityPoint;
        abilityPoint = 0;
    }
    public void OnClickSTRUp() // STR 개별 UP키
    {
        if (abilityPoint > 0)
        {
            STR += 1;
            abilityPoint -= 1;
        }
    }
    public void OnClickDEXUp() // DEX 개별 UP키
    {
        if (abilityPoint > 0)
        {
            DEX += 1;
            abilityPoint -= 1;
        }
    }
    public void OnClickINTUp() // INT 개별 UP키
    {
        if (abilityPoint > 0)
        {
            INT += 1;
            abilityPoint -= 1;
        }
    }
    public void OnClickLUKUp() // LUK 개별 UP키
    {
        if (abilityPoint > 0)
        {
            LUK += 1;
            abilityPoint -= 1;
        }
    }
    #endregion
}