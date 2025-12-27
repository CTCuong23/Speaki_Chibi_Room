using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float lifetime = 1f; // Sống 1 giây
    [SerializeField] float moveSpeed = 1f; // Tốc độ bay lên

    void Start()
    {
        Destroy(gameObject, lifetime); // Tự hủy sau 1s
    }

    void Update()
    {
        // Làm cho trái tim bay lên từ từ
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }
}