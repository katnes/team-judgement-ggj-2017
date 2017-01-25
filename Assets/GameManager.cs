using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

public class GameManager : MonoBehaviour {

	public GameObject deathCrate;
	public float gravitySpeed;
	public GameObject bloodSplat;
	public GameObject deathScream1;
	public GameObject deathScream2;
	public GameObject deathScream3;
	public GameObject nextPris;
	public GameObject squish; 
	public GameObject crowdCheer;
	public GameObject crowdBoo; 
	public RatingSlider ratingSlider;

	// VR Things
	public bool deathWave;
	public bool mercyWave;

	// Set up prisoner sounds
	public AudioSource[] pSounds;
	public AudioSource intro;
	public AudioSource plea;
	public AudioSource crowd01;
	public AudioSource crowd02;
	public AudioSource kill;
	public AudioSource spare;

	// Set up game state machine
	public enum GameState {INIT, NEXT_WALK, CINEMATIC1, ACTION, CINEMATIC2, FINAL_PAUSE};
	public GameState gameState = GameState.INIT;

	// Set up Characters
	public GameObject Cobbler;
	public GameObject Smith;
	public GameObject Noble;
	public GameObject Soldier;
	public GameObject Wanderer;
	public GameObject currentPrisoner;
	private int prisonerNum = 1;
	private const int maxPrisonerNum = 5;

	// Set up animators
	private Animator anim;
	private Animator[] crowdAnims;

	// Timing vars
	public float countdownTime = 20.0f;
	public float timePassedInGameState = 0.0f;

	// Private vars
	private bool wasKilled = true;
	private bool crowdRespondedToDeath = false;
	private Vector3 deathCrateOriginalPosition;
	private Vector3 prisonerOriginalPosition;

	// VR script setup
	private VRTK_InteractableObject vrDeathScript;
	private VRTK_InteractableObject vrMercyScript;

	// Use this for initialization
	void Start () {
		squish.SetActive (false);
		deathCrate.SetActive (false);
		deathCrateOriginalPosition = deathCrate.transform.position;
		wasKilled = true;
		crowdRespondedToDeath = false;
		crowdCheer.SetActive (false);
		Physics.gravity = new Vector3 (0, gravitySpeed * -1, 0);
		prisonerNum = 1;
		countdownTime = 20.0f;
		crowdAnims = new Animator[10];
		crowdAnims[0] = GameObject.Find ("Crowd1").GetComponent<Animator>();
		crowdAnims[1] = GameObject.Find ("Crowd2").GetComponent<Animator>();
		crowdAnims[2] = GameObject.Find ("crowd3").GetComponent<Animator>();
		crowdAnims[3] = GameObject.Find ("crowd4").GetComponent<Animator>();
		crowdAnims[4] = GameObject.Find ("crowd5").GetComponent<Animator>();
		crowdAnims[5] = GameObject.Find ("crowd6").GetComponent<Animator>();
		crowdAnims[6] = GameObject.Find ("crowd7").GetComponent<Animator>();
		crowdAnims[7] = GameObject.Find ("Crowd8").GetComponent<Animator>();
		crowdAnims[8] = GameObject.Find ("Crowd9").GetComponent<Animator>();
		crowdAnims[9] = GameObject.Find ("Crowd10").GetComponent<Animator>();
		vrDeathScript = GameObject.Find ("Death").GetComponent<VRTK_InteractableObject>();
		vrMercyScript = GameObject.Find ("Mercy").GetComponent<VRTK_InteractableObject>();
	}

	void changeGameState(GameState newState) {
		// When changing game states, always set timePassedInGameState back to zero
		gameState = newState;
		timePassedInGameState = 0.0f;
	}

