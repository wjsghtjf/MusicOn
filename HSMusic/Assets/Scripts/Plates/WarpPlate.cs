using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpPlate : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform warpPoint;
    public GameObject Player;
    Vector3 destPoint=new Vector3();
    StageManager  theStageManager;
    public void Start()
    {
        destPoint = new Vector3(warpPoint.position.x, warpPoint.position.y + 0.6f, warpPoint.position.z);
        theStageManager = FindObjectOfType<StageManager>();

    }



    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.transform.position = destPoint;
            theStageManager.ShowNextPlate();

        }

    }
}
