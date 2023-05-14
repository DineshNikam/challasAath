using UnityEngine;
using System.Collections;

public class BluePlayerI_Script : MonoBehaviour {

	public static string bluePlayerI_ColName;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == TagHolder.BLOCKS) 
		{			
			bluePlayerI_ColName = col.gameObject.name;
			if (col.gameObject.name.Contains(TagHolder.BLUE_HOUSE)) {
				SoundManagerScript.safeHouseAudioSource.Play ();
			}
		}
	}

	void Start () 
	{
		bluePlayerI_ColName = "none";
	}
}
