using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

	[Header("General")]
	[Tooltip("In ms^-1")][SerializeField] float controlSpeed = 20f;
	[Tooltip("In m")][SerializeField] float xRange = 4f;
	[Tooltip("In m")][SerializeField] float yRange = 4f;
	[SerializeField] GameObject[] guns;

	[Header("Screen-Position Based")]
	[SerializeField] float positionPitchFactor = -5f;
	[SerializeField] float positionYawFactor = 5f;

	[Header("Control-throw Based")]
	[SerializeField] float controlPitchFactor = -20f;
	[SerializeField] float controlRollFactor = -20f;

	float xThrow, yThrow;
	bool isControlEnabled = true;

	// Update is called once per frame
	void Update () {
		if (isControlEnabled)
		{
			ProcessTranslation();
			ProcessRotation();
			ProcessFiring();
		}
	}

	private void ProcessRotation()
	{
		float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
		float pitchDueToControlThrow = yThrow * controlPitchFactor;
		float pitch = pitchDueToPosition + pitchDueToControlThrow;
		
		float yawDueToPosition = transform.localPosition.x * positionYawFactor;
		float yaw = yawDueToPosition;
		
		float rollDueToControlThrow = xThrow * controlRollFactor;
		float roll = rollDueToControlThrow;
		
		transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
	}

	private void ProcessTranslation()
	{
		xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
		float xOffset = xThrow * controlSpeed * Time.deltaTime;
		float rawXPos = transform.localPosition.x + xOffset;
		float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
		
		yThrow = CrossPlatformInputManager.GetAxis("Vertical");
		float yOffset = yThrow * controlSpeed * Time.deltaTime;
		float rawYPos = transform.localPosition.y + yOffset;
		float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

		transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
	}

	void OnPlayerDeath() //called by string reference
	{
		isControlEnabled = false;
		print("Controls stopped");
	}

	void ProcessFiring()
    {
		if (CrossPlatformInputManager.GetButton("Fire1"))
        {
			SetGunsActive(true);
        }
		else
        {
			SetGunsActive(false);
        }
    }

	private void SetGunsActive(bool isActive)
    {
		foreach (GameObject gun in guns)
        {
			var emissionModule = gun.GetComponent<ParticleSystem>().emission;
			emissionModule.enabled = isActive;
        }
    }
}
