using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject pausePanel; // Kéo cái Panel vừa tạo vào đây

    [Header("Scene Names")]
    [SerializeField] private string mainMenuSceneName = "MainMenuScene"; // Tên scene menu của bạn

    [Header("Audio Settings")]
    [SerializeField] private AudioClip openSound;  // Tiếng khi mở (Esc lần 1)
    [SerializeField] private AudioClip closeSound; // Tiếng khi đóng (Esc lần 2)
    private AudioSource audioSource;

    // Biến kiểm tra trạng thái game
    public static bool isPaused = false;

    void Start()
    {
        // Đảm bảo khi bắt đầu game thì panel ẩn và game chạy bình thường
        if (pausePanel != null) pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        // Tự động lấy hoặc thêm AudioSource nếu chưa có
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Bắt sự kiện nhấn phím ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        if (pausePanel != null) pausePanel.SetActive(true);

        // Đóng băng thời gian trong game
        Time.timeScale = 0f;
        isPaused = true;

        // --- PHÁT TIẾNG MỞ ---
        if (openSound != null && audioSource != null)
        {
            // Dùng PlayOneShot để phát đè lên các âm thanh khác nếu cần
            audioSource.PlayOneShot(openSound);
        }
    }

    public void ResumeGame()
    {
        if (pausePanel != null) pausePanel.SetActive(false);

        // Trả lại thời gian bình thường
        Time.timeScale = 1f;
        isPaused = false;

        // --- PHÁT TIẾNG ĐÓNG ---
        if (closeSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(closeSound);
        }
    }

    public void RestartLevel()
    {
        // QUAN TRỌNG: Phải trả timeScale về 1 trước khi load scene mới
        Time.timeScale = 1f;
        isPaused = false;

        // Load lại scene hiện tại
        // Dùng TransitionManager nếu bạn muốn có hiệu ứng mờ dần như file TeleportGate.cs
        if (TransitionManager.Instance != null)
        {
            TransitionManager.Instance.FadeAndLoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;

        if (TransitionManager.Instance != null)
        {
            TransitionManager.Instance.FadeAndLoadScene(mainMenuSceneName);
        }
        else
        {
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}