using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public float delay;

	private AudioSource audioSource;

	// Use this for initialization
	void Start ()
	{
		InvokeRepeating ("Fire", fireRate, delay);
		audioSource = GetComponent<AudioSource> ();
	}

	void Fire ()
	{
		Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		audioSource.Play ();
	}
}
