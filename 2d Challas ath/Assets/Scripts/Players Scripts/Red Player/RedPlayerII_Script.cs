using UnityEngine;
using System.Collections;

public class RedPlayerII_Script : MonoBehaviour {

	public static string redPlayerII_ColName;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == TagHolder.BLOCKS) 
		{
			redPlayerII_ColName = col.gameObject.name;
			if (col.gameObject.name.Contains(TagHolder.RED_HOUSE)) {
				SoundManagerScript.safeHouseAudioSource.Play ();
			}
		}
	}

	void Start () 
	{
		redPlayerII_ColName = "none";
	}
}