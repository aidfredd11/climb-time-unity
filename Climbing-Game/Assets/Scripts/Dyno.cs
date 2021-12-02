using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations.Rigging;

public class Dyno : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform otherHand;
    [SerializeField] private Slider dynoSlider;

    [SerializeField] private Timer timer;
    [SerializeField] private Energy energy;

    [SerializeField] private float speed = 1;

    private bool dynoInitiated = false;

    int sliderVal = 0;
    bool countingUp = false;
    bool countingDown = false;

    public bool DynoInitiated
    {
        get { return dynoInitiated; }
        set { dynoInitiated = value; }
    }

    private void Start()
    {
        dynoSlider.gameObject.SetActive(false);
        countingUp = true;
    }

    private void Update()
    {

        if (dynoInitiated)
        {
            timer.ToggleTimer(false);
            energy.ToggleEnergy(false);

            // Move the camera
            mainCamera.transform.Translate(Vector3.up * speed * Time.deltaTime);
            mainCamera.transform.Translate(-Vector3.forward * speed * Time.deltaTime);
            if (mainCamera.transform.position.y >= 2.5)
                speed = 0;

            dynoSlider.gameObject.SetActive(true);
            Counting();

            if (Input.GetMouseButtonDown(0))
            {
                // play a jumping animation
                // move down the wall
                // disable the dyno move
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DynoStart")
        {
          //  Debug.Log(other.gameObject.name + " collided with " + gameObject.name);

            Vector3 holdPosition = other.gameObject.transform.GetChild(0).position; // get the snap position

            // Set the hands to be on the hold
            string otherHandName = otherHand.gameObject.name;
            if (otherHandName == "Left Hand_target")
            {
                // this script is on the right hand
                transform.position = holdPosition + new Vector3(0.25f, -0.125f, 0);
                otherHand.position = holdPosition + new Vector3(-0.25f, -0.125f, 0);

            }
            else if (otherHandName == "Right Hand_target")
            {
                // this script is on the left hand
                transform.position = holdPosition + new Vector3(-0.25f, -0.125f, 0);
                otherHand.position = holdPosition + new Vector3(0.25f, -0.125f, 0);
            }

            //  gameObject.transform.position = holdPosition;
            //  otherHand.position = holdPosition;

            dynoInitiated = true;

            //Debug.Log(gameObject.name + " started dyno: " + dynoInitiated.ToString());
        }
    }

    private void Counting()
    {
        // bounce slider back and forth between 0 and 100
        if (countingUp)
        {
            sliderVal++;

            if (sliderVal == 100)
            {
                countingUp = false;
                countingDown = true;
            }

        }
        else if (countingDown)
        {
            sliderVal--;
            if (sliderVal == 0)
            {
                countingDown = false;
                countingUp = true;
            }
        }

        dynoSlider.value = sliderVal;
    }
}
