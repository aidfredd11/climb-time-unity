using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float bottomBound;

    private Player playerScript;
    private BuildWall buildWallScript;

    private Transform activeTarget;

    private Vector3 startPosition;

    private void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Player>();
        buildWallScript = GameObject.Find("Wall").GetComponent<BuildWall>();       

        startPosition = gameObject.transform.position;
    }

    private void Update()
    {
        // start moving when they click
        if (playerScript.GetGameStarted() && playerScript.GetClicking())
        {
            activeTarget = playerScript.GetActiveTarget();

            // move wall and the unselected targets
            if(activeTarget != gameObject.transform)
            {
                Time.timeScale = 1;
                transform.Translate(-Vector3.up * speed * Time.deltaTime);

            }
           
        }

        if (transform.position.y < bottomBound)
        {
            Destroy(gameObject);
        } 

    }
    private void OnTriggerEnter(Collider other)
    {
      
        if(other.gameObject.name == "Player" && gameObject.name == "Wall Build Trigger")
        {
            buildWallScript.AddPanel();
            transform.position = startPosition;
        }
    }
}
