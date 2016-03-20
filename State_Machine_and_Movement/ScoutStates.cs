using UnityEngine;
using System.Collections;

public class ScoutStates : MonoBehaviour {

	public bool grounded;

	enum GameState {Idle, Duck, Jump, AirMove, FastFall, FastMoveFall, LongJump, Run, Roll, Crawl}

	GameState currentState;

	// Use this for initialization
	void Start () {
		SetCurrentState (GameState.Idle);
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
			transform.Translate (Vector2.up * 100 * Time.deltaTime);
			if (grounded == true) {
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

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Planet") {
			grounded = true;
		}
	}
}
