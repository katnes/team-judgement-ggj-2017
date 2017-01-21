using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatingSlider : MonoBehaviour {

	public Slider ratingSlider;
	public Text ratingText;
	public Text countdownText;
	public Image fillImg;

	public int count = 0;
	public float remainingTime = 10.0f;
	public bool stopCountdown = false;

	private Color middleColor = Color.yellow;
	private Color lowColor = Color.red;
	private Color highColor = Color.green;

	// Use this for initialization
	void Start () {
		ratingText.text = "";
		fillImg.color = middleColor;
	}
	
	// Update is called once per frame
	void Update () {

		// Countdown timer
		remainingTime -= Time.smoothDeltaTime;
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
			ratingSlider.value -= 0.25f;
			ratingText.text = "-25";
			stopCountdown = true;
		}
		if (Input.GetKeyDown(KeyCode.A)) {
			//run let live code
			ratingSlider.value += 0.25f;
			ratingText.text = "+25";
			stopCountdown = true;
		}

		/*
		GameObject fillAreaObj = ratingSlider.transform.Find("Fill Area").gameObject;
		GameObject fillObj = fillAreaObj.transform.Find("Fill").gameObject;
		Image fillImg = fillObj.GetComponentInChildren(typeof(Image)) as Image;
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

		// Change color of slider
		if (ratingSlider.value < 0.5f)
			fillImg.color = Color.Lerp(lowColor, middleColor, ratingSlider.value * 2);
		else if (ratingSlider.value > 0.5f)
			fillImg.color = Color.Lerp(middleColor, highColor, (ratingSlider.value - 0.5f) * 2);
		else
			fillImg.color = middleColor;
		
	}
}
