using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class BetweenBlocks : MonoBehaviour
{
    static float timeInterval;
    public CircleCollider2D player;
    public BoxCollider2D death;
    public BoxCollider2D win;
    private bool change;
    string checkpoint;
    static int platform;
    private Vector3 Location;
    static float x;
    static Dictionary<string, float> betweenValues;
    static int total;
    static bool end;

    // Start is called before the first frame update
    void Start()
    {
        end = false;
        BetweenBlocks[] allBlocks = FindObjectsOfType<BetweenBlocks>();
        total = allBlocks.Length - 2;
        betweenValues = new Dictionary<string, float>();
        x = 0;
        platform = 0;
        timeInterval = 0;
        change = false;
    }


    private void OnTriggerEnter2D(Collider2D collision2)
    {
        if ((this.gameObject.GetComponent<BoxCollider2D>() == death || this.gameObject.GetComponent<BoxCollider2D>() == win) && end == false)
        {
            end = true;

            foreach (KeyValuePair<string, float> entry in betweenValues)
            {
                AnalyticsResult timeBetweenPlatforms = Analytics.CustomEvent("10. time to reach platform " + entry.Key, new Dictionary<string, object>
                {
                    {"time", entry.Value}

                });
                UnityEngine.Debug.Log("10. Time Between Each Platform event log: " + timeBetweenPlatforms);
                UnityEngine.Debug.Log("Time between each platform value log:" + entry.Value);
            }
        }
        else {
            if (collision2 == player && collision2.transform.position.x > x)
            {
                x = collision2.transform.position.x + 5;
                platform = platform + 1;
                change = true;
            }
        }

        if (collision2 == player && collision2.transform.position.x > x)
        {
            x = collision2.transform.position.x + 5;
            platform = platform + 1;
            change = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (change && platform >= 1)
        {
            betweenValues.Add(platform.ToString(), timeInterval / total);
            change = false;
            timeInterval = 0;
            //AnalyticsResult analyticsResult = Analytics.CustomEvent(
            //"Time For Each Segment",
            //new Dictionary<string, object>{
            //    {checkpoint, timeInterval}
            //});

        }
        timeInterval += Time.deltaTime;
    }
}