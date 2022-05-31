using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPathFollower : MonoBehaviour
{


    public float speed;
    public Transform target;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 dir = target.position - transform.position;
        dir.Normalize();

        transform.position += transform.forward * speed*Time.deltaTime;



        transform.rotation = Quaternion.LookRotation(dir*Time.deltaTime);

    }









}
