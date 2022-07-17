using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameJam.DiceManager {

    public class DiceHealth : MonoBehaviour
    {
        public static DiceHealth instance;
        public DiceType currentdie;

        private int lives = 4;

        private void Awake()
        {
            instance = this;
            currentdie = DiceType.D20;
        }



        public void breakDie()
        {
            lives--;
            if(lives <= 0)
            {
                Restart();
                return;
            }
            currentdie = GetNextLivingDie();
            DiceManager.instance.SwitchToNext(currentdie);
            LevelManager.instance.RespawnPlayer();
            
        }

        private void Restart()
        {
            currentdie = DiceType.D20;
            CheckpointController.instance.ResetSpawn();
            LevelManager.instance.RespawnPlayer();
            lives = 4;
            DiceManager.instance.ResetDM();
            UIController.instance.ResetUI();
        }

        public DiceType GetNextLivingDie()
        {
            switch (currentdie)
            {
                case (DiceType.D20):
                    return DiceType.D8;
                case (DiceType.D8):
                    return DiceType.D6;
                case (DiceType.D6):
                    return DiceType.D4;
                default:
                    return DiceType.D20;

            }
        }
    }

}