using UnityEngine;
using System.Collections.Generic;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance;
    // Danh sách các bục mà Speaki đang đứng gần
    private List<VideoExhibit> nearbyExhibits = new List<VideoExhibit>();

    // THÊM DÒNG NÀY: Để nhớ bục nào đang chiếu phim
    private VideoExhibit currentPlayingExhibit;

    void Awake() => Instance = this;

    public void RegisterExhibit(VideoExhibit exhibit) => nearbyExhibits.Add(exhibit);
    public void UnregisterExhibit(VideoExhibit exhibit) => nearbyExhibits.Remove(exhibit);

    // THÊM HÀM NÀY: Để các bục báo cáo khi chúng bắt đầu phát
    public void SetCurrentExhibit(VideoExhibit exhibit)
    {
        currentPlayingExhibit = exhibit;
    }

    // THÊM HÀM NÀY: Cho nút X gọi vào
    public void CloseActiveVideo()
    {
        if (currentPlayingExhibit != null)
        {
            currentPlayingExhibit.ManualClose();
            currentPlayingExhibit = null; // Xóa dấu vết sau khi đóng
        }
    }

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