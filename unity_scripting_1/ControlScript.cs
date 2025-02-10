using System.Collections.Generic;
using UnityEngine;

public class ControlScript : MonoBehaviour
{
    [Header("Public Variables")]
    public Vector3 mousePosition;
    public Vector3 gridPosition;
    public new Camera camera;
    public LayerMask placementLayer;
    public Grid grid;

    public List<GameObject> Objects = new List<GameObject>();
    public int activeObjectIndex;
    public Input forward;
    public Input back;
    public Input left;
    public Input right;

    private void HandlePublicVariables()
    {
        //Calculate Ray
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
        }
    }

    private void Update()
    {
        HandlePublicVariables();
        if (Input.GetMouseButtonDown(0))
        {
            placeObject();
        }
    }

    public void placeObject()
    {
        Instantiate(Objects[activeObjectIndex], gridPosition, Quaternion.identity);
    }

    public void changeObject(int index)
    {
        activeObjectIndex = index;
    }
}
