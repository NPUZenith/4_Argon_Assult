using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // ok as long as this is the only script that loads scenes.

public class CollisionHandler : MonoBehaviour {

	[Tooltip("In seconds")][SerializeField] float levelLoadDelay = 1f;

	[Tooltip("Drag in a game object that should be triggered at death")][SerializeField] GameObject deathFX;

	void OnTriggerEnter(Collider collider)
	{
		print("Collided");
		StartDeathSequence();
	}

	private void StartDeathSequence()
	{
		SendMessage("OnPlayerDeath");
		deathFX.SetActive(true);
		Invoke("ReloadGame", levelLoadDelay);
	}

	private void ReloadGame() //string reference
	{
		SceneManager.LoadScene(1);
	}
}
