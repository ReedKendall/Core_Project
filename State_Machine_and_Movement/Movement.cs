using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	public float speed;
	public float accel;
	public float jump;
	public Rigidbody2D rb;
	public GameObject core;
	public Vector2 dir;
	public bool grounded;
	public bool jumping;
	bool moving;

	void Start () {
		speed = 5.0f;
		accel = 9.81f;
		jump = 150f;
		moving = false;
		rb = gameObject.GetComponent<Rigidbody2D> ();
		core = GameObject.FindGameObjectWithTag ("Planet");
	}


	void FixedUpdate() {
		JumpCheck ();
		Inputs ();
		Gravity ();
	}

	public void Inputs() {
		if (Input.GetKey (KeyCode.D)) {
			gameObject.transform.Translate (Vector2.right * speed * Time.deltaTime);
			moving = true;
		} else if (Input.GetKey (KeyCode.A)) {
			gameObject.transform.Translate (Vector2.left * speed * Time.deltaTime);
			moving = true;
		} else {moving = false;}

		if(moving == true) {
			Vector2 dir = core.transform.position - gameObject.transform.position;
			float angle = (Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg) + 90;
			gameObject.transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
		}
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Planet") {
			grounded = true;
		}
	}

	public void JumpCheck() {
		if (grounded == true) {
			if (Input.GetKey (KeyCode.Space)) {
				for (int i = 0; i < 2; i++) {
					rb.AddForce (Vector2.up * jump * Time.deltaTime);
					grounded = false;
					moving = true;
				}
			}
		}
	}

	public void Jump() {
		
	}

	public void Gravity() {
		Vector2 ppos = new Vector2 (core.transform.position.x, core.transform.position.y);
		Vector2 opos = new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y);
		rb.AddForce((ppos - opos).normalized * accel);

	}
		
}
