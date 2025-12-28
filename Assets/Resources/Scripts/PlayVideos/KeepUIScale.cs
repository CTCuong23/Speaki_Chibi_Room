using UnityEngine;

public class KeepUIScale : MonoBehaviour
{
    private Vector3 initialScale;

    void Start()
    {
        // Lưu lại kích thước ban đầu của Canvas (ví dụ 0.01, 0.01, 0.01)
        initialScale = transform.localScale;
    }

    // Dùng LateUpdate để chạy sau khi Player đã lật hình
    void LateUpdate()
    {
        if (transform.parent != null)
        {
            // Nếu cha (Speaki) bị lật (Scale.x < 0), thì con cũng tự lật ngược lại 
            // để triệt tiêu hiệu ứng gương, giúp chữ luôn xuôi chiều.
            float parentDirection = Mathf.Sign(transform.parent.localScale.x);

            transform.localScale = new Vector3(
                parentDirection * initialScale.x,
                initialScale.y,
                initialScale.z
            );
        }
    }
}