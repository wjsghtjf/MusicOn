using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPlate : MonoBehaviour
{
    public float speed = 1;
    public bool CubeSpeedUp = false;
    NoteManager noteManager;
    BoxCollider boxCol;
    PlayerController thePlayer;
    private void Start()
    {
        noteManager = FindObjectOfType<NoteManager>();
        boxCol = GetComponent<BoxCollider>();
        thePlayer = FindObjectOfType<PlayerController>();
        boxCol.size = new Vector3(0.8f, 1f, 0.8f);

    }



    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            noteManager.bpm *= speed;
            if(CubeSpeedUp)
            {
                thePlayer.moveSpeed *= speed;
                thePlayer.recoilPosY *= (1/speed);
            }
        }

    }


}
