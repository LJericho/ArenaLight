using UnityEngine;

public class MouseController : MonoBehaviour
{
    public Camera cam;
    public UnitMovement unit;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 world = cam.ScreenToWorldPoint(Input.mousePosition);
            world.z = 0;
            unit.MoveTo(world);
        }
    }
}