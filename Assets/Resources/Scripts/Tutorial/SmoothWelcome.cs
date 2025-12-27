using UnityEngine;
using System.Collections;

public class SmoothWelcome : MonoBehaviour
{
    [Header("Cài đặt UI")]
    [SerializeField] CanvasGroup panelCanvasGroup; // Kéo WelcomePanel vào đây

    [Header("Cài đặt thời gian")]
    [SerializeField] float fadeDuration = 1.5f; // Thời gian mờ dần
    [SerializeField] float displayDuration = 3f; // Thời gian hiện bảng

    void Start()
    {
        // Bắt đầu chuỗi hiệu ứng: Hiện lên -> Chờ -> Biến mất
        StartCoroutine(WelcomeSequence());
    }

    IEnumerator WelcomeSequence()
    {
        // 1. Hiện lên (Fade In)
        yield return StartCoroutine(Fade(1f));

        // 2. Chờ một chút để người chơi đọc
        yield return new WaitForSeconds(displayDuration);

        // 3. Biến mất (Fade Out)
        yield return StartCoroutine(Fade(0f));

        // Sau khi biến mất hoàn toàn thì tắt hẳn Object cho nhẹ máy
        panelCanvasGroup.gameObject.SetActive(false);
    }

    // Hàm toán học Lerp để làm mượt độ mờ (giống TransitionManager)
    IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = panelCanvasGroup.alpha;
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            panelCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            yield return null;
        }
        panelCanvasGroup.alpha = targetAlpha;
    }
}