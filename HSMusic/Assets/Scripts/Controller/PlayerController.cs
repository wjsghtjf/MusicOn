using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    TimingManager theTimingManager;

    // Start is called before the first frame update
    private void Start()
    {
        theTimingManager = FindObjectOfType<TimingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //판정 체크
            theTimingManager.CheckTiming();
        }
    }
}
