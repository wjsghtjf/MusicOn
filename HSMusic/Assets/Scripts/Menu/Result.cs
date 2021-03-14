using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public GameObject goUI = null;
    public Text[] txtCount = null;
    public Text txtCoin = null;
    public Text txtScore = null;
    public Text txtMaxCombo = null;

    ScoreManager theScore;
    ComboManager theCombo;
    TimingManager theTiming;
    
    
    // Start is called before the first frame update
    void Start()
    {
        theScore = FindObjectOfType<ScoreManager>();
        theCombo = FindObjectOfType<ComboManager>();
        theTiming = FindObjectOfType<TimingManager>();

    }
    
    public void ShowResult()
    {
        goUI.SetActive(true);
        for (int i = 0; i < txtCount.Length; i++)
            txtCount[i].text = "0";
        txtCoin.text = "0";
        txtScore.text = "0";
        txtMaxCombo.text = "0";

        int[] t_judgement = theTiming.GetJudgementRecord();
        int t_currentScore = theScore.GetCurrentScore();
        int t_maxCombo = theCombo.GetMaxCombo();
        int t_coin = (t_currentScore / 50);

        for(int i=0;i<txtCount.Length;i++)
        {
            txtCount[i].text = string.Format("{0:#,##0}", t_judgement[i]);
        }
        txtScore.text= string.Format("{0:#,##0}", t_currentScore);
        txtMaxCombo.text = string.Format("{0:#,##0}", t_maxCombo);
        txtCoin.text = string.Format("{0:#,##0}", t_coin);
    }

}
