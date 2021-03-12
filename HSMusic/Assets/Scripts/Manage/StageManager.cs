﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject stage = null;
    Transform[] stagePlates;

    public float offsetY = -5;
    public float plateSpeed = 10;

    int stepCount = 0;
    int totalPlateCount = 0;

    void Start()
    {
        stagePlates = stage.GetComponent<Stage>().plates;
        totalPlateCount = stagePlates.Length;
        for(int i=0;i<totalPlateCount; i++)
        {

            stagePlates[i].position = new Vector3(stagePlates[i].position.x,
                                                  stagePlates[i].position.y + offsetY,
                                                  stagePlates[i].position.z);

        }
    }

    // Update is called once per frame
    public void ShowNextPlate()
    {
        if (stepCount < totalPlateCount)
            StartCoroutine(MovePlateCo(stepCount++));
    }

    IEnumerator MovePlateCo(int p_num)
    {
        stagePlates[p_num].gameObject.SetActive(true);
        Vector3 t_destPos= new Vector3(stagePlates[p_num].position.x,
                                                  stagePlates[p_num].position.y - offsetY,
                                                  stagePlates[p_num].position.z);
        while(Vector3.SqrMagnitude(stagePlates[p_num].position-t_destPos)>=0.001f)
        {
            stagePlates[p_num].position = Vector3.Lerp(stagePlates[p_num].position, t_destPos, plateSpeed * Time.deltaTime);
            yield return null;
        }
        stagePlates[p_num].position = t_destPos;
    }

}
