﻿using UnityEngine;
using System.Collections;

public class BlockLogic : MonoBehaviour
{
	bool suckedUp;
	RGB blockRGB;
	public GameObject destroyFX;
	
	GameObject sucker;
	float originalDistance;
	
	float distanceScaleup = 0.1f;
	
	void Start ()
	{
		blockRGB = GetComponent<RGB> ();
	}
	
	/*
	 * When a player enters the trigger for a block, it means a collision has
	 * occurred with a black block. Play the animation and perform player collision
	 * logic.
	 */
	void OnTriggerEnter (Collider other)
	{
		if (!other.CompareTag (Tags.PLAYER)) {
			Debug.LogWarning ("Block collided with non-player object. Fix this.");
			return;
		}
		Player player = other.GetComponent<Player> ();
		BlowUp (other.transform.position);
		player.CollideWithBlock ();
	}

	public void BlowUp(Vector3 position)
	{
		GameObject fx = (GameObject)Instantiate (destroyFX, transform.position,
				Quaternion.LookRotation (Vector3.forward, Vector3.up));
		FX_BlockBreak fxScript = (FX_BlockBreak)fx.GetComponent<FX_BlockBreak>();
		if(fxScript != null) {
			fxScript.Explode(position, 40);
			Destroy (fx, 3.0f);
			// Parent the explosion to the treadmill
			fx.transform.parent = transform.parent;
		}
		else {
			Destroy (fx, 0.8f);
		}
		Destroy (gameObject);
	}
}
