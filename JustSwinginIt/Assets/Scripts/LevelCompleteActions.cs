using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class LevelCompleteActions : MonoBehaviour
{
	//0a. The PivotCounter, currently an Empty GameObject.
	public bool levelOver = false;
	[SerializeField] public PivotCounter PivotCounter;
	
    public LevelCompleteScreen LevelCompleteScreen;
    private void OnTriggerEnter2D(Collider2D collision){
    if (collision.GetComponent<Collider2D>()!=null && levelOver==false){
        AnalyticsResult analyticsResult = Analytics.CustomEvent("Level Won");
        
		//0b. Call PivotCounter to record data.
		levelOver = true;
		PivotCounter.PivotCounterBegin();
		
		UnityEngine.Debug.Log("Win log:"+analyticsResult);
        LevelCompleteScreen.Setup();	
    }
    // Start is called before the first frame update
    }
    // Update is called once per frame
}
