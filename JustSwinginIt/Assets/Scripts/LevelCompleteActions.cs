using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class LevelCompleteActions : MonoBehaviour
{
	//0a. The PivotCounter, currently an Empty GameObject.
	public bool levelOver = false;
	[SerializeField] public PivotCounter PivotCounter;
	
    public LevelCompleteScreen LevelCompleteScreen;

    public float missedGrapples;

    public void Start()
    {
        AnalyticsResult levelAttempt = Analytics.CustomEvent("Level Started");
        missedGrapples = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.GetComponent<Collider2D>() != null && levelOver==false)
        {
            AnalyticsResult levelComplete = Analytics.CustomEvent("Level Won");
            levelOver = true;
            PivotCounter.PivotCounterBegin()
            UnityEngine.Debug.Log("Win log: " + levelComplete);
            LevelCompleteScreen.Setup();

            AnalyticsResult otherAnalytics = Analytics.CustomEvent("Missed Grapples", new Dictionary<string, object>
            {
                {"Missed Grapples", missedGrapples}
            });

            StartCoroutine(WaitThenReload());

            
            
        }
    }

    IEnumerator WaitThenReload()
    {
        yield return new WaitForSeconds(5);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

}
