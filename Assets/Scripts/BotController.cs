using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{


    private Rigidbody rigidbody;

    [SerializeField]
    private WheelCollider frontRight, frontLeft, rearRight, rearLeft;

    [SerializeField]
    private float minVelocity = 5f, maxVelocity = 20f;

    [SerializeField]
    private float acceleration = 500f;
    [SerializeField]
    private float maxTurnAngle = 60f;
    [SerializeField]
    private float breakPower = 500f;




    private float currentAcceleration = 0f;
    private float currentBreak = 0f;
    private float currentTurnAngle = 0f;


    private void Start()
    {


        rigidbody = GetComponent<Rigidbody>();

    }


    void FixedUpdate()
    {

        Motor();
        Steer();
        Break();
        VisualWheels();
        ControlMinMaxVelocity();
        ControlRotation();

    }


    public void Motor()
    {

        currentAcceleration = acceleration * Input.GetAxis("Vertical");

        frontRight.motorTorque = currentAcceleration;
        frontLeft.motorTorque = currentAcceleration;

        //rearRight.motorTorque = currentAcceleration;
        //rearLeft.motorTorque = currentAcceleration;

    }


    public void Steer()
    {

        Vector3 target = Vector3.forward;
        Vector3 dir = target - transform.forward;

        currentTurnAngle = maxTurnAngle * dir.x;

        frontRight.steerAngle = currentTurnAngle;
        frontLeft.steerAngle = currentTurnAngle;

    }

    public void Break()
    {

        currentBreak = 0;
        if (Input.GetKey(KeyCode.Space))
        {
            currentBreak = breakPower;

        }
        frontRight.brakeTorque = currentBreak;
        frontLeft.brakeTorque = currentBreak;
        rearRight.brakeTorque = currentBreak;
        rearLeft.brakeTorque = currentBreak;


    }

    public void VisualWheels()
    {

        ApplyLocalPositionToVisuals(frontRight);
        ApplyLocalPositionToVisuals(frontLeft);
        ApplyLocalPositionToVisuals(rearRight);
        ApplyLocalPositionToVisuals(rearLeft);

    }


    public void ControlMinMaxVelocity()
    {


        float currentVel = rigidbody.velocity.magnitude;


        currentVel = Mathf.Clamp(currentVel, minVelocity, maxVelocity);


        Vector3 velocity = rigidbody.velocity.normalized * currentVel;

        rigidbody.velocity = velocity;



    }


    private float rangeX = 5f, rangeY = 359f, rangeZ = 16f;
    public void ControlRotation()
    {

        Vector3 currentRotation = transform.localRotation.eulerAngles;

        currentRotation.x = ConvertToAngle180(currentRotation.x);
        currentRotation.x = Mathf.Clamp(currentRotation.x, -rangeX, rangeX);

        currentRotation.y = ConvertToAngle180(currentRotation.y);
        currentRotation.y = Mathf.Clamp(currentRotation.y, -rangeY, rangeY);

        currentRotation.z = ConvertToAngle180(currentRotation.z);
        currentRotation.z = Mathf.Clamp(currentRotation.z, -rangeZ, rangeZ);

        transform.localRotation = Quaternion.Euler(currentRotation);




    }


    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;

    }




    public static float ConvertToAngle180(float input)
    {
        while (input > 360)
        {
            input = input - 360;
        }
        while (input < -360)
        {
            input = input + 360;
        }
        if (input > 180)
        {
            input = input - 360;
        }
        if (input < -180)
            input = 360 + input;
        return input;
    }



}
