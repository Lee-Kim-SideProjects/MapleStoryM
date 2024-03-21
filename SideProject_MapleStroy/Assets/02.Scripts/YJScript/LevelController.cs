using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public int level = 1;
    public float currentEXP;
    public float plusEXP
    {   get 
        {
            return currentEXP;
        } 
        set 
        {
            currentEXP += value;
            LevelUP();
        } 
    }

    private float exp; //�� ����ġ��
    private float LvPercent; //����ġ ����� ���

    private TextMeshProUGUI LvText;
    private TextMeshProUGUI LvPercentText; //����ġ�� %ǥ��
    private Image expBar;

    //������ ����Ʈ
    public Animator LevelUp_anim;

    void Start()
    {
        LvText = GameObject.Find("LvText").GetComponent<TextMeshProUGUI>();
        LvPercentText = GameObject.Find("LvPercentText").GetComponent<TextMeshProUGUI>();
        expBar = GameObject.Find("expBar").GetComponent<Image>();

        CalculateEXP();
    }

    void CalculateEXP() // �������� �ʿ��� ����ġ ���
    {
        if (level <= 10)
            exp = level * 15;
        else if (level > 10 && level <= 20)
            exp = level * 20;
    }

    void LevelUP() // ������ ���
    {
        if (currentEXP >= exp)
        {
            currentEXP -= exp;
            level++;
            LevelUp_anim.SetTrigger("isLevelUp");
            CalculateEXP();
            StatusManager.Instance.StatusUP();
            StatManager.Instance.AbilityPointUP();
        }

        expBarUpdate();
    }

    void expBarUpdate() // ����ġ�� ������Ʈ
    {
        LvText.text = level.ToString(); //���� ����ȭ
        LvPercent = currentEXP / exp * 100f; //����ġ ����� ���
        LvPercentText.text = currentEXP.ToString() + " [ " + LvPercent.ToString() + "% ]"; //����ġ�� %ǥ��
        expBar.fillAmount = currentEXP / exp;
    }
}
