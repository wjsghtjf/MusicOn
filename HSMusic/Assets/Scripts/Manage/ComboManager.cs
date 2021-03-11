using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ComboManager : MonoBehaviour
{
    public GameObject goComboImage = null;
    public Text txtCombo = null;

    int currentCombo = 0;

    Animator myAnim;
    string animComboUp = "ComboUp";

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        txtCombo.gameObject.SetActive(false);
        goComboImage.SetActive(false);
    }

    public void IncreaseCombo(int p_num=1)
    {
        currentCombo += p_num;
        txtCombo.text = string.Format("{0:#,##0}", currentCombo);
        if(currentCombo>2)
        {
            txtCombo.gameObject.SetActive(true);
            goComboImage.SetActive(true);

            myAnim.SetTrigger(animComboUp);
        }

    }

    public void ResetCombo()
    {
        currentCombo = 0;
        txtCombo.text = "0";
       txtCombo.gameObject.SetActive(false);
        goComboImage.SetActive(false);
    }

    public int GetCurrentCombo()
    {

        return currentCombo;
    }


}
