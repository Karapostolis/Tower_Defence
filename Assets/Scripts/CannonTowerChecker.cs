
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CannonTowerChecker : MonoBehaviour
{
    // Name of the prefab to check
    public string prefabName = "CannonTower";

    // Reference to the TowerData ScriptableObject
    public TowerData towerData;

    

    // Event to notify when the cost changes
    public UnityEvent onTowerCostChanged;

    void Update()
    {
        CheckCannonTowers();
    }

    void CheckCannonTowers()
    {
        // Find all game objects in the scene
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        int count = 0;

        // Count how many have the specified name or the name with "(Clone)" suffix
        foreach (GameObject obj in allObjects)
        {
            if (obj.name == prefabName || obj.name == prefabName + "(Clone)")
            {
                count++;
            }
        }

        // Check if there are more than 3 instances
        if (count > 4)
        {
            // Change the cost in the TowerData
            towerData.cost = 1100;

            // Notify listeners about the cost change
            onTowerCostChanged.Invoke();
        }
        else if  (count > 6)
        {
            // Change the cost in the TowerData
            towerData.cost = 1600;

            // Notify listeners about the cost change
            onTowerCostChanged.Invoke();
        }
        else
        {
            // Change the cost in the TowerData
            towerData.cost = 650;

            // Notify listeners about the cost change
            onTowerCostChanged.Invoke();
        }
    }

    void ChangeTowerCost()
    {
        // Action to perform when there are more than 3 CannonTower instances
        Debug.Log("There are more than 3 CannonTowers in the scene! Changing the tower cost.");
        towerData.cost = 1000;

        // Notify listeners about the cost change
        onTowerCostChanged.Invoke();
    }
}
