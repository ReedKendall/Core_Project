using UnityEngine;
using System.Collections;

public class ScoutStates : MonoBehaviour {

	public bool grounded;
	private GameObject planet;

	enum GameState {Idle, Duck, Jump, AirMove, FastFall, FastMoveFall, LongJump, Run, Roll, Crawl}

	GameState currentState;

	// Use this for initialization
	void Start () {
		SetCurrentState (GameState.Idle);
		planet = GameObject.FindGameObjectWithTag ("Planet");
	}

	void SetCurrentState(GameState state) {
		currentState = state;
	}

	
	// Update is called once per frame
	void Update () {
		Debug.Log (currentState);
		switch (currentState) {
		case GameState.Idle:
			if (Input.GetKeyDown (KeyCode.Space)) {
				grounded = false;
				SetCurrentState (GameState.Jump);
				break;
			}
			if (Input.GetKeyDown (KeyCode.A) || Input.GetKey (KeyCode.D)) {
				SetCurrentState (GameState.Run);
				Raycasting ();
				break;
			}
			if (Input.GetKeyDown (KeyCode.S)) {
				SetCurrentState (GameState.Duck);
				break;
			}
			if (Input.anyKey == false) {
				SetCurrentState (GameState.Idle);
				break;
			}
			break;
		case GameState.Duck:
			if (Input.anyKey == false) {
				SetCurrentState (GameState.Idle);
				break;
			}

			if (Input.GetKeyDown (KeyCode.Space)) {
				SetCurrentState (GameState.Jump);
				break;
			}

			if (Input.GetKeyDown (KeyCode.A) || Input.GetKey (KeyCode.D)) {
				SetCurrentState (GameState.Crawl);
				break;
			}
			break;
		case GameState.Jump:
			if (grounded == true) {
				transform.Translate (Vector2.up * 1 * Time.deltaTime);

				if (Input.anyKey == false) {
					SetCurrentState (GameState.Idle);
					break;
				}

				if (Input.GetKeyDown (KeyCode.A) || Input.GetKey (KeyCode.D)) {
					SetCurrentState (GameState.Run);
					break;
				}
			}
			if (!grounded) {
				if (Input.GetKeyDown (KeyCode.A) || Input.GetKey (KeyCode.D)) {
					SetCurrentState (GameState.AirMove);
					break;
				}

				if (Input.GetKeyDown (KeyCode.S)) {
					SetCurrentState (GameState.FastFall);
					break;
				}
				if (Input.anyKey == false) {
					SetCurrentState (GameState.Jump);
					break;
				}
			}
			break;
		case GameState.AirMove:
			if (Input.anyKey == false) {
				SetCurrentState (GameState.Jump);
				break;
			}

			if (Input.GetKeyDown (KeyCode.S)) {
				SetCurrentState (GameState.FastMoveFall);
				break;
			}
				
			if ((Input.GetKeyDown (KeyCode.A) || Input.GetKey (KeyCode.D)) && grounded == true) {
				SetCurrentState (GameState.Run);
				break;
			}
			break;
		case GameState.FastFall:
			
			break;
		case GameState.FastMoveFall:
			
			break;
		case GameState.LongJump:
			if (Input.anyKey == false) {
				SetCurrentState (GameState.Idle);
				break;
			}
			break;
		case GameState.Run:
			Raycasting ();
			Run ();
			if (Input.anyKey == false) {
				SetCurrentState (GameState.Idle);
				break;
			}
			if (Input.GetKeyDown (KeyCode.Space) && grounded == true) {
				grounded = false;
				SetCurrentState (GameState.Jump);
				break;
			}
			if (Input.GetKeyDown (KeyCode.A) || Input.GetKey (KeyCode.D)) {
				SetCurrentState (GameState.Run);
				break;
			} 
			break;
		case GameState.Roll:
			if (Input.anyKey == false) {
				SetCurrentState (GameState.Idle);
				break;
			}
			break;
		case GameState.Crawl:
			
			break;
		}
	}

	void Rotate() {
		//Vector2 pos = new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y);
		//Vector2 ppos = new Vector2 (planet.transform.position.x, planet.transform.position.y);
		//transform.rotation = Quaternion.LookRotation ((pos - ppos).normalized);
	}

	void Run() {
		if (Input.GetKey (KeyCode.A)) {
			Vector2 dir = planet.transform.position - gameObject.transform.position;
			float angle = (Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg) + 90;
			transform.Translate (Vector2.left * 10 * Time.deltaTime);
			transform.RotateAround (planet.transform.position, Vector3.forward, 100 * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.D)) {
			transform.Translate (Vector2.right * 10 * Time.deltaTime);
		}
	}

	void Raycasting() {
		if (Input.GetKeyDown (KeyCode.A)) {
			BoxCollider2D col = GetComponent<BoxCollider2D> ();
			Vector2 tpos = new Vector2 (transform.position.x, transform.position.y + col.bounds.extents.y);
			Vector2 bpos = new Vector2 (transform.position.x, transform.position.y - col.bounds.extents.y);
			RaycastHit2D thit = Physics2D.Raycast (tpos, Vector2.left, 10, LayerMask.GetMask("Environments"));
			RaycastHit2D bhit = Physics2D.Raycast (bpos, Vector2.left, 10, LayerMask.GetMask("Environments"));
			Debug.DrawRay (tpos, Vector2.left * 10, Color.white, 1);
			Debug.DrawRay (bpos, Vector2.left * 10, Color.white, 1);
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			BoxCollider2D col = GetComponent<BoxCollider2D> ();
			Vector2 tpos = new Vector2 (transform.position.x, transform.position.y + col.bounds.extents.y);
			Vector2 bpos = new Vector2 (transform.position.x, transform.position.y - col.bounds.extents.y);
			RaycastHit2D thit = Physics2D.Raycast (tpos, Vector2.left, 10, LayerMask.GetMask("Environments"));
			RaycastHit2D bhit = Physics2D.Raycast (bpos, Vector2.left, 10, LayerMask.GetMask("Environments"));
			Debug.DrawRay (tpos, Vector2.right * 10, Color.red, 1);
			Debug.DrawRay (bpos, Vector2.right * 10, Color.red, 1);
		}

	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Planet") {
			grounded = true;
		}
	}
}
