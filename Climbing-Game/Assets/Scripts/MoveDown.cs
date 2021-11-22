using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    [SerializeField] private float currentSpeed;
    [SerializeField] private float bottomBound;

    private void Update()
    {
        transform.Translate(-Vector3.up * currentSpeed * Time.deltaTime);
        
        if(transform.position.y < bottomBound)
        {
            Destroy(gameObject);
        }
    }

}
