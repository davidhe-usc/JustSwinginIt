using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class GameOverActions : MonoBehaviour
{   
	public bool levelOver = false;
	
    public GameOverScreen GameOverScreen;
    private void OnTriggerEnter2D(Collider2D collision){
    if (collision.GetComponent<Collider2D>()!=null){
		levelOver = true;
        AnalyticsResult analyticsResult = Analytics.CustomEvent(
            "Player died",
            new Dictionary<string,object>{
                {"Level", 1},
                {"Position", transform.position.x}
            }
        );
        UnityEngine.Debug.Log("Death log:"+analyticsResult);
        GameOverScreen.Setup();
    }
}
}
