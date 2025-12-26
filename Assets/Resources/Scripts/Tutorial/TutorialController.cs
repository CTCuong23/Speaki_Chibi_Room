using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] GameObject tutorialPanel; // Kéo TutorialPanel vào đây
    private bool isVisible = true;   // Trạng thái hiển thị

    void Update()
    {
        // Kiểm tra nếu người chơi nhấn phím H (viết tắt của Help)
        if (Input.GetKeyDown(KeyCode.H))
        {
            ToggleTutorial();
        }
    }

    void ToggleTutorial()
    {
        isVisible = !isVisible; // Đảo ngược trạng thái
        tutorialPanel.SetActive(isVisible); // Ẩn hoặc hiện bảng
    }
}