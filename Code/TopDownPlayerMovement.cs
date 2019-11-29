using UnityEngine;
using UnityEngine.InputSystem;


public class TopDownPlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    private Rigidbody2D rb2d;

    private Vector2 movement;
    private Vector2 rotate;
    private float angle;
    private float defaultRotation;

    private UnityEngine.InputSystem.Gamepad gamepad;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Left stick
        if(gamepad == null)
        {
            Debug.Log("no pad connected");
            return;
        }

        movement = gamepad.leftStick.ReadValue();

        //Right stick
        rotate = gamepad.rightStick.ReadValue();
    }

    void FixedUpdate()
    {
        if(gamepad == null)
        {
            return;
        }

        //Move
        rb2d.MovePosition(
            rb2d.position
            + movement 
            * moveSpeed 
            * Time.fixedDeltaTime
        );

        //Rotate
        angle = Mathf.Atan2(rotate.y,rotate.x);
        transform.rotation = Quaternion.Euler(0f,0f,angle * Mathf.Rad2Deg + defaultRotation);
    }

    public void Init(UnityEngine.InputSystem.Gamepad gamepad,float defaultRotation)
    {
        SetGamePad(gamepad);
        SetDefaultRotation(defaultRotation);
    }

    private void SetGamePad(UnityEngine.InputSystem.Gamepad gamepad)
    {
        this.gamepad = gamepad;
    }

    private void SetDefaultRotation(float defaultRotation)
    {
        this.defaultRotation = defaultRotation;
    }
    
}