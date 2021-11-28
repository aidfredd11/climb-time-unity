using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildWall : MonoBehaviour
{
    [SerializeField] private GameObject[] wallPanelPrefabs;
    [SerializeField] private int maxWallPanels = 3;

    private float wallCoordY = 0f;
    private const float wallCoordZ = 5f;

    private GameObject[] panels;

    private void Start()
    {
        panels = new GameObject[maxWallPanels];

        for (int i = 0; i < maxWallPanels; i++)
        {
            GameObject panel = Instantiate(wallPanelPrefabs[Random.Range(0, wallPanelPrefabs.Length)],
                                    new Vector3(0, wallCoordY, wallCoordZ), Quaternion.identity, gameObject.transform);
            
            panels[i] = panel; // add to list of panels

            wallCoordY += panel.transform.localScale.y; // increment height by size of panel
        }

    }

    public void AddPanel()
    {
        //get y coord of the highest panel
        GameObject lastPanel = panels.Last();
        wallCoordY = lastPanel.transform.position.y + lastPanel.transform.localScale.y;

        for (int i = 0; i < maxWallPanels; i++)
        {
            GameObject panel = Instantiate(wallPanelPrefabs[Random.Range(0, wallPanelPrefabs.Length)],
                                    new Vector3(0, wallCoordY, wallCoordZ), Quaternion.identity, gameObject.transform);

            panels[i] = panel; // add to list of panels
            wallCoordY += panel.transform.localScale.y; // increment height by size of panel
        }

    }

}
