using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private CharacterController controller;
    private Camera playerCamera;
    
    [SerializeField] public Vector3 speed;
    [SerializeField] private Vector3 rotation  = Vector3.zero;

    [Space (15)]
    [Header("Ground Movement")]
    [SerializeField] public float walkingSpeed;
    [SerializeField] public float runSpeed;
    [SerializeField] public float crouchingSpeed;
    [SerializeField] private float minHeight, maxHeight;
    private bool isCrouching;

    [Space (15)]
    [Header("Jump Controls")]
    [SerializeField] public float gravity;
    [SerializeField] public float jumpSpeed;

    [Space (15)]
    [Header("Air Movement")] 
    [SerializeField] public float airFriction;
    [SerializeField] public float maxSpeed, airMovementSpeed;

    [Space(15)] [Header("Camera settings")] 
    [SerializeField] private float defaultCameraHeight;
    [SerializeField] private float speedH;    // Sensibilidad horizontal de la c�mara
    [SerializeField] private float speedV;    // Sensibilidad vertical de la c�mara
    [SerializeField] private float minCameraDown, maxCameraUp;

    private float bobSpeed;
    private float timerBob = 0f;
    private Vector2 prevPosition = Vector2.zero;
    private float lastSpeedBob = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponentInChildren<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
        
        speed = Vector3.zero;
        bobSpeed = 10;

        isCrouching = false;
    }

    // Update is called once per frame
    private void Update()
    {
        
        
        ManageInput(ref speed);
        ManageRotation();
        
        controller.Move(speed * Time.deltaTime);
        Rotate();
        HeadBobble();

        ApplyHalfAcceleration(ref speed);
        transform.position = controller.transform.position;

        var height = controller.height;
        if (isCrouching)
        {
            if (height > minHeight)
            {
                controller.height -= Time.deltaTime * 10f;
                if (controller.height < minHeight) controller.height = minHeight;
            }
        }else if (height < maxHeight)
        {
            controller.height += Time.deltaTime * 10f;
            if (controller.height > maxHeight) controller.height = maxHeight;
        }
        
        
        ApplyHalfAcceleration(ref speed);

        if (transform.position.y < -10)
        {
            var pos = new Vector3(0, 4.5f, 0);
            transform.position = pos;
            controller.transform.position = pos;
            playerCamera.transform.position = pos;
        
            Physics.SyncTransforms();
        }
    }
    
    
    private void HeadBobble()
    {
        var position = transform.position;
        var realSpeed = (prevPosition - new Vector2(position.x, position.z))/Time.deltaTime;
        var realSpeedMag = (realSpeed.magnitude + lastSpeedBob)/2f;
        if(Mathf.Abs(realSpeed.x) > 0.1f || Mathf.Abs(realSpeed.y) > 0.1f)
        {
            //Player is moving
            timerBob += Time.deltaTime * bobSpeed;
            var localPosition = playerCamera.transform.localPosition;
            playerCamera.transform.localPosition = new Vector3(localPosition.x, defaultCameraHeight + Mathf.Sin(timerBob)  / 15f, localPosition.z);
        }
        else
        {
            //Idle
            timerBob = 0;
            var localPosition = playerCamera.transform.localPosition;
            localPosition = new Vector3(localPosition.x, Mathf.Lerp(localPosition.y, defaultCameraHeight, Time.deltaTime * bobSpeed), localPosition.z);
            playerCamera.transform.localPosition = localPosition;
        }

        lastSpeedBob = realSpeedMag;
        prevPosition = new Vector2(position.x,position.z);
    }
    
    /*
     * Manages input. Currently it manages:
     * Vertical and Horizontal axis, get converted to horizontal movement 
     * Space bar, gets converted into a jump if the controller is grounded
     */
    private void ManageInput(ref Vector3 vel)
    {
        //GetAxisRaw returns 1 if pressing the corresponding buttons, 0 if not. (raw means there will be no smoothing, resulting in an immediate stop)
        var tempDir = (transform.forward * Input.GetAxisRaw("Vertical") + transform.right*Input.GetAxisRaw("Horizontal"));
        
        //Normalizes the vector (if both inputs are 1, it distributes the speed between both axis instead of
        tempDir.Normalize();
        
        if (controller.isGrounded)
        {
            isCrouching = Input.GetAxisRaw("Fire1") > 0;
            var isRunning = Input.GetAxis("Fire3") > 0 ? runSpeed :walkingSpeed;
            if (isCrouching) isRunning = crouchingSpeed;
            tempDir *= isRunning;
            
            vel.y = jumpSpeed * Input.GetAxis("Jump");
            
            vel.x = tempDir.x; //sides
            vel.z = tempDir.z; //forward-back
        }
        else
        {
            tempDir *= airMovementSpeed;
            
            vel.x += tempDir.x * Time.deltaTime; //sides
            vel.z += tempDir.z * Time.deltaTime; //forward-back

            var mag = Mathf.Sqrt(Mathf.Pow(vel.x, 2) + Mathf.Pow(vel.z, 2));
            if (mag > maxSpeed)
            {
                //Its the same as normalizing it and the multiplying by max speed
                //vel.mag/maxSpeed is the % its going over the maximum, so we just have to divide by it
                //Only taking into account the horizontal plane velocities, so as to not change gravity!!!
                var changeRate = mag / maxSpeed;
                vel.x /= changeRate;
                vel.z /= changeRate;
            }
            //if (Mathf.Abs(vel.x) > maxSpeed) vel.x = maxSpeed * Mathf.Sign(vel.x);
            //if (Mathf.Abs(vel.z) > maxSpeed) vel.z = maxSpeed * Mathf.Sign(vel.z);
        }
    }

    private void ApplyHalfAcceleration(ref Vector3 vel)
    {
        
        if (!controller.isGrounded)
        {
            //// GRAVITY
            vel.y -= gravity * Time.deltaTime / 2f;
            
            //// AIR FRICTION
            
            //Calculates the magnitude (m) of the current speed, and from that, we calculate the desired magnitude (m')
            //We then divide m/m' to get the proportion, and divide the speed vector (x and z coordinates only, y is gravity) by it
            //This ensures the speed is evenly distributed throughout all directions
            
            var magnitude = Mathf.Sqrt(Mathf.Pow(vel.x, 2) + Mathf.Pow(vel.z, 2));
            var newMagnitude = magnitude - (airFriction*Time.deltaTime / 2f);
            if(newMagnitude > 0.001)    
            {
                var k = magnitude / newMagnitude;
                vel.x /= k;
                vel.z /= k;
            }

        }
        else
        {
            speed.y = -8.2f;
        }
    }

    private void ManageRotation()
    {
        //Get current rotation
        var rot = playerCamera.transform.rotation.eulerAngles;
        var rotY = 0f;
        var rotX = 0f;
        if (Cursor.visible)
        {
            if (Input.GetMouseButton(0))
            {
                
                //si se pulsa y no esta esccondido el raton, se esconde
                //Cursor.visible = false; 
                
                
            }
        }
        else
        {
            
            //Mouse X rotation makes the camera rotate around the Y axis (points up)
            rotY = Input.GetAxis("Mouse X") * speedH;
            //Mouse Y rotation makes the camera rotate around the X axis (points right)
            rotX = -Input.GetAxis("Mouse Y") * speedV;
            //Prevents the camera from rotating too much up or down (its a hard limit)
            rotX *= ((rot.x + rotX > minCameraDown && rot.x < 180) || (rot.x + rotX < maxCameraUp && rot.x>180)) ? 0 : 1;
            
            //Si el raton esta escondido, se coloca en el centro para poder moverlo indefinidamente a los lados
            //Mouse.current.WarpCursorPosition(new Vector2(Screen.width / 2f, Screen.height / 2f));
            //Cursor.lockState = CursorLockMode.Locked;
        }
        
        rot += new Vector3(rotX, rotY,0f);
        
        
        AdjustAngle(ref rot.x, minCameraDown, maxCameraUp);
        AdjustAngle(ref rot.z, 0, 360);
        rotation = new Vector3(rot.x, rot.y, rot.z);
    }
    
    private void AdjustAngle(ref float angle, float min, float max)
    {
        //var middle =  min+max == 0 ? 180 : (max + min) / 2f;
        if (angle < 180)
        {
            if (angle > min)
            {
                angle -= 100 * Time.deltaTime;
                if (angle < min) angle = min;
            }
        }
        else
        {
            if (angle < max)
            {
                angle += 100 * Time.deltaTime;
                if (angle > max) angle = max;
            }
        }
    }

    private void Rotate()
    {
        transform.eulerAngles = new Vector3(0, rotation.y, 0);
        playerCamera.transform.eulerAngles = rotation;
    }
    
}
