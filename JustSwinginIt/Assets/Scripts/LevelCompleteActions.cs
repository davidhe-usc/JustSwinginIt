using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;


public class LevelCompleteActions : MonoBehaviour
{

    public LevelCompleteScreen LevelCompleteScreen;
    private void OnTriggerEnter2D(Collider2D collision){
    if (collision.GetComponent<Collider2D>()!=null){
        AnalyticsResult analyticsResult = Analytics.CustomEvent("Level Won");
        UnityEngine.Debug.Log("Win log:"+analyticsResult);
        LevelCompleteScreen.Setup();
    }
    
    // Start is called before the first frame update
    }

    // Update is called once per frame
    
}
