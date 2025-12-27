using UnityEngine;

public class SlowRotator : MonoBehaviour
{
    [Header("Tốc độ xoay")]
    [Tooltip("Số càng lớn xoay càng nhanh. Số âm sẽ xoay ngược chiều.")]
    [SerializeField] float rotationSpeed = 30f; // Tốc độ mặc định là 30

    void Update()
    {
        // Thực hiện xoay quanh trục Z (trục hướng ra khỏi màn hình)
        // Time.deltaTime giúp việc xoay mượt mà trên mọi loại máy
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}