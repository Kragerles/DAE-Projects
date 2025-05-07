using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class GameManagerSandbox : MonoBehaviour{
    [Header("Public Variables")]
    public Vector3 mousePosition;
    public Vector3 gridPosition;
    public new Camera camera;
    public LayerMask placementLayer;
    public Grid grid;
    public GameObject[] ignoreDestroy;
    [SerializeField] private TMP_Dropdown structureDD;
    [SerializeField] private TMP_Dropdown gunDD;
    public GameObject[] structureParts;
    public GameObject[] gunParts;
    public Dictionary<int,GameObject> partsDB = new();
    public int activeObject = new();
    public Stack<GameObject> undoPlaceStack = new();
    public Stack<int> undoDelIndex = new();
    public Stack<Vector3> undoPosStack = new();
    public Stack<bool> undoType = new();
    void Awake(){
        foreach(GameObject item in structureParts){
            string s = item.name;
            partsDB.Add(s.GetHashCode() , item);
        }
        foreach(GameObject item in gunParts){
            string s = item.name;
            partsDB.Add(s.GetHashCode() , item);
        }
        Console.WriteLine(partsDB.ToString());
        activeObject = structureParts[0].name.GetHashCode();
    }
    private void Update(){
        structureDD.onValueChanged.AddListener(delegate {
            string s = structureDD.options[structureDD.value].text;
            activeObject = s.GetHashCode();
        });
        gunDD.onValueChanged.AddListener(delegate {
            string s = gunDD.options[gunDD.value].text;
            activeObject = s.GetHashCode();
        });
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
                GameObject restoredObject = Instantiate(partsDB[activeObject], position, Quaternion.identity);
                undoPlaceStack.Push(restoredObject);
            }else{
                if (undoPlaceStack.Peek() != null){
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
            undoPlaceStack.Push(Instantiate(partsDB[activeObject], gridPosition, Quaternion.identity));
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
}