using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    public float sensitivity;
    public float normalSpeed;
    public float sprintSpeed;
    float currentSpeed;
    public Slider  normSpeedSlider;
    public Slider shiftSpeedSlider;
    public TextMeshProUGUI shiftInd;
    
    void Update()
    {
        Movement();
        if(!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButton(1)) //if we are holding right click
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Rotation();
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        normalSpeed = normSpeedSlider.value;
        sprintSpeed = shiftSpeedSlider.value;
    }

    public void Rotation()
    {
        Vector3 mouseInput = new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
        transform.Rotate(mouseInput * sensitivity);
        Vector3 eulerRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);
    }

    public void Movement()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        if(Input.GetKey(KeyCode.LeftShift)){
            currentSpeed = sprintSpeed;
            shiftInd.fontSize = 16;
        }else{
            currentSpeed = normalSpeed;
            shiftInd.fontSize = 20;
        }
        transform.Translate(input * currentSpeed * Time.deltaTime);
    }
}