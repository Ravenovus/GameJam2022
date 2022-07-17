using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.DiceManager {
    public class DiceManager : MonoBehaviour
    {
        [SerializeField] DiceMovementController D20;
        [SerializeField] DiceMovementController D8;
        [SerializeField] DiceMovementController D6;
        [SerializeField] DiceMovementController D4;
        [SerializeField] CinemachineVirtualCamera cam;


        public static DiceManager instance;
        private void Awake()
        {
            instance = this;
        }

        public void SwitchToNext(DiceType dt)
        {
            if(dt == DiceType.D8)
            {
                D20.gameObject.SetActive(false);
                D8.transform.position = CheckpointController.instance.GetSpawnPoint();
                D8.gameObject.SetActive(true);
                cam.Follow = D8.gameObject.transform;
            }
            if (dt == DiceType.D6)
            {
                D8.gameObject.SetActive(false);
                D6.transform.position = CheckpointController.instance.GetSpawnPoint();
                D6.gameObject.SetActive(true);
                cam.Follow = D6.gameObject.transform;
            }
            if (dt == DiceType.D4)
            {
                D6.gameObject.SetActive(false);
                D4.transform.position = CheckpointController.instance.GetSpawnPoint();
                D4.gameObject.SetActive(true);
                cam.Follow = D4.gameObject.transform;
            }
            
        }

        

    }
}