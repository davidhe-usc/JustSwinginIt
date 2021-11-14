using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideGrappleFlash : MonoBehaviour
{
	float flashTime = 0.25f;
	Color originalColor;
	[SerializeField] GameObject gameObject;
	//SpriteRenderer renderer;
	bool hasGrappled = false;
	
    void Start()
    {
		//renderer = currentPivot.GetComponent<SpriteRenderer>();
        originalColor = gameObject.GetComponent<SpriteRenderer>().material.color;
		//originalColor = GetComponent<Renderer>().color;
		Invoke("FlashColor", flashTime);
    }

	void FlashColor(){
		//color
		gameObject.GetComponent<SpriteRenderer>().material.color = new Color(0,1,0,1);
		//renderer.color = Color(1f/255f, 0f/255f, 0f/255f, 1);
		Invoke("FlashWhite", flashTime);
	}
	
	void FlashWhite(){
		//white
		gameObject.GetComponent<SpriteRenderer>().material.color = new Color(1,1,1,1);
		//renderer.color = Color(1f/255f, 1f/255f, 1f/255f, 1);				
		if(hasGrappled == false){ 
			//Player hasn't clicked on grapple yet.
			Invoke("FlashColor", flashTime);
		}
	}
	
	void Update(){
		var objScript = GetComponent<HasPivoted>();
		if (objScript.HasThisPivoted() == true){
			hasGrappled = true;
		}
	}
	
}
