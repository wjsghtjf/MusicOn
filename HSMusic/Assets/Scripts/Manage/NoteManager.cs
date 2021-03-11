using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int bpm = 0;
    double currentTime = 0d;
    public Transform tfNoteAppear = null;
    public GameObject goNote = null;
 
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime>=60d/bpm)
        {
            GameObject t_note = Instantiate(goNote, tfNoteAppear.position, Quaternion.identity);
            t_note.transform.SetParent(this.transform);
            currentTime -= 60d / bpm;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Note"))
        {
            Destroy(collision.gameObject);
        }
    }

}