	// Update is called once per frame
	void Update () {

		// Keep track of how much time has passed
		timePassedInGameState += Time.deltaTime;

		if (gameState == GameState.INIT) {
			// Initialize the loop
			
			mercyWave = false;
			deathWave = false;

			// Play "next prisoner" sound
			nextPris.SetActive (true);
			// Activate next prisoner prefab
			if (prisonerNum == 1) {
				Noble.SetActive (false);
				Cobbler.SetActive (true);
				currentPrisoner = Cobbler;
				//set popularity
				ratingSlider.prisoner.background = -5;
				ratingSlider.prisoner.action = -5;
				ratingSlider.prisoner.renown = -5;
			} else if (prisonerNum == 2) {
				Cobbler.SetActive (false);
				Soldier.SetActive (true);
				currentPrisoner = Soldier;
				//set popularity
				ratingSlider.prisoner.background = 10;
				ratingSlider.prisoner.action = -5;
				ratingSlider.prisoner.renown = 10;
			} else if (prisonerNum == 3) {
				Soldier.SetActive (false);
				Wanderer.SetActive (true);
				currentPrisoner = Wanderer;
				//set popularity
				ratingSlider.prisoner.background = 10;
				ratingSlider.prisoner.action = -30;
				ratingSlider.prisoner.renown = -10;
			} else if (prisonerNum == 4) {
				Wanderer.SetActive (false);
				Smith.SetActive (true);
				currentPrisoner = Smith;
				//set popularity
				ratingSlider.prisoner.background = -10;
				ratingSlider.prisoner.action = -20;
				ratingSlider.prisoner.renown = 10;
			} else if (prisonerNum == 5) {
				Smith.SetActive (false);
				Noble.SetActive (true);
				currentPrisoner = Noble;
				//set popularity
				ratingSlider.prisoner.background = 10;
				ratingSlider.prisoner.action = 20;
				ratingSlider.prisoner.renown = 10;
			}

            // Set anim and sounds from current prisoner
            anim = currentPrisoner.GetComponent<Animator> ();
			pSounds = currentPrisoner.GetComponents<AudioSource> ();
			intro = pSounds [5];
			plea = pSounds [4];
			crowd01 = pSounds [2];
			crowd02 = pSounds [3];
			kill = pSounds [1];
			spare = pSounds [0];

			//After starting walk animation, change state
			prisonerOriginalPosition = currentPrisoner.transform.position;
			anim.Play ("Walk");
			changeGameState (GameState.NEXT_WALK);
		
		} else if (gameState == GameState.NEXT_WALK) {

			// Walk from original position to 2 units forward in 3 seconds
			currentPrisoner.transform.position = Vector3.Lerp (prisonerOriginalPosition, 
				prisonerOriginalPosition + 2 * Vector3.back, 
				timePassedInGameState / 3.0f);

			if (timePassedInGameState > 3.0f) {
				// Once the prisoner has stopped walking, kneel and play the intro
				intro.Play ();
				anim.Play ("Kneel");
				changeGameState (GameState.CINEMATIC1);
			}
		}
		else if (gameState == GameState.CINEMATIC1 && 
			     (timePassedInGameState > (intro.clip.length + 0.5f) || Input.GetKeyDown(KeyCode.S))) {
			// Once the intro is over, play the plea with crowd jeers mixed in
			// Switch to the ACTION state and allow player input
			// Note: Hidden skip intro functionality by pressing 'S'
			intro.Stop();
			plea.Play();
			crowd01.PlayDelayed(1.0f);
			crowd02.PlayDelayed(2.0f);

			changeGameState (GameState.ACTION);
		} else if (gameState == GameState.ACTION) {
			// During the ACTION state, wait for a decision from the player or until the timer runs out

			// Change the countdown timer
			countdownTime -= Time.deltaTime;
			int remainingSeconds = Mathf.CeilToInt (countdownTime);
			if (remainingSeconds < 0)
				remainingSeconds = 0;
			ratingSlider.countdownText.text = "" + remainingSeconds;

			//NOW IT listens for wave of Death game object
			if (Input.GetKeyDown (KeyCode.D) || (vrDeathScript && vrDeathScript.IsTouched() == true)) {
				//run kill code
				changeGameState (GameState.CINEMATIC2);

				plea.Stop();
				crowd01.Stop();
				crowd02.Stop();

				kill.Play();
				anim.Play("Kill");
				wasKilled = true;
				ratingSlider.shiftPopularity (wasKilled);

				// Figure out if crowd should be happy or sad based on prisoner's popularity score and choice
				// At this point, only start crowd animations
				int popShift = ratingSlider.calcPopularityShift(wasKilled);
				if (popShift < 0) {
					crowdMad ();
				} 
				else {
					crowdExcite ();
				}
			}

			//NOW IT listens for wave of Mercy gameobject
			if (Input.GetKeyDown (KeyCode.A) || countdownTime < 0.0f || 
				(vrMercyScript && vrMercyScript.IsTouched() == true)) {
				//run let live code
				changeGameState (GameState.FINAL_PAUSE);

				plea.Stop();
				crowd01.Stop();
				crowd02.Stop();

				spare.Play();
				wasKilled = false;
				ratingSlider.shiftPopularity (wasKilled);
				anim.Play("Live");

				// Figure out if crowd should be happy or sad based on prisoner's popularity score and choice
				int popShift = ratingSlider.calcPopularityShift(wasKilled);
				if (popShift < 0) {
					crowdMad ();
					crowdBoo.SetActive (true);
				} 
				else {
					crowdExcite ();
					crowdCheer.SetActive (true);
				}
			}
		} else if ((gameState == GameState.CINEMATIC2) &&
			       (timePassedInGameState > (kill.clip.length - 1.0f))) {

			// Interrupt kill clip early and replace with random death scream
			kill.Stop ();
			int rand = Random.Range (0, 3);
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

			// Release the death crate!
			if (wasKilled) {
				deathCrate.SetActive (true);
			} else {
				// When you space them, it should skip the CINEMATIC2 state, so we should never get here
				Debug.Log ("Error: Inside CINEMATIC2 when spared!");
			}
			changeGameState(GameState.FINAL_PAUSE);

		} else if (gameState == GameState.FINAL_PAUSE) {
			// Everything has now been set in motion, so just waiting for it to end

			if (timePassedInGameState > 1.0f && !crowdRespondedToDeath) {
				crowdRespondedToDeath = true;
				if (wasKilled) {
					// Determine if crowd should boo or cheer after prisoner was killed
					int popShift = ratingSlider.calcPopularityShift(wasKilled);
					if (popShift < 0) {
						crowdBoo.SetActive (true);
					} 
					else {
						crowdCheer.SetActive (true);
					}
				}
			}
			if (timePassedInGameState > 5.0f) {
				reset();
			}
		}

		// Check final game loss conditions
		if (ratingSlider.kingdom.popularity <= 0) {
            // load game over scene
            SceneManager.LoadScene("losar");
        }
	}

