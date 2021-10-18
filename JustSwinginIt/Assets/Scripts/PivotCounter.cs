using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class PivotCounter : MonoBehaviour
{
	public GameOverActions deathBed; 
	public LevelCompleteActions levelEndpoint;
	private GameObject[] gameObjectsAnchors;
	private int numPivotObjects = 0;
	public int levelNumber = 0;
	
	//Fraction/percentage of zip points/grapple points used on each level.
	public void TriggerTestAnalyticsEvent(float percentPivotsUsed){
		//X = Level Number
		if(levelEndpoint.levelOver == true){
			levelNumber++;
			//XXX Should keep levelNumber in GameManager not here!
		}
		
		//Y = percentPivotsUsed
		Dictionary<string, object> analyticsData = new Dictionary<string, object>
            {
                {"% Pivots Used", percentPivotsUsed }
            };
		Debug.Log("levelNumber: "+ levelNumber);
		Debug.Log("pivots used" + percentPivotsUsed);
		AnalyticsResult analytics_result = Analytics.CustomEvent(("Level "+levelNumber), analyticsData);
		Debug.Log("Analytics Result: "+analytics_result);		
	}
	
    // Start is called before the first frame update
    void Start()
    {
        gameObjectsAnchors = GameObject.FindGameObjectsWithTag("Anchors");
		Debug.Log("gameObjectsAnchors "+gameObjectsAnchors);
		numPivotObjects =gameObjectsAnchors.Length;
		Debug.Log("numPivotObjects: "+numPivotObjects);
    }

    // Update is called once per frame
    void Update()
    {
		//if game is over, win or lose...
        if(deathBed.levelOver==true || levelEndpoint.levelOver==true){
			//then calculate analytics.
			var pivotsUsed = 0;
			for (int i=0;i<numPivotObjects;i++){
				GameObject go = gameObjectsAnchors[i];//GameObject.Find("Pivot");
				var objScript = (HasPivoted)go.GetComponent(typeof(HasPivoted));
				if (objScript.HasThisPivoted() == true)
				{
					pivotsUsed++;
					Debug.Log("pivotsUsed: "+pivotsUsed);
				}
			}
			float percentPivotsUsed = pivotsUsed/numPivotObjects;
			TriggerTestAnalyticsEvent(percentPivotsUsed);
		}
    }
	
}
