using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlate : MonoBehaviour
{
    public GameObject particle;
    // Start is called before the first frame update
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            particle.SetActive(true);
        }

    }
}
