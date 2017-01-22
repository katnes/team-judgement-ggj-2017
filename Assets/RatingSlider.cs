using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatingSlider : MonoBehaviour {

	public Slider ratingSlider;
	public Text ratingText;
	public Text countdownText;
	public Image fillImg;
	public Kingdom kingdom;
	public Prisoner prisoner;

	public int count = 0;
	public float remainingTime = 10.0f;
	public bool stopCountdown = false;

	private Color middleColor = Color.yellow;
	private Color lowColor = Color.red;
	private Color highColor = Color.green;

	// Use this for initialization
	void Start () {
		ratingText.text = "";
		countdownText.text = "";
		fillImg.color = middleColor;
		ratingSlider.value = kingdom.popularity / 100.0f;
	}

	public void shiftPopularity(bool wasKilled) {
		// Determine how much the popularity will shift based on your decision
		int popShift;
		if (wasKilled) {
			popShift = prisoner.calcKillPopularityShift();
		}
		else {
			popShift = prisoner.calcSparePopularityShift();
		}

		// Change the kingdom popularity and score bar
		kingdom.popularity += popShift;
		if (kingdom.popularity > 100)
			kingdom.popularity = 100;
		ratingSlider.value = kingdom.popularity / 100.0f;

		// Set the text showing how your kingdom popularity score has changed
		string prefix = "";
		if (popShift > 0)
			prefix = "+";
		ratingText.text = prefix + popShift;
		countdownText.text = "";

		// Change color of slider (goes from low color, to middle color, to high color)
		if (ratingSlider.value < 0.5f)
			fillImg.color = Color.Lerp(lowColor, middleColor, ratingSlider.value * 2);
		else if (ratingSlider.value > 0.5f)
			fillImg.color = Color.Lerp(middleColor, highColor, (ratingSlider.value - 0.5f) * 2);
		else
			fillImg.color = middleColor;
	}

	// Update is called once per frame
	void Update () {

		/*
		// Countdown timer
		remainingTime -= Time.deltaTime;
		int remainingSeconds = Mathf.CeilToInt (remainingTime);
		if (remainingSeconds < 0)
			remainingSeconds = 0;
		if (stopCountdown)
			countdownText.text = "";
		else
			countdownText.text = "" + remainingSeconds;

		// Change slider based on key presses
		if(Input.GetKeyDown(KeyCode.D)){
			//run kill code
			////shiftPopularity(true);
			stopCountdown = true;
		}
		if (Input.GetKeyDown(KeyCode.A)) {
			//run let live code
			////shiftPopularity(false);
			stopCountdown = true;
		}
		*/

		/*
		// (DEBUG): "Wait" and change slider randomly once every few frames
		++count;
		if (count % 2 != 0)
			return;
		count = 0;
		// Randomly change slider value with associated text
		if (Random.value < 0.5f) {
			ratingSlider.value += 0.05f;
			ratingText.text = "+5";
		}
		else {
			ratingSlider.value -= 0.05f;
			ratingText.text = "-5";
		}
		*/

	}
}
