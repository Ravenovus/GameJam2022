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


        private float parabolaHeight;
        private bool hasMoved = false;
        private LineController newLine;
        private Vector2 throwSpeed = new Vector2();

        void Awake()
        {
            diceRigidBody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            HideHand();
        }


        void Update()
        {
            hasFullyStopped = CheckForVelocity();
            HandleThrow();

        }

        private bool CheckForVelocity()
        {
            Vector2 checker = new Vector2(0, 0);
            if (diceRigidBody.velocity.normalized.Equals(checker.normalized)) 
            {
                return true;
            }
            //Debug.Log(diceRigidBody.velocity.ToString());
            return false;
        }

        /*
        * Distance based angle/strength management
        * further you place your mouse from the die while holding down the button, the stronger
        * you shoot it/ harder angle
        * Implement strength limitations based on die type
        */
        private void HandleThrow()
        {
            

            if (!hasTouchedGround) { return; }
            if (!hasFullyStopped) { return; }

            if(Input.GetMouseButtonDown(0) && hasTouchedGround)
            { 
                ShowHand();
                newLine = Instantiate(linePrefab);
                newLine.AssignTarget(transform.position, target);

            }
            if (Input.GetMouseButton(0)) {
                Vector2 newThrowSpeed = CalculateVelocity(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                if(throwSpeed != newThrowSpeed)
                {
                    hasMoved = true;
                    throwSpeed = newThrowSpeed;
                    //calculate angle etc here
                }
                if (hasMoved)
                {
                    target.localPosition = new Vector2(Mathf.Clamp(throwSpeed.x / xSpeedFactor, 0.1f, 3), Mathf.Clamp(throwSpeed.y / ySpeedFactor, 0.1f, 3));
                    Debug.Log(target.localPosition);
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
            if (collision.gameObject.tag == "Ground")
            {
                hasTouchedGround = true;                
            }
        }

        private void HideHand()
        {
            throwingHand.localScale = new Vector3(0, 0, 0);
        }


        private void ShowHand()
        {
            throwingHand.localScale = new Vector3(1, 1, 1);
            throwingHand.rotation = Quaternion.Euler(0, 0, 60);
        }

        
        private Vector2 CalculateVelocity(Vector3 mousePos)
        {
            if(transform.position.x< mousePos.x) { return throwSpeed; }
            if (transform.position.y < mousePos.y) { return throwSpeed; }
            float x = Mathf.Abs(Mathf.Abs(transform.position.x) - Mathf.Abs(mousePos.x));
            float y = Mathf.Abs(Mathf.Abs(transform.position.y) - Mathf.Abs(mousePos.y));
            Vector2 velocity = new Vector2(x * xSpeedFactor, y * ySpeedFactor);
            //Debug.Log("actual Velocity: " + velocity);
            return velocity;
        }

        




    }    
}