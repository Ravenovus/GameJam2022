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
        [SerializeField] LaunchArcRenderer launchArcRenderer;

        [SerializeField] bool hasTouchedGround = false;
        [SerializeField] bool hasFullyStopped = false;
        [SerializeField] Transform throwingHand;
        [SerializeField] int xSpeedFactor = 1;
        [SerializeField] int ySpeedFactor = 2;


        private float parabolaHeight;

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
            Debug.Log(diceRigidBody.velocity.ToString());
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
                

            }
            Vector2 throwSpeed = CalculateVelocity(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            launchArcRenderer.RenderArc(transform.position);
            //Debug.Log("throwspeed after calculation: " + throwSpeed.ToString());
            if (Input.GetMouseButtonUp(0) && hasTouchedGround)
            {
                //Debug.Log("throwspeed in button up: "+throwSpeed.ToString());
                diceRigidBody.velocity = throwSpeed;
                hasTouchedGround = false;
                HideHand();
               
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

        //IMPORTANT//
        /* Once actuall colliders are set for each model,
         * Have the hand appear ONLY once velocity is at 0
         * It will be possible when the collider is not a fucking circle
         */

        private void ShowHand()
        {
            throwingHand.localScale = new Vector3(1, 1, 1);
            throwingHand.rotation = Quaternion.Euler(0, 0, 60);
        }

        //Function to create the parabola, requires previous function to find start and end of it

        //
        
        private void HandleParabolaCalculations()
        {
            Vector3 startingPosition = GetComponent<Transform>().position;


        }
        
        private Vector2 CalculateVelocity(Vector3 mousePos)
        {
            float x = Mathf.Abs(transform.position.x - mousePos.x);
            float y = Mathf.Abs(transform.position.y - mousePos.y);
            Vector2 velocity = new Vector2(x * xSpeedFactor, y * ySpeedFactor);
            return velocity;
        }




    }    
}