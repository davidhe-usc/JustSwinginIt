using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoDeathMode : MonoBehaviour
{
 
	public bool levelOver = false;
	[SerializeField] public PivotCounter PivotCounter;
    public GameOverScreen GameOverScreen;
	[SerializeField] private GameObject player;
    Vector3 direction = new Vector3(0,0,0);
	
    // Update is called once per frame
    void Update()
    {
        
    }
	private void OnTriggerEnter2D(Collider2D collision){

        if (collision.GetComponent<Collider2D>()!=null && levelOver==false){
			
			//No Death but Respawn at Beginning
			float xValue = -25.41693F;
            float yValue = 15.36321F;
            float zValue = 10;
            Vector3 poseCamera = new Vector3(xValue, yValue, zValue);
            player.transform.position = poseCamera;

	    }
	}
}
