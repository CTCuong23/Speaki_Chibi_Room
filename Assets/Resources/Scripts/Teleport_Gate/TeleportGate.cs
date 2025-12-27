using UnityEngine;

public class TeleportGate : MonoBehaviour
{
    [SerializeField] Transform destination; // Điểm đến (kéo cái cổng bên kia vào đây)

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra xem đối tượng va chạm có phải là Player (Speaki) không
        if (collision.CompareTag("Player"))
        {
            // "Bốc" Speaki thả sang vị trí của destination
            collision.transform.position = destination.position;
        }
    }
}