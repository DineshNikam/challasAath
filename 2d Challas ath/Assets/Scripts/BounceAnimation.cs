using UnityEngine;
using System.Collections;

public class BounceAnimation : MonoBehaviour
{
    Vector3 initialScale;

    public void Start()
    {
         initialScale = gameObject.transform.localScale; 
    }


    public void StartAnimation()
    {
    
            iTween.MoveBy(gameObject, iTween.Hash("y", 2, "easeType", "easeInOutExpo", "loopType", "pingPong", "delay", .1, "time", 0.5f));
            //iTween.ScaleAdd(gameObject, iTween.Hash("y",2, "easeType", "easeInOutExpo", "loopType", "pingPong", "delay", .1, "time", 0.5f));
            iTween.ScaleBy(gameObject, iTween.Hash("x", 1.3f, "y", 1.3f, "time", 0.5f, "easeType", "easeInOutExpo", "loopType", "pingPong"));
        
    }
     public void StopAnimation()
    {
        
        iTween.Stop(gameObject);
        gameObject.transform.localScale = initialScale;
    }
}

