using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Player : MonoBehaviour
{
    //[SerializeField] private Transform target = null;
    [SerializeField] private Transform[] targets;
    [SerializeField] private Transform hold = null;

    [SerializeField] private Camera mainCam = null;
    [SerializeField] private ChainIKConstraint leftHand = null;
    [SerializeField] private ChainIKConstraint rightHand = null;
    [SerializeField] private MultiAimConstraint head = null;

    private Transform activeTarget = null;
    private bool clicking;
    private string clickTarget = null;

    private void Start()
    {
        if (mainCam == null)
            mainCam = Camera.main;

        leftHand.weight = 0;
        rightHand.weight = 0;
        head.weight = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clicking = true;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                clickTarget = hit.collider.gameObject.name;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            clicking = false;
        }

    }

    private void LateUpdate()
    {
        if (clicking) // && clickTarget.Equals("LH Click Target")
        {
            // which limb is being clicked
            switch (clickTarget)
            {
                case "LH Click Target":

                    Debug.Log("Clicking left hand");
                    leftHand.weight = 1;
                    head.weight = 1;
                    MoveLimb();

                    break;
                case "RH Click Target":

                    Debug.Log("Clicking left hand");
                    rightHand.weight = 1;
                   // head.weight = 1;
                    MoveLimb();

                    break;

                default:
                    Debug.Log("Not clicking a limb");
                    break;
            }
            
        }
    }

    private void MoveLimb()
    {
        Vector3 targetPosition;
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;

        targetPosition = mainCam.ScreenToWorldPoint(new Vector3(mouseX, mouseY, mainCam.transform.position.z));
        targetPosition.z = target.position.z;
        target.transform.position = targetPosition;
    }
}
