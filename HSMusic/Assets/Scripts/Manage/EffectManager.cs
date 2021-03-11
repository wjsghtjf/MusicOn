using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EffectManager : MonoBehaviour
{
    public Animator noteHitAnimator = null;
    string hit = "Hit";

    public Animator judgementAnimator = null;
    public Image judgementImage = null;
    public Sprite[] judgementSprite = null;

    public void JudgementEffect(int p_num)
    {
        judgementImage.sprite = judgementSprite[p_num];
        judgementAnimator.SetTrigger(hit);
       
    }

    public void NoteHitEffect()
    {

        noteHitAnimator.SetTrigger(hit);

    }
}
