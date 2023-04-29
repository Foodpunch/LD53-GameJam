using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public float moveSpeed;
    public float ROLLSPEED = 120f;
    Rigidbody2D _rb;
    
    public Transform gunHolder;
    public SpriteRenderer gunSprite;
    Vector2 playerInput;
    Vector2 moveVelocity;
    Vector2 mouseInput;
    Vector2 mouseDirection;
    Vector2 slideDir;

    float nextTimeToRoll;
    
    enum PlayerState{
        NORMAL,
        ROLLING,
        DEAD
    }

    [SerializeField]
    PlayerState _state;

    void Awake(){
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _state = PlayerState.NORMAL;
    }

    // Update is called once per frame
    void Update()
    {
        switch(_state){
            case PlayerState.NORMAL:
            PlayerMovementInput();
            SetGunPointToMouse();
            break;
            case PlayerState.ROLLING:
            break;
            case PlayerState.DEAD:
            break;
        }
    }
    void FixedUpdate(){
        _rb.MovePosition(_rb.position+moveVelocity*Time.fixedDeltaTime);
    }
    void PlayerMovementInput(){
        playerInput = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        moveVelocity = playerInput * moveSpeed;
        //if velocity > certain number, play walking anim
    }

    void SetGunPointToMouse(){
        //should prolly use Atan2 for this but meh
        mouseInput = Input.mousePosition;
        Vector3 mousePosInWorld = Camera.main.ScreenToWorldPoint(mouseInput);
        mousePosInWorld.z = 0;
        mouseDirection = mousePosInWorld - transform.position;
        gunHolder.transform.right = mouseDirection;

        //Hacky sprite flip thing here
        float angle = gunHolder.transform.rotation.eulerAngles.z;
        gunSprite.flipY = (angle > 90f && angle < 270f);
    }
}
