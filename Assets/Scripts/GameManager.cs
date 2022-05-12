using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    GameObject[] roads;


    private GameObject lastRoad;
    private GameObject currentRoad;
    private int currentRoadIndex;

    private float limitOfCurrentRoad;

    private void Awake()
    {

        roads = GameObject.FindGameObjectsWithTag("Road");

        currentRoad = roads[0];
        lastRoad = roads[roads.Length-1];
        
        limitOfCurrentRoad = currentRoad.transform.position.z+32;
        currentRoadIndex = 0;

    }


    private void FixedUpdate()
    {
    
        if(PlayerController.Instance.transform.position.z >= limitOfCurrentRoad)
        {
            Vector3 tempPos = currentRoad.transform.position;
            tempPos.z = lastRoad.transform.position.z+32;
            currentRoad.transform.position = tempPos;

            lastRoad = currentRoad;

            int next = currentRoadIndex + 1;
            next = (next % roads.Length);
            currentRoadIndex = next;

            currentRoad = roads[currentRoadIndex];

            limitOfCurrentRoad = currentRoad.transform.position.z + 32;


        }
        
    }






}
