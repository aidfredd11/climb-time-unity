using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildWall : MonoBehaviour
{
    [SerializeField] private GameObject[] wallPanels;

    private float spawnHeight = 0f;
    private Player playerScript;

    private void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Player>();

        for (int i = 0; i < 3; i++)
        {
            Instantiate(wallPanels[Random.Range(0, wallPanels.Length)], new Vector3(0, spawnHeight, 5), Quaternion.identity, gameObject.transform);
            spawnHeight += 2.9f;//2.75f;
        }
        spawnHeight = 5.8f;//5.5f;

        InvokeRepeating("AddWallPanel", 22, 22);

    }

    private void AddWallPanel()
    {
        if (playerScript.GetGameStarted())
        {
            Instantiate(wallPanels[Random.Range(0, wallPanels.Length)], new Vector3(0, spawnHeight, 5), Quaternion.identity, gameObject.transform);
        }
    }

}
