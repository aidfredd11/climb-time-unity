using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform[] limbTargets;
    [SerializeField] private Camera mainCamera = null;
    [SerializeField] private Transform spineTransform;
    [SerializeField] private Transform hipTransform;

    [SerializeField] private TwoBoneIKConstraint leftHand = null;
    [SerializeField] private TwoBoneIKConstraint rightHand = null;
    [SerializeField] private TwoBoneIKConstraint leftFoot = null;
    [SerializeField] private TwoBoneIKConstraint rightFoot = null;

    private Transform activeTarget;
    private Animator animator;

    private string clickTarget;
    private bool clicking = false;
    private bool gameStarted = false;

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        animator = GetComponent<Animator>();

        gameStarted = false;

        leftHand.weight = 0;
        rightHand.weight = 0;
        leftFoot.weight = 0;
        rightFoot.weight = 0;

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameStarted = true;

            Time.timeScale = 0;
            animator.speed = 0; // stop plaing idle animation

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
                        //Time.timeScale = 1;
                        clicking = true;
                        activeTarget = limbTargets[0]; // make the left hand active
                        leftHand.weight = 1;

                        break;

                    case "RH Click Target":
                        //Time.timeScale = 1;
                        clicking = true;
                        activeTarget = limbTargets[1]; // right hand active
                        rightHand.weight = 1;

                        break;

                    case "LF Click Target":
                        //Time.timeScale = 1;
                        clicking = true;
                        activeTarget = limbTargets[2]; // make the left hand active
                        leftFoot.weight = 1;

                        break;

                    case "RF Click Target":
                        //Time.timeScale = 1;
                        clicking = true;
                        activeTarget = limbTargets[3]; // right hand active
                        rightFoot.weight = 1;

                        break;

                    default:
                        Time.timeScale = 0;
                        break;
                }

            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                int layer = hit.collider.gameObject.layer;
                Debug.Log("Layer: " + layer.ToString());

                // hold layer
                if (layer == 6 && clicking)
                {
                    Transform snapPosition = hit.collider.gameObject.transform.GetChild(0);

                    Vector3 holdPosition = snapPosition.position; //hit.collider.gameObject.transform.position; // get the position of the hold

                    float distanceToHold;

                    // check distance for hands
                    if(activeTarget == limbTargets[0] || activeTarget == limbTargets[1])
                    {
                        // distance from spine
                        distanceToHold = Vector3.Distance(holdPosition, spineTransform.position);
                        Debug.Log("Distance to hold: " + distanceToHold.ToString());

                        if(distanceToHold < 1f)
                        {
                            SetActiveTargetPosition(holdPosition);
                        }
                        else
                        {
                            Debug.Log("Hold too far away!!");
                        }

                        // check distance for feet
                    } else if(activeTarget == limbTargets[2] || activeTarget == limbTargets[3]){

                        var heading = holdPosition - hipTransform.position;
                        var distance = heading.magnitude;
                        var direction = (heading / distance).y;

                        Debug.Log("Direction of hold: " + direction.ToString());

                        // distance from hips
                        distanceToHold = Vector3.Distance(holdPosition, hipTransform.position);
                        Debug.Log("Distance to hold: " + distanceToHold.ToString());

                        // hold is below hips 
                        if(distanceToHold < 1.2f && direction < 0)
                        {
                            SetActiveTargetPosition(holdPosition);
                        // hold is above hips
                        } else if(distanceToHold < 0.7f && direction > 0)
                        {
                            SetActiveTargetPosition(holdPosition);
                        }
                        else
                        {
                            Debug.Log("Hold out of reach!!");
                        }

                    }
                                        
                }
            }

            clicking = false;
            activeTarget = null;

        }

    }
    private void LateUpdate()
    {
        // move the limb if one has been clicked
        if (activeTarget != null && clicking)
        {
            MoveLimb(activeTarget);
        }
    }

    public bool GetClicking()
    {
        return clicking;
    }
    public void SetClicking(bool value)
    {
        clicking = value;
    }
    public bool GetGameStarted()
    {
        return gameStarted;
    }
    public Transform GetActiveTarget()
    {
        return activeTarget;
    }
    public void SetActiveTarget(Transform targetTransform)
    {
        activeTarget = targetTransform;
    }
    public void SetActiveTargetPosition(Vector3 position)
    {
        if(activeTarget != null)
            activeTarget.position = new Vector3(position.x, position.y, 4.9f);
    }

    private void MoveLimb(Transform target)
    {
        Vector3 targetPosition;
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;

        targetPosition = mainCamera.ScreenToWorldPoint(new Vector3(mouseX, mouseY, mainCamera.transform.position.z));
        targetPosition.z = target.position.z;
        target.transform.position = targetPosition;
    }

}
