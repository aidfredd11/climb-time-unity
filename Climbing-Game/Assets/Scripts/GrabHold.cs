using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabHold : MonoBehaviour
{
    private Player playerScript;
    private static Transform mostRecentHold = null;

    private void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Player>();
    }

    private void OnMouseOver()
    {
        //if (playerScript.GetClicking() && gameObject.transform != mostRecentHold)

        if (playerScript.clickState == Player.ClickState.inProgress && gameObject.transform != mostRecentHold)
        {
            mostRecentHold = gameObject.transform;
            Debug.Log("Most recent hold: " + mostRecentHold.gameObject.name);

            Vector3 holdPosition = gameObject.transform.position;

            playerScript.SetActiveTargetPosition(holdPosition);
            playerScript.SetClicking(false);


            
            Debug.Log("Are you clicking? " + playerScript.GetClicking().ToString());
            Debug.Log("Hold position: " + holdPosition);

            Time.timeScale = 0;
        }
    }
}
