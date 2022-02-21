using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{   
    [SerializeField] float runSpeed=10f;
    [SerializeField] float jumpSpeed=5f;
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    CapsuleCollider2D myCapsuleCollider;
    
    Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody=GetComponent<Rigidbody2D>();
        myAnimator=GetComponent<Animator>();
        myCapsuleCollider=GetComponent<CapsuleCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
        
    }
    
    void OnMove(InputValue value )
    {

        moveInput=value.Get<Vector2>();
        Debug.Log(moveInput);
         
    }

    void OnJump(InputValue value)
    {   
        //if iam not touching the ground don't jump 
        
        if(!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            return;
        if(value.isPressed)
        {
            myRigidbody.velocity+=new Vector2(0f,jumpSpeed);
        }
    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x*runSpeed,myRigidbody.velocity.y);
        myRigidbody.velocity=playerVelocity;
        bool playerHorizontalSpeed=Mathf.Abs(myRigidbody.velocity.x)>Mathf.Epsilon;
        myAnimator.SetBool("isRunning",playerHorizontalSpeed);
    }

    void FlipSprite()
    {

        bool playerHorizontalSpeed=Mathf.Abs(myRigidbody.velocity.x)>Mathf.Epsilon;
        if(playerHorizontalSpeed)
        transform.localScale=new Vector2 (Mathf.Sign(myRigidbody.velocity.x),1f);

    }

}
