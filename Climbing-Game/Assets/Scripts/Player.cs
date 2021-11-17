using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Player : MonoBehaviour
{
    //[SerializeField] private float degrees = 45;
        
    [SerializeField] private Transform target = null;
    [SerializeField] private Transform hold = null;

    [SerializeField] private Camera mainCam = null;
    [SerializeField] private ChainIKConstraint leftHand = null;
    [SerializeField] private MultiAimConstraint head = null;

    private bool clicking;

    private void Start()
    {
        if (mainCam == null)
            mainCam = Camera.main;

        leftHand.weight = 0;
        head.weight = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clicking = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            clicking = false;
        }

    }

    private void LateUpdate()
    {
        if (clicking)
        {
            leftHand.weight = 1;
            head.weight = 1;

            Vector3 targetPosition;
            targetPosition = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCam.transform.position.z));
            targetPosition.z = target.position.z;
            target.transform.position = targetPosition;

           // target.transform.rotation = Quaternion.Euler(Vector3.up * degrees);
        } 
    }
}
