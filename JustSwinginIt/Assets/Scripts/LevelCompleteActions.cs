using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;


public class LevelCompleteActions : MonoBehaviour
{

    public LevelCompleteScreen LevelCompleteScreen;
    public float missedGrapples;

    public void Start()
    {
        AnalyticsResult levelAttempt = Analytics.CustomEvent("Level Started");
        missedGrapples = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.GetComponent<Collider2D>()!=null){
            AnalyticsResult levelComplete = Analytics.CustomEvent("Level Won");
            UnityEngine.Debug.Log("Win log: " + levelComplete);
            LevelCompleteScreen.Setup();

            AnalyticsResult otherAnalytics = Analytics.CustomEvent("Missed Grapples", new Dictionary<string, object>
            {
                {"Missed Grapples", missedGrapples}
            });
        
        }
    
    // Start is called before the first frame update
    }

    // Update is called once per frame
    
}
