using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideGrappleFlash : MonoBehaviour
{
	float flashTime = 0.25f;
	Color originalColorGrapple;
	Color originalColorGrappleRadius;
	[SerializeField] GameObject grapple;
	[SerializeField] GameObject grappleRadius;
	bool hasGrappled = false;
	int grCount = 0;
	
    void Start()
    {
        originalColorGrapple = grapple.GetComponent<SpriteRenderer>().material.color;
		Invoke("FlashColor", flashTime);
		originalColorGrappleRadius = grappleRadius.GetComponent<SpriteRenderer>().material.color;
		Invoke("FlashOpaque", flashTime);
    }

	void FlashColor(){
		grapple.GetComponent<SpriteRenderer>().material.color = new Color(0,0,1,1);
		Invoke("FlashWhite", flashTime);
	}
	
	void FlashWhite(){
		grapple.GetComponent<SpriteRenderer>().material.color = new Color(1,1,1,1);
		if(hasGrappled == false){ 
			//Player hasn't clicked on grapple yet.
			Invoke("FlashColor", flashTime);
		}
	}
	
	void FlashColorBlue(){
		grCount++;
		grappleRadius.GetComponent<SpriteRenderer>().material.color = new Color(0,1,1,1);
		Invoke("FlashOpaque", flashTime);
	}
	
	void FlashOpaque(){
		grappleRadius.GetComponent<SpriteRenderer>().material.color = new Color(0,0,0,0);
		if(grCount <= 3){
			Invoke("FlashColorBlue", flashTime);
		}
	}
	
	void Update(){
		var objScript = GetComponent<HasPivoted>();
		if (objScript.HasThisPivoted() == true){
			hasGrappled = true;
		}
	}
	
}
