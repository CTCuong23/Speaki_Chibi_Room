using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] GameObject hintText;
    [SerializeField] AudioClip interactSound; // Kéo file "Cuayo" vào đây
    private AudioSource audioSource;
    private bool canInteract = false;

    [Header("Cài đặt hình ảnh")]
    [SerializeField] GameObject emotePrefab; // Kéo Prefab trái tim vào đây
    [SerializeField] Transform spawnPoint;   // Điểm xuất hiện (thường là trên đầu Speaki)
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

        if (emotePrefab != null)
        {
            // Lấy vị trí trên đầu bé Speaki (thông qua tag Player)
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector3 spawnPos = player.transform.position + new Vector3(0, 1.5f, 0); // Cao hơn đầu 1.5 đơn vị

            // Tạo ra trái tim
            Instantiate(emotePrefab, spawnPos, Quaternion.identity);
        }

        Debug.Log("Cuayo with Love!");
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