using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform[] limbTargets;
    [SerializeField] private Camera mainCamera = null;

    [SerializeField] private TwoBoneIKConstraint leftHand = null;
    [SerializeField] private TwoBoneIKConstraint rightHand = null;
    [SerializeField] private TwoBoneIKConstraint leftFoot = null;
    [SerializeField] private TwoBoneIKConstraint rightFoot = null;

    private Transform activeTarget;
    private Animator animator;

    private string clickTarget;
    private bool clicking = false;
    private bool gameStarted = false;

    public ClickState clickState;

    public enum ClickState
    {
        started,
        inProgress,
        finished
    }

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

        clickState = ClickState.finished;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickState = ClickState.started;

            Time.timeScale = 1;

            gameStarted = true;
            clicking = true;

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
                        activeTarget = limbTargets[0]; // make the left hand active
                        leftHand.weight = 1;
                        clickState = ClickState.inProgress;
                        break;

                    case "RH Click Target":
                        activeTarget = limbTargets[1]; // right hand active
                        rightHand.weight = 1;
                        clickState = ClickState.inProgress;
                        break;

                    case "LF Click Target":
                        activeTarget = limbTargets[2]; // make the left hand active
                        leftFoot.weight = 1;
                        clickState = ClickState.inProgress;
                        break;

                    case "RF Click Target":
                        activeTarget = limbTargets[3]; // right hand active
                        rightFoot.weight = 1;
                        clickState = ClickState.inProgress;
                        break;

                    default:
                        break;
                }

            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            clicking = false;

           // Time.timeScale = 0;
            /*
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                string holdName = hit.collider.gameObject.name;
                Debug.Log("Hold: " + holdName);

                activeTarget.position = new Vector3(hit.transform.position.x, hit.transform.position.y, 5);
            } */

            activeTarget = null;
            clickState = ClickState.finished;
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
    public void SetActiveTargetPosition(Vector3 position)
    {
        if(activeTarget != null)
            activeTarget.position = new Vector3(position.x, position.y, 4.9f);
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

}
