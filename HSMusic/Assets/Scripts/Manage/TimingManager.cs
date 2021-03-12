using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> boxNoteList = new List<GameObject>();
    public Transform Center = null;
    public RectTransform[] timingRect = null;
    Vector2[] timingBoxs = null;

    //타이밍에 이펙트 발생
    EffectManager theEffect;
    ComboManager theComboManager;
    ScoreManager theScoreManager;
    StageManager theStageManager;
    PlayerController thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        theEffect = FindObjectOfType<EffectManager>();
        theScoreManager = FindObjectOfType<ScoreManager>();
        theComboManager = FindObjectOfType<ComboManager>();
        theStageManager = FindObjectOfType<StageManager>();
        thePlayer = FindObjectOfType<PlayerController>();

        //타이밍 박스 생성
        timingBoxs = new Vector2[timingRect.Length];
        for(int i=0;i<timingRect.Length;i++)
        {
            timingBoxs[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2,
                              Center.localPosition.x + timingRect[i].rect.width / 2);
        }

    }
    
    public bool  CheckTiming()
    {
        for(int i=0;i<boxNoteList.Count;i++)
        {
            float t_notePosX = boxNoteList[i].transform.localPosition.x;
            for(int x=0;x<timingBoxs.Length;x++)
            {
                if (timingBoxs[x].x <= t_notePosX && t_notePosX <= timingBoxs[x].y)
                {
                    //노트제거
                    boxNoteList[i].GetComponent<Note>().HideNote();
                    boxNoteList.RemoveAt(i);
                   
                    //이펙트 연출
                    if(x<3)
                        theEffect.NoteHitEffect();
                   
                    if (CheckCanNextPlate())
                    {
                        //점수 증가
                        theScoreManager.IncreaseScore(x);
                        theStageManager.ShowNextPlate(); //판때기 등장
                        theEffect.JudgementEffect(x); //판정연츨
                    }
                    else // 밟은판때기 밟음
                    {
                        theEffect.JudgementEffect(5);

                    }

                    return true;
                }

            }

        }
        theComboManager.ResetCombo();
        theEffect.JudgementEffect(4);
        return false;
    }

    bool CheckCanNextPlate()
    {
        if(Physics.Raycast(thePlayer.destPos,Vector3.down, out RaycastHit t_hitinfo, 1.1f))
        {
            if(t_hitinfo.transform.CompareTag("BasicPlate"))
            {
                BasicPlate t_plate = t_hitinfo.transform.GetComponent<BasicPlate>();
                if(t_plate.flag)
                {
                    t_plate.flag = false;
                    return true;
                }
            }
        }
        return false;

    }

}
