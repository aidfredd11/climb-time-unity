using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class MoveLimb : MonoBehaviour
{
    [SerializeField] private Rig rig;
    [SerializeField] private Transform objectToMove = null;
    [SerializeField] private Camera mainCam = null;

    private bool clicking;

    float rotationSpeed = 100;

    private void Start()
    {
        if (mainCam == null)
            mainCam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            clicking = true;

        if (Input.GetMouseButtonUp(0))
            clicking = false;
    }

    private void LateUpdate()
    {
        if (clicking)
        {
            objectToMove.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

            Vector3 targetPos;
            targetPos = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCam.transform.position.z));
            targetPos.z = objectToMove.position.z;
            objectToMove.transform.position = targetPos;
        }
    }
}
