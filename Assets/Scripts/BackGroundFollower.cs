using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundFollower : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cvm;

    private Vector3 tempVector;


    private void Awake()
    {
        cvm = FindObjectOfType<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        tempVector = new Vector3(cvm.transform.position.x,cvm.transform.position.y,12);

        transform.position = tempVector;

    }
}
