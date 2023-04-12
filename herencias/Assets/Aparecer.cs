using System.Collections.Generic;
using UnityEngine;

public class Aparecer : MonoBehaviour
{
    public float fallHeight = -20f;

    private GameObject[] teleportPoints;

    private void Awake()
    {
        teleportPoints = GameObject.FindGameObjectsWithTag("Teleport");
    }

    private void Update()
    {
        if (transform.position.y < fallHeight)
        {
            TeleportToClosestPoint();
        }
    }

    private void TeleportToClosestPoint()
    {
        GameObject closestPoint = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject point in teleportPoints)
        {
            float distanceToPoint = Vector3.Distance(point.transform.position, currentPosition);

            if (distanceToPoint < closestDistance)
            {
                closestDistance = distanceToPoint;
                closestPoint = point;
            }
        }

        transform.position = new Vector3(closestPoint.transform.position.x, closestPoint.transform.position.y, transform.position.z);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, fallHeight, transform.position.z), 1f);
    }

    public void AddTeleportPoint(GameObject point)
    {
        List<GameObject> tempList = new List<GameObject>(teleportPoints);
        tempList.Add(point);
        teleportPoints = tempList.ToArray();
    }

    public void RemoveTeleportPoint(GameObject point)
    {
        List<GameObject> tempList = new List<GameObject>(teleportPoints);
        tempList.Remove(point);
        teleportPoints = tempList.ToArray();
    }
}
