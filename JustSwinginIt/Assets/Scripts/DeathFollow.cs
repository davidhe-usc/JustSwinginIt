using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFollow : MonoBehaviour
{
    Vector2 screenBounds;
    float maxHeight;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        transform.position = new Vector2(transform.position.x, -screenBounds.y);
        maxHeight = transform.position.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        transform.position = new Vector2(transform.position.x, Mathf.Max(maxHeight, -screenBounds.y));
    }
}
