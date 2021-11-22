using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform[] limbTargets;
   // [SerializeField] private Transform headAimTarget;
    [SerializeField] private Camera mainCamera = null;

   // [SerializeField] private MultiAimConstraint head = null;
    [SerializeField] private TwoBoneIKConstraint leftHand = null;
    [SerializeField] private TwoBoneIKConstraint rightHand = null;
    [SerializeField] private TwoBoneIKConstraint leftFoot = null;
    [SerializeField] private TwoBoneIKConstraint rightFoot = null;
 
    private Transform activeTarget;
    private Animator animator;
   // private RigBuilder rigBuilder;

    private bool clicking;
    private string clickTarget;

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        animator = GetComponent<Animator>();
        //rigBuilder = GetComponent<RigBuilder>();

       // head.weight = 0;
        leftHand.weight = 0;
        rightHand.weight = 0;
        leftFoot.weight = 0;
        rightFoot.weight = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.speed = 0; // stop plaing idle animation

            clicking = true;

            // find what they're clicking
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                clickTarget = hit.collider.gameObject.name;

                // which limb is being clicked
                switch (clickTarget)
                {
                    case "LH Click Target":
                        activeTarget = limbTargets[0]; // make the left hand active
                        leftHand.weight = 1;
                        break;

                    case "RH Click Target":
                        activeTarget = limbTargets[1]; // right hand active
                        rightHand.weight = 1;
                        break;

                    case "LF Click Target":
                        activeTarget = limbTargets[2]; // make the left hand active
                        leftFoot.weight = 1;
                        break;

                    case "RF Click Target":
                        activeTarget = limbTargets[3]; // right hand active
                        rightFoot.weight = 1;
                        break;

                    default:
                        break;
                }

             /*   // aim the head at the limb if they have clicked one
                if (activeTarget != null)
                {
                    head.weight = 1;
                    HeadLookTarget(activeTarget);
                } */
            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            clicking = false;
            activeTarget = null;
        }

    }
    private void LateUpdate()
    {
        // move the limb if one has been clicked
        if(activeTarget != null && clicking)
        {
            MoveLimb(activeTarget);
        }
    }

    // Helper functions
    private void MoveLimb(Transform target)
    {
        Vector3 targetPosition;
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;

        targetPosition = mainCamera.ScreenToWorldPoint(new Vector3(mouseX, mouseY, mainCamera.transform.position.z));
        targetPosition.z = target.position.z;
        target.transform.position = targetPosition;
    }

/*    private void HeadLookTarget(Transform target)
    {
        headAimTarget.position = target.position;
       /* var data = head.data.sourceObjects;
        data.Clear();

        WeightedTransform weightedTransform = new WeightedTransform(target, 1);
        data.Insert(0, weightedTransform);
        head.data.sourceObjects = data;

        rigBuilder.Build(); 

    } */
}
