using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildWall : MonoBehaviour
{
    [SerializeField] private GameObject[] wallPanels;

    private float spawnHeight = 0f;

    private void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            Instantiate(wallPanels[Random.Range(0, wallPanels.Length)], new Vector3(0, spawnHeight, 5), Quaternion.identity, gameObject.transform);
            spawnHeight += 2.75f;
        }
        //GameObject startPanel = Instantiate(wallPanels[Random.Range(0, wallPanels.Length)], new Vector3(0, 0, 5), Quaternion.identity, gameObject.transform);
        //spawnHeight = startPanel.transform.localScale.y - 0.25f;
    }
    private void Update()
    {
       // GameObject nextPanel = Instantiate(wallPanels[Random.Range(0, wallPanels.Length)], new Vector3(0, repeatHeight, 5), Quaternion.identity, gameObject.transform);
        
        // put the next one in the right spot
      //  repeatHeight += nextPanel.transform.localScale.y - 0.25f;
    }
}
