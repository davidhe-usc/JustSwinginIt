using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class GetAvgSpeed : MonoBehaviour
{
    [Header("Physics Ref:")]
    public Rigidbody2D m_rigidbody;
    public BoxCollider2D deathBed;
    public BoxCollider2D endPoint;

    private float averageSpeed;
    private float lastTimeSpan;
    private bool keepUpdatingAvgSpeed;
    


    // Start is called before the first frame update
    void Start()
    {
        lastTimeSpan = 0;
        averageSpeed = 0;
        keepUpdatingAvgSpeed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (keepUpdatingAvgSpeed)
        {
            updateAvgSpeed();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>() != null && (collision == deathBed || collision == endPoint))
        {
            Debug.Log("Collided with " + collision.ToString());
            Debug.Log("Avg. speed: " + averageSpeed.ToString());
            keepUpdatingAvgSpeed = false;

            AnalyticsResult analyticsResult = Analytics.CustomEvent(
            "Average Speed",
            new Dictionary<string, object>{
                {"AvgSpeed", averageSpeed}
            });
        }
    }

    private void updateAvgSpeed()
    {
        averageSpeed = (averageSpeed * lastTimeSpan + m_rigidbody.velocity.magnitude * Time.deltaTime) / (lastTimeSpan + Time.deltaTime);
        lastTimeSpan += Time.deltaTime;
    }

    //void OnDestroy()
    //{
    //    Debug.Log("OnDestroy Avg. speed: " + averageSpeed.ToString());
    //}
}
