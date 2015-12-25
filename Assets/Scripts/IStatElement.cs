using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AbstractStatElement : IStatElement
{
	private int strength;

	public int Strength {
		get {
			return strength;
		}
		set {
			strength = value;
		}
	}
}



