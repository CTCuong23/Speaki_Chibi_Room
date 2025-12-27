using UnityEngine;

public class TeleportGate : MonoBehaviour
{
    [Header("Dịch chuyển nội bộ (Cùng Scene)")]
    [SerializeField] Transform destination; // Điểm đến (vẫn như cũ)

    [Header("Dịch chuyển sang Scene khác")]
    [SerializeField] string sceneToLoad; // Gõ tên Scene vào đây (VD: MainScene)

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Nếu có điền tên Scene -> Chuyển Scene
            if (!string.IsNullOrEmpty(sceneToLoad))
            {
                TransitionManager.Instance.FadeAndLoadScene(sceneToLoad);
            }
            // Nếu không có tên Scene nhưng có điểm đến -> Dịch chuyển tọa độ
            else if (destination != null)
            {
                TransitionManager.Instance.FadeAndTeleport(collision.transform, destination);
            }
        }
    }
}