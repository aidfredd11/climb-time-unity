using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Player : MonoBehaviour
{
    [SerializeField] private Camera mainCamera = null;

    [SerializeField] private Transform[] limbTargets;
    [SerializeField] private Transform spineTransform;
    [SerializeField] private Transform hipTransform;

    [SerializeField] private TwoBoneIKConstraint leftHand = null;
    [SerializeField] private TwoBoneIKConstraint rightHand = null;
    [SerializeField] private TwoBoneIKConstraint leftFoot = null;
    [SerializeField] private TwoBoneIKConstraint rightFoot = null;

    [SerializeField] private float fallSpeed;

    private Timer timer;
    private Energy energy;
    private GameController gameController;

    private Transform activeTarget;
    private Animator animator;

    private string clickTarget;
    private bool clicking = false;

    
    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        // animation
        leftHand.weight = 0;
        rightHand.weight = 0;
        leftFoot.weight = 0;
        rightFoot.weight = 0;

        animator = GetComponent<Animator>();
        timer = GetComponent<Timer>();
        energy = GetComponent<Energy>();
        gameController = GetComponent<GameController>();

    }

    private void Update()
    {
        // out of energy or time
        if(timer.GetCurrentTime() <= 0 || energy.GetCurrentEnergyLevel() <= 0)
        {
            gameController.GameOver = true;

            timer.ToggleTimer(false);
            energy.ToggleEnergy(false);

            // make it look like they're falling
            animator.SetBool("gameOver", true);
            animator.speed = 1;

            transform.Translate(-Vector3.up * fallSpeed * Time.deltaTime);
            if (transform.position.y <= -5)
                fallSpeed = 0;
        }

        // Mouse is clicked down
        if (Input.GetMouseButtonDown(0))
        {
            gameController.GameStarted = true;

            // Raycast to find what they're clicking
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && !gameController.GameOver)
            {
                // which limb is being clicked
                clickTarget = hit.collider.gameObject.name;
                switch (clickTarget)
                {
                    case "LH Click Target":
                        StartLimbDrag(limbTargets[0], leftHand);
                        break;

                    case "RH Click Target":
                        StartLimbDrag(limbTargets[1], rightHand);
                        break;

                    case "LF Click Target":
                        StartLimbDrag(limbTargets[2], leftFoot);
                        break;

                    case "RF Click Target":
                        StartLimbDrag(limbTargets[3], rightFoot);
                        break;

                    default:
                        break;
                }

            }

        }

        // Mouse is released
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && !gameController.GameOver)
            {

                int layer = hit.collider.gameObject.layer; // has the mouse been released over a hold
                if (layer == 6 && clicking)
                {
                    Vector3 holdPosition = hit.collider.gameObject.transform.GetChild(0).position; // get the position of the holds snap position
                    float distanceToHold;

                    // check distance for hands
                    if(activeTarget == limbTargets[0] || activeTarget == limbTargets[1])
                    {
                        // distance from spine
                        distanceToHold = Vector3.Distance(holdPosition, spineTransform.position);

                        if(distanceToHold < 1f) // only move target when in range of hold
                        {
                            SetActiveTargetPosition(holdPosition);
                        }

                    // check distance for feet
                    } else if(activeTarget == limbTargets[2] || activeTarget == limbTargets[3]){

                        // find if hold is above or below the hips
                        var heading = holdPosition - hipTransform.position;
                        var distance = heading.magnitude;
                        var direction = (heading / distance).y;

                        // distance from hips
                        distanceToHold = Vector3.Distance(holdPosition, hipTransform.position);

                        // hold is below hips 
                        if (distanceToHold < 1.2f && direction < 0)
                        {
                            SetActiveTargetPosition(holdPosition);

                            // hold is above hips
                        }
                        else if (distanceToHold < 0.7f && direction > 0)
                        {
                            SetActiveTargetPosition(holdPosition);
                        }
                    }

                    // Decrease energy
                    energy.ToggleEnergy(false);
                    // reset timer
                    timer.ResetTimer();
                }
            }

            clicking = false;
            activeTarget = null;
        }

    }

    //Follow the target with mouse
    private void LateUpdate()
    {
        // move the limb if one has been clicked
        if (activeTarget != null && clicking)
        {
            MoveLimb(activeTarget);
        }
        
    }

    // Helper functions
    private void StartLimbDrag(Transform target, TwoBoneIKConstraint limb)
    {
        animator.speed = 0; // stop plaing idle animation
        Time.timeScale = 0;

        clicking = true;

        timer.ToggleTimer(); // start the strength timer
        energy.ToggleEnergy();

        activeTarget = target; // make the left hand active
        limb.weight = 1;
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
    private void SetActiveTargetPosition(Vector3 position)
    {
        if (activeTarget != null)
            activeTarget.position = new Vector3(position.x, position.y, 4.9f);
    }

    // Functions used in other scripts
    public bool GetClicking()
    {
        return clicking;
    }
    public Transform GetActiveTarget()
    {
        return activeTarget;
    }
    public Transform[] GetLimbTargets()
    {
        return limbTargets;
    }

    public Timer GetTimer()
    {
        return timer;
    }
    public Energy GetEnergy()
    {
        return energy;
    }

}
