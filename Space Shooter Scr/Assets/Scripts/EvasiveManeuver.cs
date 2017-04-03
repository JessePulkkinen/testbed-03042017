using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour
{
	public float dodge;
	public float smoothing;
	public float tilt;
	public Vector2 startWait;
	public Vector2 maneuverTime;
	public Vector2 maneuverWait;
	public Boundary boundary;

	private float currentSpeed;
	private float targetManeuver;
	private Rigidbody rb;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		currentSpeed = rb.velocity.z;
		StartCoroutine (Evade ());
	}

	IEnumerator Evade()
	{
		yield return new WaitForSeconds (Random.Range (startWait.x, startWait.y));

		while (true)
		{
			targetManeuver = Random.Range (1, dodge) * -Mathf.Sign (transform.position.y);
			yield return new WaitForSeconds (Random.Range (maneuverTime.x, maneuverTime.y));
			targetManeuver = 0;
			yield return new WaitForSeconds (Random.Range (maneuverWait.x, maneuverWait.y));
		}
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		float newManeuver = Mathf.MoveTowards (rb.velocity.y, targetManeuver, Time.deltaTime * smoothing);
		rb.velocity = new Vector3 (0.0f, newManeuver, currentSpeed);
		rb.position = new Vector3
			(
				0.0f,
				Mathf.Clamp (rb.position.y, boundary.yMin, boundary.yMax),
				Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
			);
		
		rb.rotation = Quaternion.Euler (rb.velocity.y * -tilt, 0.0f, rb.velocity.y * tilt);
	}
}
