using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Road : MonoBehaviour
{



    public GameObject forwardJunctionPoint;
    public GameObject backwardJunctionPoint;


    public Road [] nextRoads;


    public Road GetRandomNext()
    {
        int r = Random.Range(0, nextRoads.Length);
        return nextRoads[r];
    }


    public bool signal;
    private bool first = true;

    private void OnTriggerStay(Collider other)
    {


        if (first && other.CompareTag("RoadTrigger"))
        {
            first = false;
            signal = true;
            Debug.Log("hi");
        }

    }


}
