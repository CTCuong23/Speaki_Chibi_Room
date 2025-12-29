using UnityEngine;
using UnityEngine.Video;

public class VideoExhibit : MonoBehaviour
{
    [Header("Video Settings")]
    [SerializeField] private VideoClip videoClip;

    [Header("UI References")]
    [SerializeField] private GameObject videoPanel;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private GameObject interactionHint; // <--- THÊM DÒNG NÀY: Kéo cái chữ "Press E" vào đây

    void Start()
    {
        // Lúc đầu game thì ẩn chữ nhấn E đi cho chắc
        if (interactionHint != null) interactionHint.SetActive(false);
    }

    public void PlayVideo()
    {
        if (videoClip != null && videoPanel != null && videoPlayer != null)
        {
            videoPanel.SetActive(true);
            videoPlayer.clip = videoClip;

            // ĐĂNG KÝ SỰ KIỆN: Khi video chạy hết thì gọi hàm CloseVideo
            videoPlayer.loopPointReached += CloseVideo;

            videoPlayer.Play();
        }
    }

    // Hàm này sẽ tự động chạy khi video kết thúc
    private void CloseVideo(VideoPlayer vp)
    {
        // Hủy đăng ký để tránh bị gọi chồng chéo lần sau
        videoPlayer.loopPointReached -= CloseVideo;

        // Tắt video và bảng UI
        videoPlayer.Stop();
        videoPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InteractionManager.Instance.RegisterExhibit(this); // Đăng ký với Manager
            if (interactionHint != null) interactionHint.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InteractionManager.Instance.UnregisterExhibit(this); // Hủy đăng ký
            if (interactionHint != null) interactionHint.SetActive(false);
        }
    }
}