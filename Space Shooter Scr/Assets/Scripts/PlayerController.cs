using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
	public float zMin, zMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	private float nextFire;

	private Rigidbody rb;
	private AudioSource fireAudio;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		fireAudio = GetComponent<AudioSource> ();
	}

	void Update ()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			fireAudio.Play ();
		}
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (0.0f, moveVertical, moveHorizontal);

		rb.velocity = movement * speed;

		rb.position = new Vector3
		(
			0.0f,
			Mathf.Clamp (rb.position.y, boundary.yMin, boundary.yMax),
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
		);

		rb.rotation = Quaternion.Euler (rb.velocity.y * -tilt, 0.0f, rb.velocity.y * tilt);
	}
}
