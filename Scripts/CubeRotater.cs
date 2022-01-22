using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CubeRotater : MonoBehaviour, IDragHandler
{
    public float rotate_speed = 10;
    public void OnDrag(PointerEventData eventData)
    {
        float x = eventData.delta.x * Time.deltaTime * rotate_speed;
        float y = eventData.delta.y * Time.deltaTime * rotate_speed;

        transform.Rotate(y, -x, 0, Space.World);
    }
}
