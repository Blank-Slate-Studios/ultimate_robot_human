using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreateObstacle : MonoBehaviour, IPointerDownHandler
{
    //Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] Transform obstaclePosition;
    GameObject newObstacle;


    public void OnPointerDown(PointerEventData eventData)
    {
        //Vector3 worldPos;
        //Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit, 1000f))
        //{
        //    worldPos = hit.point;
        //}
        //else
        //{
        //    worldPos = Camera.main.ScreenToWorldPoint(mousePosition);
        //}
        newObstacle = Instantiate(obstaclePrefab, obstaclePosition.position, Quaternion.identity);
    }
}
