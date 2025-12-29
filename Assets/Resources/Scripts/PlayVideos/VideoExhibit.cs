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

    // Biến để quản lý nhạc nền
    private AudioSource bgmSource;

    void Start()
    {
        // Lúc đầu game thì ẩn chữ nhấn E đi cho chắc
        if (interactionHint != null) interactionHint.SetActive(false);

        // Tìm AudioManager trong Scene và lấy AudioSource của nó
        GameObject am = GameObject.Find("AudioManager");
        if (am != null) bgmSource = am.GetComponent<AudioSource>();
    }

    public void PlayVideo()
    {
        if (videoClip != null && videoPanel != null && videoPlayer != null)
        {
            // BÁO CÁO: "Tôi đang phát nè"
            InteractionManager.Instance.SetCurrentExhibit(this);

            // 1. Tạm dừng nhạc nền
            if (bgmSource != null) bgmSource.Pause();

            videoPanel.SetActive(true);
            videoPlayer.clip = videoClip;

            // ĐĂNG KÝ SỰ KIỆN: Khi video chạy hết thì gọi hàm CloseVideo
            videoPlayer.loopPointReached += CloseVideo;

            videoPlayer.Play();
        }
    }

    // Hàm này dùng cho cả sự kiện tự động hết phim VÀ nút đóng thủ công
    public void ManualClose()
    {
        // Gọi hàm CloseVideo với tham số null vì ta không cần dùng đến VideoPlayer ở đây
        CloseVideo(null);
    }

    // Hàm này sẽ tự động chạy khi video kết thúc
    private void CloseVideo(VideoPlayer vp)
    {
        // Hủy đăng ký để tránh bị gọi chồng chéo lần sau
        videoPlayer.loopPointReached -= CloseVideo;

        // Tắt video và bảng UI
        videoPlayer.Stop();
        videoPanel.SetActive(false);

        // 2. Tiếp tục phát nhạc nền từ đoạn đang dang dở
        if (bgmSource != null) bgmSource.UnPause();

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