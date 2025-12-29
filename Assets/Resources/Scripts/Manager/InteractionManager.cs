using UnityEngine;
using System.Collections.Generic;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance;
    // Danh sách các bục mà Speaki đang đứng gần
    private List<VideoExhibit> nearbyExhibits = new List<VideoExhibit>();

    void Awake() => Instance = this;

    public void RegisterExhibit(VideoExhibit exhibit) => nearbyExhibits.Add(exhibit);
    public void UnregisterExhibit(VideoExhibit exhibit) => nearbyExhibits.Remove(exhibit);

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && nearbyExhibits.Count > 0)
        {
            GetClosestExhibit().PlayVideo();
        }
    }

    private VideoExhibit GetClosestExhibit()
    {
        VideoExhibit closest = null;
        float minDistance = Mathf.Infinity;
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        foreach (var exhibit in nearbyExhibits)
        {
            float dist = Vector3.Distance(playerPos, exhibit.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                closest = exhibit;
            }
        }
        return closest;
    }
}