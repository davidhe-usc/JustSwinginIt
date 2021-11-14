using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideGrappleFlash : MonoBehaviour
{
	float flashTime = 2.0f;
	Color originalColor;
	GameObject currentPivot;
	MeshRenderer renderer;
	bool hasGrappled = false;
	
    void Start()
    {
		renderer = currentPivot.GetComponent<MeshRenderer>();
        originalColor = gameObject.GetComponent<MeshRenderer>().material.color;
		//originalColor = GetComponent<Renderer>().color;
		Invoke("FlashRed", flashTime);
    }

	void FlashRed(){
		//red
		gameObject.GetComponent<MeshRenderer>().material.color = new Color(1/255,0/255,0/255,1);
		//renderer.color = Color(1f/255f, 0f/255f, 0f/255f, 1);
		Invoke("FlashWhite", flashTime);
	}
	
	void FlashWhite(){
		//white
		gameObject.GetComponent<MeshRenderer>().material.color = new Color(1/255,1/255,1/255,1);
		//renderer.color = Color(1f/255f, 1f/255f, 1f/255f, 1);				
		if(hasGrappled == false){ 
			//Player hasn't clicked on grapple yet.
			Invoke("FlashRed", flashTime);
		}
	}
	
	void Update(){
		var objScript = GetComponent<HasPivoted>();
		if (objScript.HasThisPivoted() == true){
			hasGrappled = true;
		}
	}
	
}
