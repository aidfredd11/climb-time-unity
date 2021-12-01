using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float bottomBound;

    private GameController gameController;
    private BuildWall buildWallScript;
    private Player playerScript;

    private Transform activeTarget;

    private Vector3 startPosition;

    private void Start()
    {
        GameObject player = GameObject.Find("Player");

        playerScript = player.GetComponent<Player>();
        gameController = player.GetComponent<GameController>();
        buildWallScript = GameObject.Find("Wall").GetComponent<BuildWall>();       

        startPosition = gameObject.transform.position;
    }

    private void Update()
    {
        // start moving when they click
        if (gameController.GameStarted && playerScript.GetClicking())
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

    // used for spawning wall panels
    private void OnTriggerEnter(Collider other)
    {
      
        if(other.gameObject.name == "Player" && gameObject.name == "Wall Build Trigger")
        {
            buildWallScript.AddPanel();
            transform.position = startPosition;
        }
    }
}
