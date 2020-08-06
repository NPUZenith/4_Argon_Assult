using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[SerializeField] GameObject deathFX;
	[SerializeField] Transform parent;
	
	[SerializeField] int scorePerHit = 100;
	[SerializeField] int hits = 5;

	ScoreBoard scoreBoard;

	// Use this for initialization
	void Start () {
		scoreBoard = FindObjectOfType<ScoreBoard>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hits <= 0)
        {
            KillEnemy();
        }
    }

    private void ProcessHit()
    {
        hits--;
        // todo consider hit FX
    }

    private void KillEnemy()
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        scoreBoard.ScoreHit(scorePerHit);
        Destroy(gameObject);
    }
}
