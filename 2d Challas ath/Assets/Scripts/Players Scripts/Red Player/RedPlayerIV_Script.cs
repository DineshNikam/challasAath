using UnityEngine;
using System.Collections;

public class RedPlayerIV_Script : MonoBehaviour {

	public static string redPlayerIV_ColName;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == TagHolder.BLOCKS) 
		{
			redPlayerIV_ColName = col.gameObject.name;
			if (col.gameObject.name.Contains(TagHolder.RED_HOUSE)) {
				SoundManagerScript.safeHouseAudioSource.Play ();
			}
		}
		
	}

	void Start () 
	{
		redPlayerIV_ColName = "none";
	}
}