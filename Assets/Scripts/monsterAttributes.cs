using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class monsterAttributes : MonoBehaviour
{
	public IStatElement mainStat;
	public IStatElement secondaryStat;
	public List<IStatElement> extraStats;
	private int mainMinStrength;
	private int mainMaxStrength;
	private int secondaryMinStrength;
	private int secondaryMaxStrength;
	private int extraMinStrength;
	private int extraMaxStrength;
	private int remainingCombatStat;

	IStatElement generateStat (List<IStatElement> availableElements, int minStrength, int maxStrength)
	{
		int elementIndex = UnityEngine.Random.Range (0, availableElements.Count);
		IStatElement element = availableElements [elementIndex];
		availableElements.RemoveAt (elementIndex);
		element.Strength = UnityEngine.Random.Range (minStrength, maxStrength + 1);
		remainingCombatStat -= element.Strength;
		return element;
	}

	void generateMainStat (List<IStatElement> availableElements)
	{
		mainStat = generateStat (availableElements, mainMinStrength, mainMaxStrength);
	}

	void generateSecondaryStat (List<IStatElement> availableElements)
	{
		secondaryStat = generateStat (availableElements, secondaryMinStrength, secondaryMaxStrength);
	}

	void generateExtraStats (List<IStatElement> availableElements)
	{
		extraStats = new List<IStatElement> ();
		for (int i = availableElements.Count - 1; i > -1; i--) {
			int totalRemainingExtra = availableElements.Count * extraMaxStrength;

			int remainingDiff = totalRemainingExtra - remainingCombatStat;
			if (remainingDiff > (extraMaxStrength - extraMinStrength)) {
				remainingDiff = extraMaxStrength - extraMinStrength;
			}

			int minStrength = extraMaxStrength - remainingDiff;

			int maxStrength = remainingCombatStat > extraMaxStrength ? extraMaxStrength : remainingCombatStat;
			IStatElement element = generateStat (availableElements, minStrength, maxStrength);
			extraStats.Add (element);
		}
	}

	void LogStat (IStatElement stat, string statType)
	{
		Debug.LogFormat ("{0} - {1} Element: {2}, strength: {3}", this.gameObject.tag, statType, stat, stat.Strength);
	}

	// Use this for initialization
	void Start ()
	{


		switch (this.gameObject.tag) {
		case "levelOne":
			Debug.Log (this.gameObject.tag);

			mainMinStrength = 3;
			mainMaxStrength = 5;
			secondaryMinStrength = 1;
			secondaryMaxStrength = 3;
			extraMinStrength = 0;
			extraMaxStrength = 2;

			break;
		case "levelTwo":
			Debug.Log (this.gameObject.tag);
			mainMinStrength = 4;
			mainMaxStrength = 6;
			secondaryMinStrength = 2;
			secondaryMaxStrength = 4;
			extraMinStrength = 1;
			extraMaxStrength = 3;
			break;
		case "levelThree":
			Debug.Log (this.gameObject.tag);
			break;
		case "levelFour":
			Debug.Log (this.gameObject.tag);
			break;
		case "levelFive":
			Debug.Log (this.gameObject.tag);
			break;
		}

		remainingCombatStat = mainMinStrength + secondaryMinStrength + 3 * extraMaxStrength;


		List<IStatElement> availableElements = new List<IStatElement> ();
		availableElements.Add (new FireElement ());
		availableElements.Add (new EarthElement ());
		availableElements.Add (new WindElement ());
		availableElements.Add (new WaterElement ());
		availableElements.Add (new MagicElement ());

		generateMainStat (availableElements);
		generateSecondaryStat (availableElements);
		generateExtraStats (availableElements);




		LogStat (mainStat, "Main");
		LogStat (secondaryStat, "Secondary");
		for (int i = 0; i < extraStats.Count; i++) {
			LogStat (extraStats [i], "Extra");
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}


