using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManagerSandbox : MonoBehaviour
{
    [Header("Public Variables")]
    public Vector3 mousePosition;
    public Vector3 gridPosition;
    public Camera camera;
    public LayerMask placementLayer;
    public Grid grid;

    public GameObject ignoreDestroy;

    public List<GameObject> Objects = new List<GameObject>();
    public int activeObjectIndex;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            placeObject();
        }
        if(Input.GetKeyDown(KeyCode.LeftControl)){
            deleteObject();
        }
    }

    public void placeObject()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = camera.nearClipPlane;
        Ray ray = camera.ScreenPointToRay(mousePos);
        RaycastHit hit;

        //Shoot Raycast
        if (Physics.Raycast(ray, out hit, 100, placementLayer))
        {
            //Sett all mouse and grid position Variables
            mousePosition = hit.point;
            gridPosition = grid.GetCellCenterWorld(grid.WorldToCell(mousePosition));
            Debug.DrawLine(mousePosition, mousePosition + Vector3.up * 1, Color.white);
            Instantiate(Objects[activeObjectIndex], gridPosition, Quaternion.identity);
        }
    }

    public void deleteObject(){
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = camera.nearClipPlane;
        Ray ray = camera.ScreenPointToRay(mousePos);
        RaycastHit hit;

        //Shoot Raycast
        if (Physics.Raycast(ray, out hit, 100, placementLayer)){
            //deletes item if not the grid
            if(!hit.collider.name.Equals(ignoreDestroy.name)){
                mousePosition = hit.point;
                gridPosition = grid.GetCellCenterWorld(grid.WorldToCell(mousePosition));
                Debug.DrawLine(mousePosition, mousePosition + Vector3.up * 1, Color.white);
                Destroy(hit.collider.gameObject);
            }  
        }
    }

    public void changeObject(int index)
    {
        activeObjectIndex = index;
    }
}
