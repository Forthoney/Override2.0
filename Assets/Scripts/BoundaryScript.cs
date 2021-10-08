using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryScript : MonoBehaviour
{

    public Transform otherBoundary;
    public Vector3 offset;

    public bool isVertical;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")){
            
            Vector3 playerEntryPosition = other.gameObject.transform.position - transform.position; 
            other.gameObject.transform.position = otherBoundary.position - offset;

            // Offset by the players entry axis coordinate
            if(isVertical){
                playerEntryPosition = new Vector3(0, playerEntryPosition.y);
                other.gameObject.transform.position += playerEntryPosition;
            } else {
                playerEntryPosition = new Vector3(playerEntryPosition.x, 0);
                other.gameObject.transform.position += playerEntryPosition;
            }
        }
    }
    


}
