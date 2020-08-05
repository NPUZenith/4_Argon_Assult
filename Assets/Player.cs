using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

	[Tooltip("In ms^-1")][SerializeField] float speed = 20f;
	[Tooltip("In m")][SerializeField] float xRange = 4f;
	[Tooltip("In m")][SerializeField] float yRange = 4f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		ProcessTranslation();
		ProcessRotation();
	}

	private void ProcessRotation()
	{
		transform.localRotation = Quaternion.Euler(30f, 30f, 0);
	}

	private void ProcessTranslation()
	{
		float xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
		float xOffset = xThrow * speed * Time.deltaTime;
		float rawXPos = transform.localPosition.x + xOffset;
		float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
		
		float yThrow = CrossPlatformInputManager.GetAxis("Vertical");
		float yOffset = yThrow * speed * Time.deltaTime;
		float rawYPos = transform.localPosition.y + yOffset;
		float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

		transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
	}
}
