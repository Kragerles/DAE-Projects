using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float boostMod = 2f;
    public float rotateSpeed = 2f;
    public bool InvertY;
    public KeyCode moveDown;
    public KeyCode moveUp;
    public KeyCode moveForward;
    public KeyCode moveBack;
    private float yaw = 0f;
    private float pitch = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
    }

    // Update is called once per frame
    void Update(){
        handleMovement();
        HandleRotation();
    }

    void handleMovement(){
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        float moveZ = 0f;

        if(Input.GetKey(moveUp))
            moveY += 1f;
        if(Input.GetKey(moveDown))
            moveY -= 1f;
        if(Input.GetKey(moveForward))
            moveZ += 1f;
        if(Input.GetKey(moveBack))
            moveZ -= 1f;

        float currentSpeed = moveSpeed * (Input.GetKey(KeyCode.LeftShift) ? boostMod:1);

        Vector3 movementX = transform.right * moveX;
        Vector3 movementY = transform.up * moveY;
        Vector3 movementZ = transform.forward * moveZ;
        transform.position += movementX * currentSpeed * Time.deltaTime;
        transform.position += movementY * currentSpeed * Time.deltaTime;
        transform.position += movementZ * currentSpeed * Time.deltaTime;
    }

    void HandleRotation(){
        if(Input.GetMouseButton(2)){
        float mouseX = Input.GetAxis("Moust X") * rotateSpeed;
        float mouseY = Input.GetAxis("Moust Y") * rotateSpeed;

        yaw += mouseX;
        pitch -= mouseY;

        pitch = Mathf.Clamp(pitch,-90,90);

        transform.rotation = Quaternion.Euler(pitch,yaw,0f);
        }
    }
}
