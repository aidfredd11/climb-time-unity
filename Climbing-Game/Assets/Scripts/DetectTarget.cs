using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTarget : MonoBehaviour
{
    private Transform[] limbTargets;
    private Player playerScript;
    private bool onHold = false;

    private void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Player>();

        limbTargets = playerScript.GetLimbTargets();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != playerScript.GetActiveTarget())
        {
            Debug.Log("Limb " + other.gameObject.name + " leaving hold " + gameObject.name);
            Time.timeScale = 0;
        }
    }
    public bool GetOnHold()
    {
        return onHold;
    }
}
