using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class DeathPosition : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D collision){

      if (collision.GetComponent<Collider2D>()!=null){

          int deathSegment = Mathf.RoundToInt(transform.position.x/20f);
            //7. NUMBER OF DEATHS AT EACH SEGMENT
          AnalyticsResult deathSegmentAnalytics = Analytics.CustomEvent(
              "7. Segment of death",
              new Dictionary<string,object>{
                  {"Segment", deathSegment}
              }
          );
        UnityEngine.Debug.Log("Death segment event log: "+ deathSegmentAnalytics);
        UnityEngine.Debug.Log("Death segment value: "+ deathSegment);

        }
        

   }
}
