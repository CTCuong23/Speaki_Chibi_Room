using UnityEngine;
using UnityEngine.SceneManagement; // Thư viện bắt buộc để chuyển cảnh

public class MainMenuManager : MonoBehaviour
{
    // Hàm này sẽ chạy khi nhấn nút Play
    public void PlayGame()
    {
        TransitionManager.Instance.FadeAndLoadScene("SampleScene");
    }

    // Hàm này sẽ chạy khi nhấn nút Quit
    public void QuitGame()
    {
        Debug.Log("Người chơi đã nhấn thoát!"); // In ra console để mình biết nó chạy
        Application.Quit(); // Lệnh thoát ứng dụng (chỉ có tác dụng khi đã Build game)
    }
}