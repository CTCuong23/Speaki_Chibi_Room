using UnityEngine;

public class TeleportGate : MonoBehaviour
{
    [SerializeField] Transform destination; // Điểm đến (vẫn như cũ)

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // CŨ: Dịch chuyển ngay lập tức gây nhức đầu
            // collision.transform.position = destination.position;

            // MỚI: Gọi TransitionManager để làm hiệu ứng mượt mà
            // Chúng ta truyền vào: đối tượng cần dịch chuyển (Speaki) và điểm đến
            TransitionManager.Instance.FadeAndTeleport(collision.transform, destination);
        }
    }
}