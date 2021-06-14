using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    private bool jumpKeyWasPressed; // bool will check true or false 
    private float horizontalInput; 
    private Rigidbody rigidbodyComponent;
    private bool isGrounded;

    private int superJumpsRemaining;
    [SerializeField] private Transform groundCheckTransform; //public Transform groundCheckTransfrom;
    [SerializeField] private LayerMask playerMask;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Space bar
        if (Input.GetKeyDown(KeyCode.Space)){
            jumpKeyWasPressed = true;
        }
        horizontalInput = Input.GetAxis("Horizontal");
    }

    //FixedUpdate is called once every physic update
    private void FixedUpdate(){
        rigidbodyComponent.velocity = new Vector3(horizontalInput,rigidbodyComponent.velocity.y,0);

       if(Physics.OverlapSphere(groundCheckTransform.position,0.1f,playerMask).Length == 0){
           return ; 
       }
        
        // if(!isGrounded){
            // return ; 
        // }

        //Space bar
        if (jumpKeyWasPressed){
            float jumpPower = 5.0f;
            if (superJumpsRemaining > 0){
                jumpPower *= 2;
                superJumpsRemaining--;
            }
            rigidbodyComponent.AddForce(Vector3.up * jumpPower,ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
            Debug.Log("Space key was pressed down");
        }
        
    }

//    private void OnCollisionEnter(Collision collision){
//        isGrounded = true;
//    }

    //  private void OnCollisionExit(Collision collision){
        // isGrounded = false;
    // }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.layer == 9){
            Destroy(other.gameObject);
            superJumpsRemaining++;
        }
    }
}