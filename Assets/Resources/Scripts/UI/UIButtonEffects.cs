using UnityEngine;
using UnityEngine.EventSystems; // Thư viện để bắt sự kiện chuột

public class UIButtonEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 originalScale;
    public float hoverScale = 1.2f; // Tương đương transform: scale(1.2)
    public float downScale = 0.9f;  // Khi nhấn xuống
    public float speed = 10f;       // Tốc độ transition

    private Vector3 targetScale;

    void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;
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
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        targetScale = originalScale * hoverScale;
    }
}