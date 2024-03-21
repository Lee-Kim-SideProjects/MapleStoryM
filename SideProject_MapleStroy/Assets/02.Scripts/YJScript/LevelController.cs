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

    private float exp; //총 경험치량
    private float LvPercent; //경험치 백분율 계산

    private TextMeshProUGUI LvText;
    private TextMeshProUGUI LvPercentText; //경험치바 %표시
    private Image expBar;

    //레벨업 이펙트
    public Animator LevelUp_anim;

    void Start()
    {
        LvText = GameObject.Find("LvText").GetComponent<TextMeshProUGUI>();
        LvPercentText = GameObject.Find("LvPercentText").GetComponent<TextMeshProUGUI>();
        expBar = GameObject.Find("expBar").GetComponent<Image>();

        CalculateEXP();
    }

    void CalculateEXP() // 레벨업에 필요한 경험치 계산
    {
        if (level <= 10)
            exp = level * 15;
        else if (level > 10 && level <= 20)
            exp = level * 20;
    }

    void LevelUP() // 레벨업 계산
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

    void expBarUpdate() // 경험치바 업데이트
    {
        LvText.text = level.ToString(); //레벨 동기화
        LvPercent = currentEXP / exp * 100f; //경험치 백분율 계산
        LvPercentText.text = currentEXP.ToString() + " [ " + LvPercent.ToString() + "% ]"; //경험치바 %표시
        expBar.fillAmount = currentEXP / exp;
    }
}
