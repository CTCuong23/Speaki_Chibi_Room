using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] GameObject hintText;
    [SerializeField] AudioClip interactSound; // Kéo file "Cuayo" vào đây
    private AudioSource audioSource;
    private bool canInteract = false;

    void Start()
    {
        hintText.SetActive(false);
        // Tự động lấy component Audio Source trên cùng object này
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            DoAction();
        }
    }

    void DoAction()
    {
        // Phát tiếng Cuayo một lần duy nhất
        if (interactSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(interactSound);
        }

        Debug.Log("Speaki says: Cuayo!");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
            hintText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
            hintText.SetActive(false);
        }
    }
}