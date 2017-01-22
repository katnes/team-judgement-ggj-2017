using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public TextMesh text;
	public GameObject deathCrate;
	//public GameObject bloodTrigger;
	public float gravitySpeed;
	public GameObject bloodSplat;
	public GameObject deathScream1;
	public GameObject deathScream2;
	public GameObject deathScream3;
	public GameObject squish; 
	public GameObject crowdCheer; 
	public RatingSlider ratingSlider;

	public enum GameState {INIT, CINEMATIC1, ACTION, CINEMATIC2, FINAL_PAUSE};
	public GameState gameState = GameState.INIT;

	public GameObject peasant;
	int prisonerNum = 1;
	const int maxPrisonerNum = 5;

	int rand;
	bool screaming = false;
	bool wasKilled = true;

	public float countdownTime = 10.0f;
	public float timePassedInGameState = 0.0f;

	// Use this for initialization
	void Start () {
		squish.SetActive (false);
		deathCrate.SetActive (false);
		crowdCheer.SetActive (false);
		Physics.gravity = new Vector3 (0, gravitySpeed * -1, 0);
		//peasant = GameObject.Find("Character_Knight_Black");
		prisonerNum = 1;
	}

	void changeGameState(GameState newState) {
		gameState = newState;
		timePassedInGameState = 0.0f;
	}

	// Update is called once per frame
	void Update () {

		timePassedInGameState += Time.deltaTime;

		if (gameState == GameState.INIT) {
			// Initialize the loop
			// TODO: Play "next prisoner" sound
			// TODO: Activate next prisoner prefab
			if (prisonerNum == 1) {
				peasant = GameObject.Find ("Character_Knight_Black");
				ratingSlider.prisoner.background = -5;
				ratingSlider.prisoner.action = -5;
				ratingSlider.prisoner.renown = -5;
			} else if (prisonerNum == 2) {
				//peasant = GameObject.Find ("Character_Knight_Black");
				ratingSlider.prisoner.background = -5;
				ratingSlider.prisoner.action = -5;
				ratingSlider.prisoner.renown = -5;
			} else if (prisonerNum == 3) {
				//peasant = GameObject.Find ("Character_Knight_Black");
				ratingSlider.prisoner.background = -5;
				ratingSlider.prisoner.action = -5;
				ratingSlider.prisoner.renown = -5;
			} else if (prisonerNum == 4) {
				//peasant = GameObject.Find ("Character_Knight_Black");
				ratingSlider.prisoner.background = -5;
				ratingSlider.prisoner.action = -5;
				ratingSlider.prisoner.renown = -5;
			} else if (prisonerNum == 5) {
				//peasant = GameObject.Find ("Character_Knight_Black");
				ratingSlider.prisoner.background = -5;
				ratingSlider.prisoner.action = -5;
				ratingSlider.prisoner.renown = -5;
			}

			// Loop back around prisoners
			++prisonerNum;
			if (prisonerNum > maxPrisonerNum)
				prisonerNum = 1;
			
			peasant.SetActive (true);

			// TODO: After walk animation, activate prisoner idle animation

			changeGameState (GameState.CINEMATIC1);
		}
		// TODO: When CINEMATIC1 is over, go to ACTION
		else if (gameState == GameState.CINEMATIC1) {

			changeGameState (GameState.ACTION);
		} else if (gameState == GameState.ACTION) {

			countdownTime -= Time.deltaTime;
			int remainingSeconds = Mathf.CeilToInt (countdownTime);
			if (remainingSeconds < 0)
				remainingSeconds = 0;
			ratingSlider.countdownText.text = "" + remainingSeconds;

			if (Input.GetKeyDown (KeyCode.D)) {
				//run kill code
				changeGameState (GameState.CINEMATIC2);
				wasKilled = true;
				ratingSlider.shiftPopularity (wasKilled);
				text.text = "Killed";

				rand = Random.Range (0, 3);
				if (screaming == false) {
					switch (rand) {
					case 0:
						deathScream1.SetActive (true);
						break;
					case 1:
						deathScream2.SetActive (true);
						break;
					case 2:
						deathScream3.SetActive (true);
						break;
					default:
						Debug.Log ("something is wrong");
						break;
					}
				}
			}

			if (Input.GetKeyDown (KeyCode.A) || countdownTime < 0.0f) {
				//run let live code
				changeGameState (GameState.FINAL_PAUSE);
				wasKilled = false;
				ratingSlider.shiftPopularity (wasKilled);
				text.text = "Let live";

				//TODO: Play sound of boos
			}
		} else if (gameState == GameState.CINEMATIC2) {
			
			if (timePassedInGameState > 2.0f) {

				if (wasKilled) {
					deathCrate.SetActive (true);
					//Physics.gravity = new Vector3 (0, gravitySpeed * -1, 0);
				} else {
					Debug.Log ("Inside CINEMATIC2 when spared!");
					// What do we do if you spare them?  For now, should never be here
				}
				changeGameState(GameState.FINAL_PAUSE);
			}
		} else if (gameState == GameState.FINAL_PAUSE) {
			if (timePassedInGameState > 3.0f) {
				reset();
			}
		}

		// Check final game loss conditions
		if (ratingSlider.kingdom.popularity <= 0) {
			// TODO: lose
		}
	}

	void reset() {

		squish.SetActive (false);
		deathCrate.transform.position = new Vector3 (deathCrate.transform.position.x, 10.0f, deathCrate.transform.position.z);
		deathCrate.SetActive (false);

		crowdCheer.SetActive (false);
		bloodSplat.SetActive (false);
		text.text = "";
		ratingSlider.ratingText.text = "";

		screaming = false;
		wasKilled = true;
		countdownTime = 10.0f;

		changeGameState(GameState.INIT);
	}
}
