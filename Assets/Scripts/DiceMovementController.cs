using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.DiceManager
{
    public class DiceMovementController : MonoBehaviour
    {
        [SerializeField] Rigidbody2D diceRigidBody;
        /*Once changed to a die, consider changing to a different type of collider depending on each model
         * Have to figure out to work it out with ENUM.
         * OR considering its only 4 models, have prefabs of all 4 and have a separate scrips that just checks for
         * a collider type
         */
        [SerializeField] BoxCollider2D diceCollider;

        //Once switched to Tilecollider, create tag and have the object do an autosearch instead of manual insertion
        [SerializeField] BoxCollider2D groundCollider;
        [SerializeField] Transform target;
        [SerializeField] LineController linePrefab;
        [SerializeField] bool hasTouchedGround = false;
        [SerializeField] bool hasFullyStopped = false;
        [SerializeField] Transform throwingHand;
        [SerializeField] int xSpeedFactor = 1;
        [SerializeField] int ySpeedFactor = 2;
        [SerializeField] DiceType diceType;


        private bool stopInput = false;
        private bool hasMoved = false;
        private LineController newLine;
        private Vector2 throwSpeed = new Vector2();

        void Awake()
        {
            diceRigidBody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            stopInput = false;
            HideHand();
        }


        void Update()
        {
            hasFullyStopped = CheckForMagnitude();

            HandleThrow();


            //Manual Testing of deaths
            if (Input.GetKeyDown(KeyCode.Q))
            {
                DiceHealth.instance.breakDie();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                LevelManager.instance.EndLevel();
            }
        }

        private bool CheckForMagnitude()
        {
            if (diceRigidBody.velocity.normalized.magnitude <=0.1) 
            {
                return true;
            }
            //Debug.Log(diceRigidBody.velocity.ToString());
            return false;
        }


        private void HandleThrow()
        {

            if (stopInput) { return; }
            if (!hasTouchedGround) { return; }
            if (!hasFullyStopped) { return; }
            
            if(Input.GetMouseButtonDown(0) && hasTouchedGround)
            {
                newLine = Instantiate(linePrefab);
                newLine.AssignTarget(transform.position, target);
                ShowHand();
                

            }
            if (Input.GetMouseButton(0)) {
                Vector2 newThrowSpeed = CalculateVelocity(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                if(throwSpeed != newThrowSpeed)
                {
                    hasMoved = true;
                    throwSpeed = newThrowSpeed;
                }
                if (hasMoved)
                {
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                    target.localPosition = new Vector2(Mathf.Clamp(throwSpeed.x / xSpeedFactor, 0.1f, 3), Mathf.Clamp(throwSpeed.y / ySpeedFactor, 0.1f, 3));
                    newLine.AssignTarget(transform.position, target);
                    hasMoved = false;
                    
                }

            }
            if (Input.GetMouseButtonUp(0) && hasTouchedGround)
            {
                //throwSpeed = CalculateVelocity(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                //Debug.Log("throwspeed in button up: "+throwSpeed.ToString());
                diceRigidBody.velocity = throwSpeed;
                hasTouchedGround = false;
                HideHand();
                newLine.SelfDestruction();
            }
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "DeathPit")
            {
                Debug.Log("Has Entered the Pit");
                DiceHealth.instance.breakDie();
                //add function from the UI element
                //Add an ienumerable for the respawn thing
            }
            if (collision.gameObject.tag == "Ground")
            {
                AudioManager.audioManager.PlaySFX(UnityEngine.Random.Range(0, 14));
                hasTouchedGround = true;
                return;
            }      
            if(collision.gameObject.tag == "EndPoint")
            {
                LevelManager.instance.EndLevel();
            }

        }
       


        private void HideHand()
        {
            throwingHand.localScale = new Vector3(0, 0, 0);
        }


        private void ShowHand()
        {
            throwingHand.localScale = new Vector3(1, 1, 1);
            throwingHand.localRotation = Quaternion.Euler(0, 0, -17);
        }

        
        private Vector2 CalculateVelocity(Vector3 mousePos)
        {
            if (transform.position.x< mousePos.x) { return throwSpeed; }
            if (transform.position.y < mousePos.y) { return throwSpeed; }
            float x = Mathf.Clamp(Mathf.Abs(Mathf.Abs(transform.position.x) - Mathf.Abs(mousePos.x)),1,5);
            float y = Mathf.Clamp(Mathf.Abs(Mathf.Abs(transform.position.y) - Mathf.Abs(mousePos.y)),1,5);
            Vector2 velocity = new Vector2(x * xSpeedFactor, y * ySpeedFactor);
            //Debug.Log("actual Velocity: " + velocity);
            return velocity;
        }

        public void DisableInput()
        {
            stopInput = true;
        }




    }    
}