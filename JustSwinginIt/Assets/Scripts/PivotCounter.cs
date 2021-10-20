using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using System;

public class PivotCounter : MonoBehaviour
{ 
	public LevelCompleteActions levelEndpoint;
	public GameOverActions gameOver;
	public int levelNumber = 0; //x
	private double numPivotObjects = 0.0; //y
	
	public void PivotCounterBegin()
	{
		//1. Collect all Grappable Pivots.
		int layerNum = 9; // Potential XXX 9 for Grappable.
		List<GameObject> goa = new List<GameObject>();
		var allObjects = FindObjectsOfType<GameObject>();
		for (int i=0;i<allObjects.Length;i++)
		{
			if(allObjects[i].layer == layerNum)
			{
				goa.Add(allObjects[i]);
			}
		}
		var gameObjectsAnchors = goa.ToArray();
		numPivotObjects = gameObjectsAnchors.Length;
		
		//2. Check for all pivots whether used or not.
		double pivotsUsed = 0.0;
		//2a. For i pivot.
		for (int i=0;i<numPivotObjects;i++)
		{
			GameObject go = gameObjectsAnchors[i];//GameObject.Find("Target");
			var objScript = (HasPivoted)go.GetComponent(typeof(HasPivoted));
			//2b. Check if pivot was used-if so, increase pivot count.
			if (objScript.HasThisPivoted() == true)
			{
				pivotsUsed++;
			}
		}
		
		//2c. Calculate percentage of pivots used. Round to 3 digits.
		double percentPivotsUsed = ((pivotsUsed/numPivotObjects)*100.0);
		percentPivotsUsed = Math.Round(percentPivotsUsed, 3);
		TriggerTestAnalyticsEvent(percentPivotsUsed, pivotsUsed);
	}
	//3. Analytics: Transfer fraction/percentage of zip points/grapple points used on each level.
	public void TriggerTestAnalyticsEvent(double percentPivotsUsed, double pivotsUsed)
	{
		//3a. Calculate X = Level Number
		if(levelEndpoint.levelOver == true || gameOver.levelOver == true)
		{
			levelNumber++; //XXX Should keep levelNumber in GameManager not here.
		}		
		
		//3b. Calculate Y = percentPivotsUsed
		Dictionary<string, object> analyticsData = new Dictionary<string, object>
            {
                {"% Pivots Used", percentPivotsUsed }
            };
		// Debug.Log("Level Number analytics: "+ levelNumber);
		Debug.Log("#Pivots used analytics: "+pivotsUsed+"/"+numPivotObjects+" "+percentPivotsUsed+"%Pivots used");
		
		//3c. Generate Analytics. Responds "Ok".
		AnalyticsResult analytics_result = Analytics.CustomEvent(("Level "+levelNumber), analyticsData);
		// Debug.Log("Pivot Count Log: "+analytics_result);		
	}
}
