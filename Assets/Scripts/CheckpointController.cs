using GameJam.DiceManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    [SerializeField] Vector3 spawnPoint;

    public static CheckpointController instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        spawnPoint = DiceManager.instance.transform.position;
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }

    public Vector3 GetSpawnPoint()
    {
        return spawnPoint;
    }
}
