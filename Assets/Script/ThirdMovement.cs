using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ThirdMovement : MonoBehaviour
{

    [Header("Player Controller")]
    public CharacterController controller;
    public Transform cam;
    public Cinemachine.CinemachineFreeLook cinemachine;
    [SerializeField] private float gravity;

    [Header("Player info Movement")]
    public float speed = 4f;
    Vector3 moveDir;
    
    [SerializeField] float RotationSpeed = 5f;
    public bool CanMove = true;


    [Header("Input Touch")]
    public FixedJoystick joyStick;


    [Header("Jump")]
    private float gravityY;
    public bool isJump = false;
    public bool CanJump = true;

    [Header("CheckGround")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask groundMask;

    [Header("Animation")]
    private Animator anim;
    bool stepOffsetGround = false;

    [Header("Attack")]
    public bool isAttack = false;
    public bool CanAttack = true;

    [Header("Rotation")]
    public Quaternion angle;
    public bool CanRotation = true;

    
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        angle = transform.rotation;
    }
    // Update is called once per frame
    void Update()
    {

        Move();
        ChamDau();

    }
    
    void Move()
    {
        Vector3 direction = new Vector3(joyStick.Horizontal, 0f, joyStick.Vertical).normalized;
        #region Xu Ly Trong Luc
        if (controller.isGrounded && gravityY < 0)
        {
            isJump = false;
            CanAttack = true;
            gravityY = gravity;
        }
        if (!controller.isGrounded)
        {
            CanAttack = false;
            gravityY += gravity * Time.deltaTime;
        }
        #endregion   
        #region Animation
        stepOffsetGround = Physics.CheckCapsule(transform.position, transform.position + Vector3.down * controller.stepOffset, groundDistance, groundMask);
        if (isJump || !stepOffsetGround && !controller.isGrounded)//Nhay
        {
            isGrounded = false;
            controller.stepOffset = 0.05f;
        }
        else
        {
            isGrounded = true; //Khong Nhay
            controller.stepOffset = 0.5f;
        }
        //3. Thiet Lap Animation
        anim.SetBool("Grounded", isGrounded);
        anim.SetBool("isJump", isJump);
        anim.SetBool("Attack", isAttack);
        if (direction == Vector3.zero)
        {
            Idle();
        }
        else if (!isAttack && isGrounded)
        {
            Run();
        }
        #endregion
        #region Tinh Toan Di Chuyen Theo Cam & Xoay
        //4. Tinh Toan Di Chuyen
        moveDir = Vector3.zero;
        if (direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            angle = Quaternion.Euler(0f, targetAngle, 0f);
            moveDir = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward);
            moveDir *= speed * SpeedDownAngleRotate(Quaternion.Angle(transform.rotation, angle));
        }
        //5. Xoay Player
        if (Quaternion.Angle(transform.rotation, angle) != 0 && CanRotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, angle, RotationSpeed * Time.deltaTime);
        }
        moveDir.y = gravityY;
        if(CanMove)
        controller.Move(moveDir * Time.deltaTime);
        #endregion 
    }
    

    
    float SpeedDownAngleRotate(float cx)
    {
        if (cx > 91f && controller.velocity == Vector3.zero)
            return 0f;
        return 1f;
    }
    private void Idle()
    {
        //moveSpeed = 0;
        anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
       
    }
    private void Run()
    {
        anim.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
       
    }
    public void buttonJump()
    {
        if(isGrounded && CanJump)
        {
            isJump = true;
            float jumpHeight = 5f;
            gravityY = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }
    void ResetJump()
    {
        if (controller.isGrounded)
        {
            isJump = false;
            CanAttack = true;
        }
        else
        {
            CanAttack = false;
            gravityY += gravity * Time.deltaTime;
        }
    }
    void ChamDau()
    {
        if(controller.collisionFlags == CollisionFlags.Above)
        {
            gravityY = -4f;
        }
    }
    //    Vector3 impact = Vector3.zero;
    //float mass = 3f;
    //KT xem co nen bu dap 1 luc de dua nhan vat xuong khi dang dung ho hinh
    //if(!Physics.CheckCapsule(transform.position, transform.position + Vector3.down * controller.stepOffset, groundDistance, groundMask) && !isJump && controller.isGrounded)
    //{
    //    gravityY = -3f;     
    //}
    //else if (!Physics.CheckCapsule(transform.position, transform.position + Vector3.down * controller.stepOffset, 0.1f, groundMask) && !isJump && controller.isGrounded)
    //{
    //    gravityY = -3f;

    //}
    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    RayCastSlopeNormal = hit.normal;
    //}
    //[Header("CheckSpeedOfSlope")]
    //[SerializeField] public Vector3 RayCastSlopeNormal;
    //float speedSlopeNormal;
    //[SerializeField] private float MinSlopeLimit = 20;
    //public void AddImpact(Vector3 dir, float force)
    //{
    //    dir.Normalize();
    //    if (dir.y < 0) dir.y = -dir.y; // reflect down force on the ground
    //    impact += dir.normalized * force / mass;
    //}
    //#region Xu ly Slope => Speed
    //if (controller.isGrounded) 
    //{
    //    if(Vector3.Angle(RayCastSlopeNormal, transform.up) <= controller.slopeLimit)
    //    {
    //        if(Vector3.Angle(RayCastSlopeNormal, transform.up) > 45f)
    //        {
    //            speedSlopeNormal = Mathf.Clamp(MinSlopeLimit * (2f/3f) / Vector3.Angle(RayCastSlopeNormal, transform.up), 0.1f, 1f);
    //        }
    //        else
    //        {
    //            speedSlopeNormal = Mathf.Clamp(MinSlopeLimit / Vector3.Angle(RayCastSlopeNormal, transform.up), 0.1f, 1f);
    //        }
    //        //di xuong doc be thi khong tinh
    //        if (Mathf.Abs(Vector3.Angle(RayCastSlopeNormal, transform.forward) - 90) <= MinSlopeLimit)
    //        {
    //            speedSlopeNormal = 1f;
    //        }
    //    }
    //    else
    //    {
    //        //Khi Player dung tren doc khong cho phep nhan vat se truoc xuong 
    //        Vector3 SpeedSlopeDown = new Vector3(RayCastSlopeNormal.x, -RayCastSlopeNormal.y, RayCastSlopeNormal.z).normalized;

    //        controller.Move(SpeedSlopeDown * 2f * Time.deltaTime);
    //    }

    //}
    //else if(!isGrounded && controller.isGrounded)
    //{
    //    Vector3 SpeedSlopeDown = new Vector3(RayCastSlopeNormal.x, -RayCastSlopeNormal.y, RayCastSlopeNormal.z).normalized;

    //    controller.Move(SpeedSlopeDown * 2f * Time.deltaTime);
    //}
    //#endregion
}
