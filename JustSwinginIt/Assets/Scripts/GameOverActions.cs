using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class GameOverActions : MonoBehaviour
{   
	//0a. The PivotCounter, currently an Empty GameObject.
	public bool levelOver = false;
	[SerializeField] public PivotCounter PivotCounter;
	
    public GameOverScreen GameOverScreen;
    private void OnTriggerEnter2D(Collider2D collision){
    if (collision.GetComponent<Collider2D>()!=null && levelOver==false){
        AnalyticsResult analyticsResult = Analytics.CustomEvent(
            "Player died",
            new Dictionary<string,object>{
                {"Level", 1},
                {"Position", transform.position.x}
            }
        );

		//0b. Call PivotCounter to record data.
		levelOver = true;
		PivotCounter.PivotCounterBegin();
		
        UnityEngine.Debug.Log("Death log:"+analyticsResult);
        GameOverScreen.Setup();	
	}
	}
}
