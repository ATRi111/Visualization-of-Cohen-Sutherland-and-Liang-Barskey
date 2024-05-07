using Tools;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableVertex : MonoBehaviour, IDragHandler
{
    [HideInInspector]
    public Rect range;

    public void OnDrag(PointerEventData eventData)
    {
        float z = transform.position.z;
        Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        temp.ResetZ(z);
        if(range.Contains(temp))
            transform.position = new Vector3(temp.x, temp.y, z);
    }
}
