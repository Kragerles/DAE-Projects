using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameManagerSandbox : MonoBehaviour
{
    [Header("Public Variables")]
    public Vector3 mousePosition;
    public Vector3 gridPosition;
    public new Camera camera;
    public LayerMask placementLayer;
    public Grid grid;

    public GameObject ignoreDestroy;

    public List<GameObject> Objects = new();
    public int activeObjectIndex;
    public Stack<UnityEngine.Object> undoStack = new();
    public Stack<bool> undoType = new();

    private void Update(){
        if (Input.GetMouseButtonDown(0)){
            PlaceObject();
        }
        if(Input.GetKeyDown(KeyCode.LeftControl)){
            DeleteObject();
        }
        if(Input.GetKeyDown(KeyCode.Z)){
            if(undoType.Pop()==true){
                Instantiate(undoStack.Pop());
            }else{
                Destroy(undoStack.Pop());
            }
        }
    }

    public void PlaceObject()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = camera.nearClipPlane;
        Ray ray = camera.ScreenPointToRay(mousePos);
        RaycastHit hit;

        //Shoot Raycast
        if (Physics.Raycast(ray, out hit, 100, placementLayer))
        {
            //Set all mouse and grid position Variables
            mousePosition = hit.point;
            gridPosition = grid.GetCellCenterWorld(grid.WorldToCell(mousePosition));
            Debug.DrawLine(mousePosition, mousePosition + Vector3.up * 1, Color.white);
            undoStack.Push(Instantiate(Objects[activeObjectIndex], gridPosition, Quaternion.identity));
            undoType.Push(false);
        }
    }

    public void DeleteObject(){
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = camera.nearClipPlane;
        Ray ray = camera.ScreenPointToRay(mousePos);
        RaycastHit hit;

        //Shoot Raycast
        if (Physics.Raycast(ray, out hit, 100, placementLayer)){
            //deletes item if not the grid
            if(!hit.collider.name.Equals(ignoreDestroy.name)){
                UnityEngine.Object copyOfDeleted = hit.collider.gameObject;
                undoStack.Push(copyOfDeleted);
                Destroy(hit.collider.gameObject);
                undoType.Push(true);
            }  
        }
    }

    public void ChangeObject(int index)
    {
        activeObjectIndex = index;
    }
}