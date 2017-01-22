using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class GameManager : MonoBehaviour {

	public GameObject deathCrate;
	//public GameObject bloodTrigger;
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

	//VR Things
	public bool deathWave;
	public bool mercyWave;

	//setting up prisoner sounds
	 public AudioSource[] pSounds;
	 public AudioSource intro;
	 public AudioSource plea;
	 public AudioSource crowd01;
	 public AudioSource crowd02;
	 public AudioSource kill;
	 public AudioSource spare;

	public enum GameState {INIT, NEXT_WALK, CINEMATIC1, ACTION, CINEMATIC2, FINAL_PAUSE};
	public GameState gameState = GameState.INIT;

	// Set up Characters
	public GameObject Cobbler;
	public GameObject Smith;
	public GameObject Noble;
	public GameObject Soldier;
	public GameObject Wanderer;
	public GameObject peasant;
	int prisonerNum = 1;
	const int maxPrisonerNum = 5;

	int rand;
	bool screaming = false;
	bool wasKilled = true;

	public float countdownTime = 20.0f;
	public float timePassedInGameState = 0.0f;

	//animator setup
	private Animator anim;
	private Animator crowd1;
	private Animator crowd2;
	private Animator crowd3;
	private Animator crowd4;
	private Animator crowd5;
	private Animator crowd6;
	private Animator crowd7;
	private Animator crowd8;
	private Animator crowd9;
	private Animator crowd10;

	//vr script setup
	private VRTK_InteractableObject vrDeathScript;
	private VRTK_InteractableObject vrMercyScript;

	// Use this for initialization
	void Start () {
		squish.SetActive (false);
		deathCrate.SetActive (false);
		crowdCheer.SetActive (false);
		Physics.gravity = new Vector3 (0, gravitySpeed * -1, 0);
		prisonerNum = 1;
		countdownTime = 20.0f;
		crowd1 = GameObject.Find ("Crowd1").GetComponent<Animator>();
		crowd2 = GameObject.Find ("Crowd2").GetComponent<Animator>();
		crowd3 = GameObject.Find ("crowd3").GetComponent<Animator>();
		crowd4 = GameObject.Find ("crowd4").GetComponent<Animator>();
		crowd5 = GameObject.Find ("crowd5").GetComponent<Animator>();
		crowd6 = GameObject.Find ("crowd6").GetComponent<Animator>();
		crowd7 = GameObject.Find ("crowd7").GetComponent<Animator>();
		crowd8 = GameObject.Find ("Crowd8").GetComponent<Animator>();
		crowd9 = GameObject.Find ("Crowd9").GetComponent<Animator>();
		crowd10 = GameObject.Find ("Crowd10").GetComponent<Animator>();
		vrDeathScript = GameObject.Find ("Death").GetComponent<VRTK_InteractableObject>();
		vrMercyScript = GameObject.Find ("Mercy").GetComponent<VRTK_InteractableObject>();
	}

	void changeGameState(GameState newState) {
		gameState = newState;
		timePassedInGameState = 0.0f;
	}

	/*
	//coroutine Test	
	IEnumerator TestCoroutine()
	{
		yield return new WaitUntil (() => !intro.isPlaying);
		plea.Play();
	}
	*/

	// Update is called once per frame
	void Update () {

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
				peasant = GameObject.Find ("Cobbler");
				anim = peasant.GetComponent<Animator> ();
				//set sounds
				pSounds = peasant.GetComponents<AudioSource> ();
				intro = pSounds [5];
				plea = pSounds [4];
				crowd01 = pSounds [2];
				crowd02 = pSounds [3];
				kill = pSounds [1];
				spare = pSounds [0];
				//set popularity
				ratingSlider.prisoner.background = -5;
				ratingSlider.prisoner.action = -5;
				ratingSlider.prisoner.renown = -5;
			} else if (prisonerNum == 2) {
				Cobbler.SetActive (false);
				Soldier.SetActive (true);
				peasant = GameObject.Find ("Soldier");
				anim = peasant.GetComponent<Animator> ();
				//set sounds
				pSounds = peasant.GetComponents<AudioSource> ();
				intro = pSounds [5];
				plea = pSounds [4];
				crowd01 = pSounds [2];
				crowd02 = pSounds [3];
				kill = pSounds [1];
				spare = pSounds [0];
				//set popularity
				ratingSlider.prisoner.background = 10;
				ratingSlider.prisoner.action = -5;
				ratingSlider.prisoner.renown = 10;
			} else if (prisonerNum == 3) {
				Soldier.SetActive (false);
				Wanderer.SetActive (true);
				peasant = GameObject.Find ("Wanderer");
				anim = peasant.GetComponent<Animator> ();
				//set sounds
				pSounds = peasant.GetComponents<AudioSource> ();
				intro = pSounds [5];
				plea = pSounds [4];
				crowd01 = pSounds [2];
				crowd02 = pSounds [3];
				kill = pSounds [1];
				spare = pSounds [0];
				//set popularity
				ratingSlider.prisoner.background = -10;
				ratingSlider.prisoner.action = 30;
				ratingSlider.prisoner.renown = 10;
			} else if (prisonerNum == 4) {
				Wanderer.SetActive (false);
				Smith.SetActive (true);
				peasant = GameObject.Find ("Smith");
				anim = peasant.GetComponent<Animator> ();
				//set sounds
				pSounds = peasant.GetComponents<AudioSource> ();
				intro = pSounds [5];
				plea = pSounds [4];
				crowd01 = pSounds [2];
				crowd02 = pSounds [3];
				kill = pSounds [1];
				spare = pSounds [0];
				//set popularity
				ratingSlider.prisoner.background = -10;
				ratingSlider.prisoner.action = 0;
				ratingSlider.prisoner.renown = 20;
			} else if (prisonerNum == 5) {
				Smith.SetActive (false);
				Noble.SetActive (true);
				peasant = GameObject.Find ("Noble");
				anim = peasant.GetComponent<Animator> ();
				//set sounds
				pSounds = peasant.GetComponents<AudioSource> ();
				intro = pSounds [5];
				plea = pSounds [4];
				crowd01 = pSounds [2];
				crowd02 = pSounds [3];
				kill = pSounds [1];
				spare = pSounds [0];
				//set popularity
				ratingSlider.prisoner.background = -10;
				ratingSlider.prisoner.action = -20;
				ratingSlider.prisoner.renown = -10;
			}

			// Loop back around prisoners
			++prisonerNum;
			if (prisonerNum > maxPrisonerNum)
				//TODO: Turn this into the game over win condition
				prisonerNum = 1;
			
			peasant.SetActive (true);

			//After walk animation, change state
			anim.Play ("Walk");
			changeGameState (GameState.NEXT_WALK);
			
		
		} 
		else if (gameState == GameState.NEXT_WALK && timePassedInGameState > 3.0f) {
			intro.Play ();
			//prisoner idle animation
			anim.Play ("Kneel");
			changeGameState (GameState.CINEMATIC1);
		}
		// TODO: When CINEMATIC1 is over, go to ACTION - think this is OK?
		else if (gameState == GameState.CINEMATIC1 && timePassedInGameState > (intro.clip.length + 0.5f)) {
			plea.Play();
			crowd01.PlayDelayed(1.0f);
			crowd02.PlayDelayed(2.0f);

			changeGameState (GameState.ACTION);
		} else if (gameState == GameState.ACTION) {

			countdownTime -= Time.deltaTime;
			int remainingSeconds = Mathf.CeilToInt (countdownTime);
			if (remainingSeconds < 0)
				remainingSeconds = 0;
			ratingSlider.countdownText.text = "" + remainingSeconds;

			/*
			//playing audio clips
			if (intro.isPlaying)
				{
				plea.PlayDelayed(.5f);
				//else StartCoroutine(TestCoroutine());
				crowd01.PlayDelayed(.5f + plea.clip.length);
				crowd02.PlayDelayed(.5f + plea.clip.length + crowd01.clip.length);
				}
			*/
							
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

				int popShift = ratingSlider.calcPopularityShift(wasKilled);
				if (popShift < 0) {
					crowdMad ();
				} 
				else {
					crowdExcite ();
				}

				/*
				rand = Random.Range (0, 3);
				if (screaming == false && !kill.isPlaying) {
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
				*/
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

				//TODO: Figure out if crowd should be happy or sad based on popularity score and choice
				int popShift = ratingSlider.calcPopularityShift(wasKilled);
				if (popShift < 0) {
					crowdMad ();
					crowdBoo.SetActive (true);
				} 
				else {
					crowdExcite ();
					crowdCheer.SetActive (true);
				}
				//crowdBoo.SetActive (true);
				//crowdanim.Play("Mad");
			}
		} else if (gameState == GameState.CINEMATIC2) {
			
			if (timePassedInGameState > (kill.clip.length - 0.5f)) {
				kill.Stop ();
				rand = Random.Range (0, 3);
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
			if (timePassedInGameState > 1.0f) {
				if (wasKilled) {
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
			// TODO: lose
		}
	}

	void reset() {

		Debug.Log ("reset");
		squish.SetActive (false);
		deathCrate.transform.position = new Vector3 (deathCrate.transform.position.x, 10.0f, deathCrate.transform.position.z);
		deathCrate.SetActive (false);

		crowdCheer.SetActive (false);
		crowdBoo.SetActive (false);

		nextPris.SetActive (false);
		bloodSplat.SetActive (false);
		ratingSlider.ratingText.text = "";

		screaming = false;
		wasKilled = true;
		countdownTime = 20.0f;

		crowdIdle ();

		changeGameState(GameState.INIT);
	}

	void crowdMad() {
		crowd1.Play ("Mad");
		crowd2.Play ("Mad");
		crowd3.Play ("Mad");
		crowd4.Play ("Mad");
		crowd5.Play ("Mad");
		crowd6.Play ("Mad");
		crowd7.Play ("Mad");
		crowd8.Play ("Mad");
		crowd9.Play ("Mad");
		crowd10.Play ("Mad");
	}

	void crowdExcite() {
		crowd1.Play ("Excite");
		crowd2.Play ("Excite");
		crowd3.Play ("Excite");
		crowd4.Play ("Excite");
		crowd5.Play ("Excite");
		crowd6.Play ("Excite");
		crowd7.Play ("Excite");
		crowd8.Play ("Excite");
		crowd9.Play ("Excite");
		crowd10.Play ("Excite");
	}

	void crowdIdle() {
		crowd1.Play ("Idle");
		crowd2.Play ("Idle");
		crowd3.Play ("Idle");
		crowd4.Play ("Idle");
		crowd5.Play ("Idle");
		crowd6.Play ("Idle");
		crowd7.Play ("Idle");
		crowd8.Play ("Idle");
		crowd9.Play ("Idle");
		crowd10.Play ("Idle");
	}
}
