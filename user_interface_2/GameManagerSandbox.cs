using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class GameManagerSandbox : MonoBehaviour{
    [Header("Public Variables")]
    public Vector3 mousePosition;
    public Vector3 gridPosition;
    public new Camera camera;
    public LayerMask placementLayer;
    public Grid grid;
    public GameObject[] ignoreDestroy;
    public GameObject[] parts;
    public TMP_Dropdown structureDD;
    public TMP_Dropdown gunPartsDD;
    public int activeObjectIndex = 0;
    public Stack<Object> undoPlaceStack = new();
    public Stack<int> undoDelIndex = new();
    public Stack<Vector3> undoPosStack = new();
    public Stack<bool> undoType = new();
    private void Update(){
        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0)){
            PlaceObject();
        }
        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetKeyDown(KeyCode.LeftControl)){
            DeleteObject();
        }
        if (Input.GetKeyDown(KeyCode.Z) && undoType.Count > 0){
            if (undoType.Peek()){
                int objIndex = undoDelIndex.Pop();
                Vector3 position = undoPosStack.Pop();
                GameObject restoredObject = Instantiate(parts[activeObjectIndex], position, Quaternion.identity);
                undoPlaceStack.Push(restoredObject);
            }else{
                if ((GameObject)undoPlaceStack.Peek()!=null){
                    Destroy(undoPlaceStack.Pop());
                }
            }
            undoType.Pop();
        }
    }
    public void PlaceObject(){
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = camera.nearClipPlane;
        Ray ray = camera.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, placementLayer)){
            mousePosition = hit.point;
            gridPosition = grid.GetCellCenterWorld(grid.WorldToCell(mousePosition));
            Debug.DrawLine(mousePosition, mousePosition + Vector3.up * 1, Color.white);
            undoPlaceStack.Push(Instantiate(parts[activeObjectIndex], gridPosition, Quaternion.identity));
            undoType.Push(false);
        }
    }
   public void DeleteObject(){
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = camera.nearClipPlane;
        Ray ray = camera.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, placementLayer)){
            if (!ignoreDestroy.Contains(hit.collider.gameObject)){
                undoPosStack.Push(hit.collider.gameObject.transform.position);
                if(hit.collider.name.Equals("Cube(Clone)"))
                    undoDelIndex.Push(0);
                if(hit.collider.name.Equals("Cylinder(Clone)"))
                    undoDelIndex.Push(1);
                if(hit.collider.name.Equals("Sphere(Clone)"))
                    undoDelIndex.Push(2);
                Destroy(hit.collider.gameObject);
                undoType.Push(true);
            }
        }
    }
    public void ChangeObject(int index)
    {
        
    }
}