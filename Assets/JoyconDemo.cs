﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyconDemo : MonoBehaviour {
	
	private List<Joycon> joycons;

    // Values made available via Unity
    public float[] stick;
    public Vector3 gyro;
    public Vector3 accel;
    public int jc_ind = 0;
    public Quaternion orientation;
    private Quaternion aim_offset;

    public AudioSource gunshot_clip;

	public GameObject destroyedPrefab;
	public LaserSight laserSight;  // Reference to the LaserSight script
    void Start ()
    {
        gyro = new Vector3(0, 0, 0);
        accel = new Vector3(0, 0, 0);
        aim_offset = Quaternion.identity;
        // get the public Joycon array attached to the JoyconManager in scene
        joycons = JoyconManager.Instance.j;
		if (joycons.Count < jc_ind+1){
			Destroy(gameObject);
		}
		
		gameObject.transform.rotation = orientation;
		gameObject.transform.Rotate(90,0,0,Space.World);
	}

    // Update is called once per frame
    void Update () {
		// make sure the Joycon only gets checked if attached
		if (joycons.Count > 0)
        {
			Joycon j = joycons [jc_ind];
			// GetButtonDown checks if a button has been pressed (not held)
            if (j.GetButtonDown(Joycon.Button.SHOULDER_2))
            {
				// Debug.Log ("Right trigger pressed");
				joycons[jc_ind].SetRumble(160, 320, 0.6f, 100);  // Short 
				laserSight.Shoot();
				gunshot_clip.Play();
            }
			// GetButtonDown checks if a button has been released
			if (j.GetButtonUp (Joycon.Button.SHOULDER_2))
			{
				// Debug.Log ("Right trigger released");
			}
			// GetButtonDown checks if a button is currently down (pressed or held)
			if (j.GetButton (Joycon.Button.SHOULDER_2))
			{
				// Debug.Log ("Right trigger held");
			}

			if (j.GetButtonDown(Joycon.Button.DPAD_RIGHT))
            {
				Debug.Log ("DPAD_RIGHT pressed");
				// GetStick returns a 2-element vector with x/y joystick components
				Debug.Log(string.Format("Stick x: {0:N} Stick y: {1:N}",j.GetStick()[0],j.GetStick()[1]));
            
				// Joycon has no magnetometer, so it cannot accurately determine its yaw value. Joycon.Recenter allows the user to reset the yaw value.
				j.Recenter ();
			}
			// GetButtonDown checks if a button has been released
			if (j.GetButtonUp (Joycon.Button.DPAD_RIGHT))
			{
				Debug.Log ("DPAD_RIGHT released");
			}
			// GetButtonDown checks if a button is currently down (pressed or held)
			if (j.GetButton (Joycon.Button.DPAD_RIGHT))
			{
				Debug.Log ("DPAD_RIGHT held");
			}

			if (j.GetButtonDown (Joycon.Button.DPAD_DOWN)) {
				Debug.Log ("Rumble");

				// Rumble for 200 milliseconds, with low frequency rumble at 160 Hz and high frequency rumble at 320 Hz. For more information check:
				// https://github.com/dekuNukem/Nintendo_Switch_Reverse_Engineering/blob/master/rumble_data_table.md

				j.SetRumble (160, 320, 0.6f, 200);

				// The last argument (time) in SetRumble is optional. Call it with three arguments to turn it on without telling it when to turn off.
                // (Useful for dynamically changing rumble values.)
				// Then call SetRumble(0,0,0) when you want to turn it off.
			}

            stick = j.GetStick();

            // Gyro values: x, y, z axis values (in radians per second)
            gyro = j.GetGyro();
            
            // Accel values:  x, y, z axis values (in Gs)
            accel = j.GetAccel();

            orientation = j.GetVector();
            
            if (j.GetButton(Joycon.Button.DPAD_UP)){
				gameObject.GetComponent<Renderer>().material.color = Color.red;
			}
			else
			{
				gameObject.GetComponent<Renderer>().material.color = Color.blue;
			}

			
	        Quaternion desiredRot = orientation;
            if (j.GetButtonDown(Joycon.Button.DPAD_RIGHT))
			{
				aim_offset = Quaternion.Inverse(desiredRot);
			}
			
			gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, aim_offset * desiredRot, 20f * Time.deltaTime);
			// https://github.com/Looking-Glass/JoyconLib/issues/8
			// gameObject.transform.Rotate(90,0,0,Space.World); 
			// Debug.Log(gameObject.transform.rotation);
        }
	    
    }
}