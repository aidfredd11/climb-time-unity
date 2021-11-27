using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float bottomBound;

    private Player playerScript;
    private Transform activeTarget;

    private void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if (playerScript.GetGameStarted() && playerScript.GetClicking())
        {
            activeTarget = playerScript.GetActiveTarget();

            // move everything that isn't the selected limb
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
}
