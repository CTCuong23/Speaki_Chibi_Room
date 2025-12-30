using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DisclaimerAutoLoad : MonoBehaviour
{
    [SerializeField] CanvasGroup faderGroup; // Kéo Panel đen vào đây
    [SerializeField] float waitTime = 3f;

    IEnumerator Start()
    {
        if (faderGroup == null) yield break;
        faderGroup.alpha = 0; // Lúc đầu màn hình trong suốt để thấy chữ CTKUN
        yield return new WaitForSeconds(waitTime);

        // Làm màn hình đen lại (Fade Out)
        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime;
            faderGroup.alpha = t;
            yield return null;
        }

        // Chuyển sang Menu
        SceneManager.LoadScene("MainMenuScene");
    }
}