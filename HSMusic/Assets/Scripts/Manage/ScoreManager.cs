using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public Text txtScore = null;
    public int increaseScore = 10;
    int currentScore = 0;
    public float[] weight = null;
    public int comboBonusScore = 10;


    Animator myAnim;
    string animScoreUp = "ScoreUp";

    ComboManager theCombo;


    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        theCombo = FindObjectOfType<ComboManager>();
        currentScore = 0;
        txtScore.text = "0";
    }

    public void IncreaseScore(int p_judgemnetState)
    {
        //콤보 증가
        theCombo.IncreaseCombo();

        //콤보 보너스 점수 계산
        int t_currentCombo = theCombo.GetCurrentCombo();
        int t_bonusComboScore = (t_currentCombo / 10) * comboBonusScore;
        
        
        //가중치 계산
        int t_increaseScore =increaseScore+t_bonusComboScore;
        t_increaseScore = (int)(t_increaseScore * weight[p_judgemnetState]);

        currentScore += t_increaseScore;
        txtScore.text = string.Format("{0:#,##0}", currentScore);
        
        //애니 실행
        myAnim.SetTrigger(animScoreUp);
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

}
