using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour
{

    public Text maxSpeed;
    public Text acceleration;
    public Text currentSpeed;

    private float maxSpeedCar;
    private float accelerationCar;
    private float currentSpeedCar;

   // Use this for initialization
	void Start ()
	{
	    Show();
	}
	
	// Update is called once per frame
	void Update ()
	{
        currentSpeedCar = FindObjectOfType<WheelJointCarMovement>().wheelJoints[0].motor.motorSpeed;
        currentSpeed.text = "Текущая скорость: " + Mathf.Abs(currentSpeedCar);
        Show();
    }

    void Show()
    {
        maxSpeedCar = FindObjectOfType<WheelJointCarMovement>().maxFwdSpeed;
        accelerationCar = FindObjectOfType<WheelJointCarMovement>().accelerationRate;
        maxSpeed.text = "Макс. скорость:  " + Mathf.Abs(maxSpeedCar);
        acceleration.text = "Макс. ускорение: " + accelerationCar;
    }

    public void PlusSpeed()
    {
        maxSpeedCar = FindObjectOfType<WheelJointCarMovement>().maxFwdSpeed -= 200;
    }

    public void MinusSpeed()
    {
        maxSpeedCar = FindObjectOfType<WheelJointCarMovement>().maxFwdSpeed += 200;
    }

    public void PlusAccel()
    {
        accelerationCar = FindObjectOfType<WheelJointCarMovement>().accelerationRate += 100;
    }

    public void MinusAccel()
    {
        accelerationCar = FindObjectOfType<WheelJointCarMovement>().accelerationRate -= 100;
    }

    
}
