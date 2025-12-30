using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    // Tạo Singleton để dễ gọi từ các cổng dịch chuyển
    public static TransitionManager Instance;

    [Header("Cài đặt UI")]
    [SerializeField] CanvasGroup fadePanelCanvasGroup; // Kéo cái FadePanel có Canvas Group vào đây
    [Header("Thời gian chuyển cảnh")]
    [SerializeField] float fadeDuration = 1f; // Thời gian để đen dần/sáng dần (1 giây)
    [SerializeField] float waitOnBlack = 0.5f; // Thời gian chờ khi màn hình tối thui

    private void Awake()
    {
        // Thiết lập Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Giữ lại khi chuyển Scene thật (nếu sau này dùng)
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Hàm này sẽ được cái cổng gọi
    public void FadeAndTeleport(Transform player, Transform destination)
    {
        // Bắt đầu quy trình chuyển cảnh (Coroutine)
        StartCoroutine(TransitionSequence(player, destination));
    }

    // Quy trình diễn ra theo thời gian
    IEnumerator TransitionSequence(Transform player, Transform destination)
    {
        // 1. Fade Out (Sáng -> Tối dần)
        yield return StartCoroutine(Fade(1f));

        // 2. Khi đã tối thui, thực hiện dịch chuyển
        player.position = destination.position;

        // (Tùy chọn) Chờ một chút cho "nguy hiểm"
        yield return new WaitForSeconds(waitOnBlack);

        // 3. Fade In (Tối -> Sáng dần)
        yield return StartCoroutine(Fade(0f));
    }

    // Hàm xử lý việc thay đổi Alpha
    IEnumerator Fade(float targetAlpha)
    {
        // Kiểm tra xem cái Fader có còn tồn tại không trước khi chạy
        if (fadePanelCanvasGroup == null) yield break;

        float startAlpha = fadePanelCanvasGroup.alpha;
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            // Thay đổi giá trị alpha từ từ theo thời gian
            fadePanelCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            yield return null; // Chờ đến khung hình tiếp theo
        }
        // Đảm bảo kết thúc ở đúng giá trị mục tiêu
        fadePanelCanvasGroup.alpha = targetAlpha;
    }

    // Hàm mới để chuyển sang Scene khác
    public void FadeAndLoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneSequence(sceneName));
    }

    IEnumerator LoadSceneSequence(string sceneName)
    {
        // 1. Đen màn hình
        yield return StartCoroutine(Fade(1f));

        // 2. Nạp Scene mới bằng tên
        SceneManager.LoadScene(sceneName);

        // Lưu ý: Vì Manager có DontDestroyOnLoad, nó sẽ đi theo sang Scene mới.
        // Khi Scene mới nạp xong, chúng ta làm nó sáng lại.
        yield return StartCoroutine(Fade(0f));
    }
}