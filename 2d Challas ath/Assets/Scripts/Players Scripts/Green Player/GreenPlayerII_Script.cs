using UnityEngine;
using System.Collections;

public class GreenPlayerII_Script : MonoBehaviour {

	public static string greenPlayerII_ColName;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == TagHolder.BLOCKS) 
		{
			greenPlayerII_ColName = col.gameObject.name;
			if (col.gameObject.name.Contains(TagHolder.GREEN_HOUSE)) {
				SoundManagerScript.safeHouseAudioSource.Play ();
			}
		}
	}

	void Start () 
	{
		greenPlayerII_ColName = "none";
	}
}
