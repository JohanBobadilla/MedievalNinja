using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    public Animator playerAnimator;
    public GameObject virtualCameraPrefab;

    [Header("Parameters")]
    public float walkSpeed = 1f;
    public float runSpeed = 3f;
    private float move_x;
    private float move_y;
    private bool isFacingRight;
    private bool isAttacking;
    private bool isRunning;
    private bool canMove;

    void Start()
    {
        SetUpCamera();
        SetInitialValues();
    }

    private void SetInitialValues()
    {
        canMove = true;
        isFacingRight = (this.transform.localScale.x > 0) ? true : false;
    }

    private void SetUpCamera()
    {
        GameObject camera = GameObject.FindGameObjectWithTag("VirtualCamera");
        if (camera == null)
        {
            Camera.main.gameObject.AddComponent<CinemachineBrain>();
            camera = Instantiate(virtualCameraPrefab, Vector3.zero, Quaternion.identity);
            Debug.LogWarning("No Virtual camera in the Scene, instatiating one...");
        }

		CinemachineVirtualCamera virtualCamera = camera.GetComponent<CinemachineVirtualCamera>();
		virtualCamera.Follow = this.transform;
	}

    void Update()
    {
        if (canMove)
        {
            move_x = Input.GetAxisRaw("Horizontal");
            move_y = Input.GetAxisRaw("Vertical");
            isRunning = Input.GetKey(KeyCode.LeftShift);
            isAttacking = Input.GetKeyDown(KeyCode.Z);
            Flip();
            UpdateAnimator();
        }
    }

    void FixedUpdate() {
        
        if (canMove)
        {
            float finalSpeed = isRunning ? runSpeed : walkSpeed;
            if (rb.velocity.magnitude > finalSpeed)
            {
                rb.velocity = rb.velocity.normalized * finalSpeed;
            }
            else
            {
                rb.velocity = new Vector2(move_x * finalSpeed, move_y * finalSpeed);
            }
        }
    }

    private void Flip(){

        if ((!isFacingRight && move_x > 0) || (isFacingRight && move_x < 0))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = this.transform.localScale;
            localScale.x *= -1;
            this.transform.localScale = localScale;
        }
    }

    private void UpdateAnimator()
    {
        float move = move_x == 0 ? move_y : move_x;
        move *= isRunning ? runSpeed : 1; 
        playerAnimator.SetFloat("move", move);
        
        if (isAttacking)
        {
            playerAnimator.SetTrigger("attack");
        }
        
    }


}
