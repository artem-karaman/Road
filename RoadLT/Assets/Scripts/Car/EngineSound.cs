using System;
using UnityEngine;
using System.Collections;

public class EngineSound : MonoBehaviour
{

    private AudioSource carSound;

    private const float lowPitch = 0.5f;
    private const float highPitch = 5f;

    private const float reductionFactor = .001f;

    private float userInput;

    private WheelJoint2D wj;

    void Awake()
    {
        carSound = GetComponent<AudioSource>();
        wj = GetComponent<WheelJoint2D>();
    }

	// Update is called once per frame
	void FixedUpdate ()
	{
	    userInput = Input.GetAxis("Horizontal");
	    float forwardSpeed = Mathf.Abs(wj.jointSpeed);
	    float pitchFactor = Mathf.Abs(forwardSpeed*reductionFactor*userInput);
	    carSound.pitch = Mathf.Clamp(pitchFactor, lowPitch, highPitch);
	}
}
