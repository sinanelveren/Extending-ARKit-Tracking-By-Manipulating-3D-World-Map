using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Xsl;
using UnityEngine;

public class RotateRight : MonoBehaviour
{

	private Vector3 pos;
	private int count;
	private int way;
	
	float maxValue = 0.05f; // or whatever you want the max value to be
	float currentValue = 0f; // or wherever you want to start
	int direction = 1;
	private bool flag = true;


	
	void Update()
	{
		if (flag)
		{
			pos = transform.localPosition;
			flag = false;
		}
		currentValue = Time.deltaTime*0.25f; // or however you are incrementing the position
		if(System.Math.Abs(transform.localPosition.x - pos.x) > 0.3f) {
			 direction *= -2;
			//currentValue = 0;
			//transform.localPosition = pos;
		}

		if (transform.localPosition.x < pos.x)
		{
			direction = 1;
		}
		
		
		
		transform.Rotate(new Vector3(0f, 50f, 0f) * Time.deltaTime);
		//transform.Translate(new Vector3(0f,  currentValue, 0f));
		transform.localPosition += new Vector3(currentValue*direction,0f, 0f);

		/*	var transformPosition = transform.position;
			transformPosition.x +=50f * Time.deltaTime;

			transform.position = transformPosition;
		*/



	}
}