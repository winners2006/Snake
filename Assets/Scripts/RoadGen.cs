using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoadGen : MonoBehaviour
{
    public GameObject RoadPrefab;
    private List<GameObject> roads = new List<GameObject>();
    public float maxSpeed = 10;
    public float speed = 0;
    public int maxRoadCount = 5;

    void Start()
    {
        ReselRoad();
        //StartRoad();
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (speed == 0) return;

        foreach (GameObject road in roads)
        {
            road.transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
        }

        if (roads [0].transform.position.z < -15)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);

            CreatNextRoad();
        }
    }

    private void CreatNextRoad()
    {
        Vector3 pos = Vector3.zero;
        if(roads.Count > 0)
        {
            pos = roads[roads.Count - 1].transform.position + new Vector3(0, 0, 10);
        }
        GameObject go = Instantiate(RoadPrefab, pos, Quaternion.identity);
        go.transform.SetParent(transform);
        roads.Add(go);
    }

    public void StartRoad()
    {
        speed = maxSpeed;
        SwipeManager.instance.enabled = true;
    }

    public void ReselRoad()
    {
        speed = 0;
        while (roads.Count > 0)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
        }
        for(int i = 0; i<maxRoadCount; i++)
        {
            CreatNextRoad();
        }
        SwipeManager.instance.enabled = false;
    }
}
