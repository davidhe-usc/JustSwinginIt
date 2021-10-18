using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class GameOverActions : MonoBehaviour
{
    public GameOverScreen GameOverScreen;
    [SerializeField]
    private LevelCompleteActions manager;


    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.GetComponent<Collider2D>()!=null){
            AnalyticsResult deathResult = Analytics.CustomEvent("Player died", new Dictionary<string,object>{
                {"Level", 1},
                {"Position", transform.position.x}
            });

            AnalyticsResult otherAnalytics = Analytics.CustomEvent("Missed Grapples", new Dictionary<string, object>
            {
                {"Missed Grapples", manager.missedGrapples}
            });
            UnityEngine.Debug.Log("Death log: "+ deathResult);
            GameOverScreen.Setup();
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
