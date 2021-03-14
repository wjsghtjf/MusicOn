using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPlate : MonoBehaviour
{
    public double speed = 1;
    NoteManager noteManager;
    BoxCollider boxCol;
    private void Start()
    {
        noteManager = FindObjectOfType<NoteManager>();
        boxCol = GetComponent<BoxCollider>();
        boxCol.size = new Vector3(0.8f, 1f, 0.8f);

    }



    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            noteManager.bpm *= speed;
        }

    }


}
