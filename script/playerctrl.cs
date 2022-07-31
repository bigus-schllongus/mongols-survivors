using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Takes and handles input and movement for a player character
public class playerctrl : MonoBehaviour
{

    public float moveSpeed = 2f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public Animator animator;
    public projectile ProjectilePrefab;
    public Transform LaunchOffset;
    private bool fire;
    private Vector3 fireDirection;



    Vector2 movementInput;
    SpriteRenderer spriterenderer;
    Rigidbody2D rb;
    

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
        spriterenderer=GetComponent<SpriteRenderer>();
    }
    private void Update(){

        fire = Input.GetButtonDown("Fire1");
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        fireDirection = (mousePos-transform.position).normalized;
                    if (fire)
            {
               projectile inst=Instantiate(ProjectilePrefab,LaunchOffset.position,transform.rotation);
               //inst.transform.LookAt(mousePos);
            }



    }

    private void FixedUpdate() {
            // if mvmt input is not 0 , try to move
            if (movementInput != Vector2.zero){
                // check for potential collisions
                int count = rb.Cast(
                    movementInput, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
                    movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
                    castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                    moveSpeed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset

                if(count == 0){
                    rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);

                }
                animator.SetBool("isMoving",true);
            }else{               
                animator.SetBool("isMoving",false);
            }
            if(movementInput.x < 0) {
                spriterenderer.flipX= true;
            }else if(movementInput.x > 0){
                spriterenderer.flipX= false;
            }

        }
    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    }
}