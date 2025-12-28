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

    private bool canInteract = false;

    void Start()
    {
        // Lúc đầu game thì ẩn chữ nhấn E đi cho chắc
        if (interactionHint != null) interactionHint.SetActive(false);
    }

    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            PlayVideo();
            // Khi đang xem video thì ẩn chữ nhấn E đi cho đỡ vướng
            if (interactionHint != null) interactionHint.SetActive(false);
        }

        
    }

    private void PlayVideo()
    {
        if (videoClip != null && videoPanel != null && videoPlayer != null)
        {
            videoPanel.SetActive(true);
            videoPlayer.clip = videoClip;
            videoPlayer.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
            if (interactionHint != null) interactionHint.SetActive(true); // <--- HIỆN CHỮ NHẤN E
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
            if (interactionHint != null) interactionHint.SetActive(false); // <--- ẨN CHỮ NHẤN E
        }
    }
}