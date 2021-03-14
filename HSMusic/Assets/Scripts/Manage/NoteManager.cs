using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    bool noteActive = true;

    public double bpm = 0;
    double currentTime = 0d;
    public Transform tfNoteAppear = null;
    


    TimingManager theTimingManager;
    EffectManager theEffectManager;
    ComboManager theComboManager;

    private void Start()
    {
        theComboManager = FindObjectOfType<ComboManager>();
        theTimingManager = GetComponent<TimingManager>();
        theEffectManager = FindObjectOfType<EffectManager>();
    }


    void Update()
    {
        if (noteActive)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= 60d /bpm)
            {
                GameObject t_note = ObjectPool.instance.noteQueue.Dequeue();
                t_note.transform.position = tfNoteAppear.position;
                t_note.SetActive(true);
                theTimingManager.boxNoteList.Add(t_note);
                currentTime -= 60d / bpm;

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Note"))
        {
            //노트를 앞에서는 이미지만 삭제하기때문. 이미지가 켜진상태로 오면 틀린것. 꺼진상태로오면 맞춘것 
            if (collision.GetComponent<Note>().GetNoteFlag())
            {
                theComboManager.ResetCombo();
                theEffectManager.JudgementEffect(4);
                theTimingManager.MissRecord();
            }
            theTimingManager.boxNoteList.Remove(collision.gameObject);

            ObjectPool.instance.noteQueue.Enqueue(collision.gameObject);
            collision.gameObject.SetActive(false);
            
        }
    }

    public void RemoveNote()
    {
        noteActive = false;
        for(int i=0;i<theTimingManager.boxNoteList.Count;i++)
        {
            theTimingManager.boxNoteList[i].SetActive(false);
            ObjectPool.instance.noteQueue.Enqueue(theTimingManager.boxNoteList[i]);

        }

    }



}
