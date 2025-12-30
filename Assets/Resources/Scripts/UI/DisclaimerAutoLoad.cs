using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DisclaimerAutoLoad : MonoBehaviour
{
    [SerializeField] CanvasGroup faderGroup; // Kéo Panel chứa chữ vào đây
    [SerializeField] float waitTime = 5f;    // Thời gian đợi đọc chữ
    [SerializeField] string menuSceneName = "MainMenuScene"; // Tên scene menu

    private bool isSkipped = false; // Biến kiểm tra để tránh chạy chồng chéo

    IEnumerator Start()
    {
        if (faderGroup == null) yield break;

        // BƯỚC 1: Hiện chữ lên từ từ (Fade In)
        float t = 0;
        while (t < 1f)
        {
            if (isSkipped) yield break; // Thoát nếu đã nhấn skip
            t += Time.deltaTime;
            faderGroup.alpha = t;
            yield return null;
        }

        // BƯỚC 2: Đợi người chơi đọc
        float timer = 0;
        while (timer < waitTime)
        {
            if (isSkipped) yield break;
            timer += Time.deltaTime;
            yield return null;
        }

        // BƯỚC 3: Mờ dần chữ (Fade Out)
        t = 1;
        while (t > 0f)
        {
            if (isSkipped) yield break;
            t -= Time.deltaTime;
            faderGroup.alpha = t;
            yield return null;
        }

        EnterMainMenu();
    }

    void Update()
    {
        // CÁCH 1: Nhấn phím bất kỳ để skip (cực kỳ tiện cho người dùng)
        if (Input.anyKeyDown && !isSkipped)
        {
            SkipDisclaimer();
        }
    }

    // CÁCH 2: Dùng cho nút bấm (Button)
    public void SkipDisclaimer()
    {
        if (isSkipped) return;
        isSkipped = true;

        StopAllCoroutines(); // Dừng việc đếm thời gian
        EnterMainMenu();
    }

    private void EnterMainMenu()
    {
        // Chuyển sang Menu ngay lập tức
        SceneManager.LoadScene(menuSceneName);
    }
}