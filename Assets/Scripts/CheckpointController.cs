using GameJam.DiceManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    [SerializeField] Vector3 spawnPoint;
    [SerializeField] Vector3 originalSpawn;

    public static CheckpointController instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        spawnPoint = DiceManager.instance.transform.position;
        originalSpawn = spawnPoint;
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }

    public Vector3 GetSpawnPoint()
    {
        return spawnPoint;
    }

    public void ResetSpawn()
    {
        spawnPoint = originalSpawn;
    }
}