	void reset() {
		// Reset everything so the loop can start over

		squish.SetActive (false);
		deathCrate.transform.position = deathCrateOriginalPosition;
		deathCrate.SetActive (false);

		crowdCheer.SetActive (false);
		crowdBoo.SetActive (false);

		nextPris.SetActive (false);
		bloodSplat.SetActive (false);

		// Sometimes the ratingSlider is higher, sometimes the backgroundDiffSlider is higher
		// When resetting, just set both to the current value and color
		ratingSlider.backgroundDiffSlider.value = ratingSlider.ratingSlider.value = ratingSlider.currentSliderValue;
		Color fillColor = ratingSlider.getFillColor ();
		ratingSlider.backgroundDiffFillImg.color = ratingSlider.ratingFillImg.color = fillColor;
		ratingSlider.ratingText.text = "";

		wasKilled = true;
		crowdRespondedToDeath = false;
		countdownTime = 20.0f;

		crowdIdle ();

		// Go to next prisoner (loop back if necessary)
		++prisonerNum;
		if (prisonerNum > maxPrisonerNum)
        {
			//load winning scene
                SceneManager.LoadScene("winnar");
        }

		changeGameState(GameState.INIT);
	}

	void crowdMad() {
		for (int i = 0; i < crowdAnims.Length; ++i) {
			crowdAnims [i].Play ("Mad");
		}
	}

	void crowdExcite() {
		for (int i = 0; i < crowdAnims.Length; ++i) {
			crowdAnims [i].Play ("Excite");
		}
	}

	void crowdIdle() {
		for (int i = 0; i < crowdAnims.Length; ++i) {
			crowdAnims [i].Play ("Idle");
		}
	}
}
