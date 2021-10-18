using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasPivoted : MonoBehaviour
{
	public bool hasPivoted = false;
	public GameObject currentPivot;
	
	void OnMouseDown() 
	{
		//XXX have to click the object itself, not the area
		hasPivoted = true;
		Debug.Log("HasPivoted"+hasPivoted);
	}
	public bool HasThisPivoted(){
		return hasPivoted;
	}
    /*// Start is called before the first frame update
    void Start()
    { 
		
    }
    // Update is called once per frame
    void Update()
    {   
    }*/
}
