using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Dyno : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform otherHand;
    [SerializeField] private Rig characterRig;
    [SerializeField] private Rig dynoRig;

    [SerializeField] private Timer timer;
    [SerializeField] private Energy energy;

    [SerializeField] private float speed = 1;

    private bool dynoInitiated = false;

    private void Start()
    {
    }

    private void Update()
    {
        if (dynoInitiated)
        {
            timer.ToggleTimer(false);
            energy.ToggleEnergy(false);

            //characterRig.weight = 0;

            // Move the camera
            mainCamera.transform.Translate(Vector3.up * speed * Time.deltaTime);
            mainCamera.transform.Translate(-Vector3.forward * speed * Time.deltaTime);
            if (mainCamera.transform.position.y >= 2.5)
                speed = 0;

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "DynoStart")
        {
            Debug.Log(other.gameObject.name + " collided with " + gameObject.name);

            Vector3 holdPosition = other.gameObject.transform.GetChild(0).position; // get the snap position

            // Set the hands to be on the hold
            gameObject.transform.position = holdPosition;
            otherHand.position = holdPosition;

            dynoInitiated = true;
        }
    }
}
