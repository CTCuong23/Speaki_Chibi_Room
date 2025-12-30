using UnityEngine;
using UnityEngine.EventSystems; // Thư viện để bắt sự kiện chuột

public class UIButtonEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 originalScale;
    [SerializeField] float hoverScale = 1.2f; // Tương đương transform: scale(1.2)
    [SerializeField] float downScale = 0.9f;  // Khi nhấn xuống
    [SerializeField] float speed = 10f;       // Tốc độ transition

    // --- PHẦN THÊM MỚI ---
    [Header("Audio Settings")]
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip clickSound;
    private AudioSource audioSource;

    private Vector3 targetScale;

    void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;

        // Tự động lấy component AudioSource gắn trên nút
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Cực kỳ giống transition trong CSS: mượt mà chuyển đổi kích thước
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * speed);
    }

    // Tương đương :hover
    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = originalScale * hoverScale;

        // Phát tiếng hover (rê chuột vào)
        if (hoverSound != null && audioSource != null)
            audioSource.PlayOneShot(hoverSound);
    }

    // Tương đương trạng thái bình thường
    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;
    }

    // Tương đương :active
    public void OnPointerDown(PointerEventData eventData)
    {
        targetScale = originalScale * downScale;

        // Phát tiếng click (khi nhấn xuống)
        if (clickSound != null && audioSource != null)
            audioSource.PlayOneShot(clickSound);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        targetScale = originalScale * hoverScale;
    }
}