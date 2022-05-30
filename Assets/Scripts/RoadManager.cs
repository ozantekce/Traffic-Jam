using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{

    [SerializeField]
    List<Road> roads;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    float delay = 0.1f;

    // Update is called once per frame
    void Update()
    {

        delay -= Time.deltaTime;
        if(delay < 0)
        {
            delay = 0.1f;

            Road first = roads[0];
            
            Road third = roads[2]; //sinyali bu verecek

            if (third.signal == false)
            {
                return;
            }
            third.signal = false;


            Road last = roads[roads.Count-1];

            Debug.Log("last : "+last);

            Road next = last.GetRandomNext();
            Road next_ 
                = GameObject.Instantiate(next
                ,last.forwardJunctionPoint.transform.position
                ,next.transform.rotation, transform);



            roads.Remove(first);
            GameObject.Destroy(first.gameObject);

            roads.Add(next_);

        }

    }






}
