using UnityEngine;
using System.Collections;

public class YellowPlayerI_Script : MonoBehaviour {

	public static string yellowPlayerI_ColName;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == TagHolder.BLOCKS) 
		{
			yellowPlayerI_ColName = col.gameObject.name;
			if (col.gameObject.name.Contains(TagHolder.YELLOW_HOUSE)) {
				SoundManagerScript.safeHouseAudioSource.Play ();
			}
		}
	}

	void Start () 
	{
		yellowPlayerI_ColName = "none";
	}
}
