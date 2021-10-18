using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasPivoted : MonoBehaviour
{
	public bool hasPivoted = false;
	public GameObject currentPivot;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	void OnMouseDown()
	{
		hasPivoted = true;
		Debug.Log("HasPivoted"+hasPivoted);
	}
	bool HasThisPivoted(){
		return hasPivoted;
	}
}
