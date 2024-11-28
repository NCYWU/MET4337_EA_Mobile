using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class TowerDefenseToolkit : EditorWindow
{
    private GameObject[] towerPrefabs;
    private GameObject[] enemyPrefabs;
    private Vector3 towerPosition = Vector3.zero;
    private int selectedTowerIndex = 0;

    [MenuItem("Tools/Tower Defense Toolkit")]
    public static void ShowWindow()
    {
        GetWindow<TowerDefenseToolkit>("Tower Defense Toolkit");
    }

    private void OnGUI()
    {
        GUILayout.Label("Tower Defense Toolkit", EditorStyles.boldLabel);

        // Load Tower Prefabs
        if (GUILayout.Button("Load Tower Prefabs"))
        {
            LoadTowerPrefabs();
        }

        // Tower Selection
        selectedTowerIndex = EditorGUILayout.Popup("Select Tower", selectedTowerIndex, GetTowerNames());
        towerPosition = EditorGUILayout.Vector3Field("Tower Position", towerPosition);

        // Place Tower Button
        if (GUILayout.Button("Place Tower"))
        {
            PlaceTower();
        }

        // Load Enemy Prefabs
        if (GUILayout.Button("Load Enemy Prefabs"))
        {
            LoadEnemyPrefabs();
        }

        // Enemy Spawning
        if (GUILayout.Button("Spawn Enemy"))
        {
            SpawnEnemy();
        }
    }

    private void LoadTowerPrefabs()
    {
        towerPrefabs = Resources.LoadAll<GameObject>("Prefabs/Towers");
        Debug.Log("Loaded " + towerPrefabs.Length + " tower prefabs.");
    }

    private string[] GetTowerNames()
    {
        string[] names = new string[towerPrefabs.Length];
        for (int i = 0; i < towerPrefabs.Length; i++)
        {
            names[i] = towerPrefabs[i].name;
        }
        return names;
    }

    private void PlaceTower()
    {
        if (towerPrefabs.Length > 0)
        {
            GameObject tower = Instantiate(towerPrefabs[selectedTowerIndex], towerPosition, Quaternion.identity);
            Undo.RegisterCreatedObjectUndo(tower, "Place Tower");
            Debug.Log("Placed Tower: " + tower.name);
        }
        else
        {
            Debug.LogWarning("No tower prefabs loaded!");
        }
    }

    private void LoadEnemyPrefabs()
    {
        enemyPrefabs = Resources.LoadAll<GameObject>("Prefabs/Enemies");
        Debug.Log("Loaded " + enemyPrefabs.Length + " enemy prefabs.");
    }

    private void SpawnEnemy()
    {
        if (enemyPrefabs.Length > 0)
        {
            GameObject enemy = Instantiate(enemyPrefabs[0], new Vector3(0, 0, 0), Quaternion.identity);
            Undo.RegisterCreatedObjectUndo(enemy, "Spawn Enemy");
            Debug.Log("Spawned Enemy: " + enemy.name);
        }
        else
        {
            Debug.LogWarning("No enemy prefabs loaded!");
        }
    }
}