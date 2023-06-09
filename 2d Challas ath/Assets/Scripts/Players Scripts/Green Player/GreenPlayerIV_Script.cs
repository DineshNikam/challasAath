﻿using UnityEngine;
using System.Collections;

public class GreenPlayerIV_Script : MonoBehaviour {

	public static string greenPlayerIV_ColName;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == TagHolder.BLOCKS) 
		{
			greenPlayerIV_ColName = col.gameObject.name;
			if (col.gameObject.name.Contains(TagHolder.GREEN_HOUSE)) {
				SoundManagerScript.safeHouseAudioSource.Play ();
			}
		}
		
	}

	void Start () 
	{
		greenPlayerIV_ColName = "none";
	}
}
