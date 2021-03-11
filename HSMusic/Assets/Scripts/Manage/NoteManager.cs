﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
  
    public int bpm = 0;
    double currentTime = 0d;
    public Transform tfNoteAppear = null;
    public GameObject goNote = null;
    TimingManager theTimingManager;

    private void Start()
    {
        theTimingManager = GetComponent<TimingManager>();        
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
            theTimingManager.boxNoteList.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }

}
