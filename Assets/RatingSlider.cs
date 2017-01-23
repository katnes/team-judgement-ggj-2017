using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatingSlider : MonoBehaviour {

	public Slider ratingSlider;
	public Slider backgroundDiffSlider;
	public Text ratingText;
	public Text countdownText;
	public Image ratingFillImg;
	public Image backgroundDiffFillImg;
	public Kingdom kingdom;
	public Prisoner prisoner;

	public float currentSliderValue;
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
		currentSliderValue = kingdom.popularity / 100.0f;
		backgroundDiffSlider.value = ratingSlider.value = currentSliderValue;
		ratingFillImg.color = getFillColor ();
		backgroundDiffFillImg.color = ratingFillImg.color;
	}

	public int calcPopularityShift(bool wasKilled) {
		if (wasKilled) {
			return prisoner.calcKillPopularityShift();
		}
		else {
			return prisoner.calcSparePopularityShift();
		}
	}

	public void shiftPopularity(bool wasKilled) {
		// Determine how much the popularity will shift based on your decision
		int popShift = calcPopularityShift(wasKilled);

		// Change the kingdom popularity
		kingdom.popularity += popShift;
		if (kingdom.popularity > 100)
			kingdom.popularity = 100;

		currentSliderValue = kingdom.popularity / 100.0f;
			
		// Visually show the difference on the score bar
		// If the popularity went down, fill the background slider with red and move the rating slider down
		// If it went up, fill the background slider with green and move it up to the new rating
		Color fillColor = getFillColor ();
		if (popShift < 0) {
			ratingSlider.value = currentSliderValue;
			backgroundDiffFillImg.color = lowColor;
			ratingFillImg.color = fillColor;
		} 
		else {
			backgroundDiffSlider.value = currentSliderValue;
			backgroundDiffFillImg.color = highColor;
		}

		// Set the text showing how your kingdom popularity score has changed
		string prefix = "";
		if (popShift > 0)
			prefix = "+";
		ratingText.text = prefix + popShift;

		// Stop the countdown text
		countdownText.text = "";

	}

	public Color getFillColor() {
		// Determine color of slider (goes from low color, to middle color, to high color)
		if (currentSliderValue < 0.5f)
			return Color.Lerp(lowColor, middleColor, currentSliderValue * 2);
		else if (currentSliderValue > 0.5f)
			return Color.Lerp(middleColor, highColor, (currentSliderValue - 0.5f) * 2);
		else
			return middleColor;
	}

	// Update is called once per frame
	void Update () {

		/*
		// (DEBUG): "Wait" and change slider randomly once every few frames
		++count;
		if (count % 5 != 0)
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
