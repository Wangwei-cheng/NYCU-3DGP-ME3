using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateModel : MonoBehaviour
{
    public Transform ModelTransform;

    private bool isRotate;
    private Vector3 startPoint;
    private Vector3 startAngel;
    public float rotateScale = 1.0f;

    public GameObject area;

    private Vector2 min;
    private Vector2 max;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !isRotate && inArea(Input.mousePosition))
        {
            isRotate = true;
            startPoint = Input.mousePosition;
            startAngel = ModelTransform.eulerAngles;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isRotate = false;
        }

        if (isRotate)
        {
            var curPoint = Input.mousePosition;
            var x = startPoint.x - curPoint.x;

            ModelTransform.eulerAngles = startAngel + new Vector3(0, x * rotateScale, 0);
        }
    }

    bool inArea(Vector2 pos)
    {
        GetMinMax();
        
        if(pos.x > min.x && pos.x < max.x && 
           pos.y > min.y && pos.y < max.y)
        {
            return true;
        }

        return false;
    }

    void GetMinMax()
    {
        Vector3[] worldCorners = new Vector3[4];
        RectTransform rectTransform = area.GetComponent<RectTransform>();
        rectTransform.GetWorldCorners(worldCorners);
        min.x = worldCorners[0].x;
        min.y = worldCorners[0].y;
        max.x = worldCorners[2].x;
        max.y = worldCorners[2].y;
    }
}
