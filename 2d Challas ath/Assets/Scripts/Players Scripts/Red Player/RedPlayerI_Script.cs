using UnityEngine;
using System.Collections;

public class RedPlayerI_Script : MonoBehaviour
{

    public static string redPlayerI_ColName;

    void OnTriggerEnter2D(Collider2D col)
    {
            //Debug.Log("outside IF block Code run player :" + redPlayerI_ColName+" \t co: "+col.gameObject.name);
        if (col.gameObject.tag == TagHolder.BLOCKS)
        {
            redPlayerI_ColName = col.gameObject.name;
           // Debug.Log("Code run player :" + redPlayerI_ColName+" \t co: "+col.gameObject.name);
            if (col.gameObject.name.Contains(TagHolder.RED_HOUSE))
            {
                Debug.Log("RED House reached");
                SoundManagerScript.safeHouseAudioSource.Play();
            }
        }
      
    }

    void Start()
    {
        redPlayerI_ColName = "none";
    }
}
