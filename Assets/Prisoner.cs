using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prisoner : MonoBehaviour {

	public int background = 0;
	public int action = -5;
	public int renown = -5;
	public bool traitor = false;

	public Prisoner(int _background, int _action, int _renown, bool _traitor) {
		background = _background;
		action = _action;
		renown = _renown;
		traitor = _traitor;
	}

	public int calcKillPopularityShift() {
		return background + action + renown;
	}

	public int calcSparePopularityShift() {
		if (traitor)
			return -60;
		else return (-1 * calcKillPopularityShift());
	}


	// Use this for initialization
	void Start () {
		background = 0;
		action = -5;
		renown = -5;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/*
	public class Attributes
	{
		public int background;
		public int action;
		public int renown;

		public Attributes(int _background, int _action, int _renown) {
			background = _background;
			action = _action;
			renown = _renown;
		}

		public Attributes() {
			background = 0;
			action = 0;
			renown = 0;
		}
	};
	*/
}
