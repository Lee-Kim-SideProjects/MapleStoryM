using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusManager : MonoBehaviour
{
    public static StatusManager Instance { get; private set; }

    //�������ͽ�
    public string nickName;
    internal float maxHP = 50; // internal = ���� ����������� ���� ����
    public float currentHP = 50;
    internal float maxMP = 30;
    public float currentMP = 30;

    //�ɷ�ġ
    public int stat_STR;
    public int stat_DEX;
    public int stat_LUK;
    public int stat_INT;

    //�ؽ�Ʈ
    private TextMeshProUGUI nickNameText;
    private TextMeshProUGUI HPText;
    private TextMeshProUGUI MPText;

    //�̹��� ��
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
        //StatusManager�� �г������� ��ü
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

    void UpdateStatus() //���߿� HP,MP �����ϴ� �Լ� ������ Update������ ���� �� �Լ��� �̵� ����
    {
        hpBar.fillAmount = currentHP / maxHP;
        mpBar.fillAmount = currentMP / maxMP;
        HPText.text = currentHP.ToString() + " / " + maxHP.ToString();
        MPText.text = currentMP.ToString() + " / " + maxMP.ToString();
    }

    public void StatusUP() //HP, MP ���� ���� �� �ʱ�ȭ
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
            // ����ġ 30% ����
            levelController.currentEXP = levelController.currentEXP * 0.5f;
        }
    }
}