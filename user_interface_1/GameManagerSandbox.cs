using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class GameManagerSandbox : MonoBehaviour{
    [Header("Public Variables")]
    public Vector3 mousePosition;
    public Vector3 gridPosition;
    public new Camera camera;
    public LayerMask placementLayer;
    public Grid grid;
    public GameObject ignoreDestroy;
    public List<GameObject> Objects = new();
    public int activeObjectIndex;
    public Stack<Object> undoPlaceStack = new();
    public Stack<int> undoDelIndex = new();
    public Stack<Vector3> undoPosStack = new();
    public Stack<bool> undoType = new();
    private void Update(){
        if ((!EventSystem.current.IsPointerOverGameObject()) && Input.GetMouseButtonDown(0)){
            PlaceObject();
        }
        if ((!EventSystem.current.IsPointerOverGameObject()) && Input.GetKeyDown(KeyCode.LeftControl)){
            DeleteObject();
        }
        if (Input.GetKeyDown(KeyCode.Z) && undoType.Count > 0){
            if (undoType.Peek()){
                int objIndex = undoDelIndex.Pop();
                Vector3 position = undoPosStack.Pop();
                GameObject restoredObject = Instantiate(Objects[objIndex], position, Quaternion.identity);
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
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, placementLayer)){
            mousePosition = hit.point;
            gridPosition = grid.GetCellCenterWorld(grid.WorldToCell(mousePosition));
            Debug.DrawLine(mousePosition, mousePosition + Vector3.up * 1, Color.white);
            undoPlaceStack.Push(Instantiate(Objects[activeObjectIndex], gridPosition, Quaternion.identity));
            undoType.Push(false);
        }
    }
   public void DeleteObject(){
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = camera.nearClipPlane; 
        Ray ray = camera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, placementLayer)){
            if(!hit.collider.name.Equals(ignoreDestroy.name)){
                undoPosStack.Push(hit.collider.gameObject.transform.position);
                undoDelIndex.Push(0);
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