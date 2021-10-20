using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class GameOverActions : MonoBehaviour
{   
    //0a. The PivotCounter, currently an Empty GameObject.
	  public bool levelOver = false;
	  [SerializeField] public PivotCounter PivotCounter;
    public GameOverScreen GameOverScreen;
    [SerializeField] private LevelCompleteActions manager;


    private void OnTriggerEnter2D(Collider2D collision){

      if (collision.GetComponent<Collider2D>()!=null && levelOver==false){
          AnalyticsResult deathResult = Analytics.CustomEvent(
              "Player died",
              new Dictionary<string,object>{
                  {"Level", 1},
                  {"Position", transform.position.x}
              }
          );

		//0b. Call PivotCounter to record data.
		      levelOver = true;
		      PivotCounter.PivotCounterBegin();         

            AnalyticsResult otherAnalytics = Analytics.CustomEvent("Missed Grapples", new Dictionary<string, object>
            {
                {"Missed Grapples", manager.missedGrapples}
            });
            UnityEngine.Debug.Log("Player dead analytics: "+ transform.position.x);
            UnityEngine.Debug.Log("Missed Grapple analytics: "+ manager.missedGrapples);
            GameOverScreen.Setup();
            // StartCoroutine(WaitThenReload());
        }
    }

    // IEnumerator WaitThenReload()
    // {
    //     yield return new WaitForSeconds(5);
    //     Scene scene = SceneManager.GetActiveScene();
    //     SceneManager.LoadScene(scene.name);
    // }

}
