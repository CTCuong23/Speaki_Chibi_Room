using UnityEngine;

public class FloatingEffect : MonoBehaviour
{
    [Header("Cài đặt trôi nổi")]
    [SerializeField] float amplitude = 0.2f; // Độ cao trôi nổi (bay lên/xuống bao nhiêu)
    [SerializeField] float frequency = 2f;   // Tốc độ trôi nổi (nhanh hay chậm)

    private Vector3 startPos;

    void Start()
    {
        // Lưu lại vị trí gốc để không bị trôi mất xác
        startPos = transform.localPosition;
    }

    void Update()
    {
        // Tính toán vị trí Y mới dựa trên hàm Sin
        float newY = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;

        // Cập nhật vị trí cho tấm ảnh
        transform.localPosition = new Vector3(startPos.x, newY, startPos.z);
    }
}