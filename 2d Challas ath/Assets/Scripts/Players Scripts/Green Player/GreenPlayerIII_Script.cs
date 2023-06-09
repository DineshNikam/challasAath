﻿using UnityEngine;
using System.Collections;

public class GreenPlayerIII_Script : MonoBehaviour {

	public static string greenPlayerIII_ColName;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == TagHolder.BLOCKS) 
		{
			greenPlayerIII_ColName = col.gameObject.name;
			if (col.gameObject.name.Contains(TagHolder.GREEN_HOUSE)) {
				SoundManagerScript.safeHouseAudioSource.Play ();
			}
		}
	}

	void Start () 
	{
		greenPlayerIII_ColName = "none";
	}
}
