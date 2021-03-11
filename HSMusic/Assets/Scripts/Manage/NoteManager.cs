using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
  
    public int bpm = 0;
    double currentTime = 0d;
    public Transform tfNoteAppear = null;
    public GameObject goNote = null;

    TimingManager theTimingManager;
    EffectManager theEffectManager;

    private void Start()
    {
        theTimingManager = GetComponent<TimingManager>();
        theEffectManager = FindObjectOfType<EffectManager>();
    }


    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime>=60d/bpm)
        {
            GameObject t_note = Instantiate(goNote, tfNoteAppear.position, Quaternion.identity);
            t_note.transform.SetParent(this.transform);
            theTimingManager.boxNoteList.Add(t_note);
            currentTime -= 60d / bpm;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Note"))
        {
            //노트를 앞에서는 이미지만 삭제하기때문. 이미지가 켜진상태로 오면 틀린것. 꺼진상태로오면 맞춘것 
            if(collision.GetComponent<Note>().GetNoteFlag())
                theEffectManager.JudgementEffect(4);
            theTimingManager.boxNoteList.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }

}
