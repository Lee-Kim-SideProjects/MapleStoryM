using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusManager : MonoBehaviour
{
    public static StatusManager Instance { get; private set; }

    //스테이터스
    public string nickName;
    internal float maxHP = 50; // internal = 같은 어셈블리에서만 접근 가능
    public float currentHP = 50;
    internal float maxMP = 30;
    public float currentMP = 30;

    //능력치
    public int stat_STR;
    public int stat_DEX;
    public int stat_LUK;
    public int stat_INT;

    //텍스트
    private TextMeshProUGUI nickNameText;
    private TextMeshProUGUI HPText;
    private TextMeshProUGUI MPText;

    //이미지 바
    private Image hpBar;
    private Image mpBar;

    public LevelController levelController;

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
        hpBar = GameObject.Find("hpBar").GetComponent<Image>();
        mpBar = GameObject.Find("mpBar").GetComponent<Image>();
        levelController = FindObjectOfType<LevelController>();

        StatusUP();
        Die();
    }

    void Update()
    {
        UpdateStatus();
    }

    void UpdateStatus() //나중에 HP,MP 감소하는 함수 구현시 Update문에서 빼고 그 함수로 이동 예정
    {
        hpBar.fillAmount = currentHP / maxHP;
        mpBar.fillAmount = currentMP / maxMP;
        HPText.text = currentHP.ToString() + " / " + maxHP.ToString();
        MPText.text = currentMP.ToString() + " / " + maxMP.ToString();
    }

    public void StatusUP() //HP, MP 레벨 증가 및 초기화
    {
        maxHP += 50f * levelController.level;
        currentHP = maxHP;

        maxMP += 40f * levelController.level;
        currentMP = maxMP;
    }

    public void MonsterDamage(int damage)
    {
        currentHP -= damage;
    }

    void Die()
    {
        if (currentHP <= 0)
        {
            currentHP = 0;
            // 경험치 30% 감소
            levelController.currentEXP = levelController.currentEXP * 0.5f;
        }
    }
}