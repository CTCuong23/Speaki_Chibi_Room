using UnityEngine;
using System.Collections; // Cần để dùng Coroutine

public class WelcomeManager : MonoBehaviour
{
    [SerializeField] GameObject welcomePanel;
    [SerializeField] float displayDuration = 3f; // Số giây hiển thị (ví dụ 3 giây)

    void Start()
    {
        if (welcomePanel != null)
        {
            // Bắt đầu quy trình tự động ẩn
            StartCoroutine(AutoHidePanel());
        }
    }

    IEnumerator AutoHidePanel()
    {
        // 1. Hiện bảng lên
        welcomePanel.SetActive(true);

        // 2. Chờ trong khoảng thời gian đã định
        yield return new WaitForSeconds(displayDuration);

        // 3. Ẩn bảng đi
        welcomePanel.SetActive(false);
    }
}