using UnityEngine;
using System.Collections;
using UnityEditor;

public class WheelJointCarMovement : MonoBehaviour
{

    public WheelJoint2D[] wheelJoints;

    public Transform centerOfMass;

    private JointMotor2D motorBack;
    //horizontal movement keyboard input
    private float dir = 0f;
    //input for rotation of the car
    private float torqueDir = 0f;

    public float maxFwdSpeed = -5000;

    public float maxBwdSpeed = 2000f;

    public float accelerationRate = 100;

    public float decelerationRate = -500;

    public float brakeSpeed = 2500f;

    private float gravity = 9.81f;

    private float slope = 0;

    public Transform rearWheel;
    public Transform frontWheel;

    private Rigidbody2D rgb;

    void Awake()
    {
        rgb = GetComponent<Rigidbody2D>();
    }

	// Use this for initialization
	void Start () {
        //set the center of mass of the car
        rgb.centerOfMass = centerOfMass.transform.localPosition;
        
	    wheelJoints = gameObject.GetComponents<WheelJoint2D>();
        //get the reference to the motor of rear wheels join
	    motorBack = wheelJoints[0].motor;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    torqueDir = Input.GetAxis("Horizontal");

	    if (torqueDir != 0)
	    {
	        rgb.AddTorque(3*Mathf.PI*torqueDir, ForceMode2D.Force);
	    }
	    else
	    {
	        rgb.AddTorque(0);
	    }
        //determine the cars angle wrt the horizontal ground
	    slope = transform.localEulerAngles.z;


        //convert the slope values greater than 180 to a negative value so as to add motor speed
        //based on slope angle
	    if (slope >= 180)
	        slope = slope - 360;

        //horizontal movement input. same as torqueDir. Could have avoided it, but decide to 
        //use it since some of you might want to use the vertical axis for the torqueDir
	    dir = Input.GetAxis("Horizontal");

	    if (dir != 0)
	        motorBack.motorSpeed =
	            Mathf.Clamp(
	                motorBack.motorSpeed -
	                (dir*accelerationRate - gravity*Mathf.Sin((slope*Mathf.PI)/180)*80)*Time.deltaTime, maxFwdSpeed,
	                maxBwdSpeed);
        //if no input and car is moving forward or no input and car is stagnant and is on an inclined plane with negative slope
        if ((dir == 0 && motorBack.motorSpeed < 0) || (dir == 0 && motorBack.motorSpeed == 0 && slope < 0))
        {
            //decelerate the car while adding the speed if the car is on an inclined plane
            motorBack.motorSpeed = Mathf.Clamp(motorBack.motorSpeed - (decelerationRate - gravity * Mathf.Sin((slope * Mathf.PI) / 180) * 80) * Time.deltaTime, maxFwdSpeed, 0);
        }
        //if no input and car is moving backward or no input and car is stagnant and is on an inclined plane with positive slope
        else if ((dir == 0 && motorBack.motorSpeed > 0) || (dir == 0 && motorBack.motorSpeed == 0 && slope > 0))
        {
            //decelerate the car while adding the speed if the car is on an inclined plane
            motorBack.motorSpeed = Mathf.Clamp(motorBack.motorSpeed - (-decelerationRate - gravity * Mathf.Sin((slope * Mathf.PI) / 180) * 80) * Time.deltaTime, 0, maxBwdSpeed);
        }

        //apply brakes to the car
        if (Input.GetKey(KeyCode.Space) && motorBack.motorSpeed > 0)
        {
            motorBack.motorSpeed = Mathf.Clamp(motorBack.motorSpeed - brakeSpeed * Time.deltaTime, 0, maxBwdSpeed);
        }
        else if (Input.GetKey(KeyCode.Space) && motorBack.motorSpeed < 0)
        {
            motorBack.motorSpeed = Mathf.Clamp(motorBack.motorSpeed + brakeSpeed * Time.deltaTime, maxFwdSpeed, 0);
        }
        //connect the motor to the joint
        wheelJoints[0].motor = motorBack;
    }
}
