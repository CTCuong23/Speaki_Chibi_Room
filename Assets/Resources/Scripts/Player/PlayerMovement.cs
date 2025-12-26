using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Migration Settings")]
    [SerializeField] float moveSpeed = 5f; // Tốc độ chạy

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isFacingRight = true; // Biến kiểm tra xem đang quay mặt sang phải hay trái

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Giả sử ảnh gốc của Speaki đang quay mặt sang phải
        isFacingRight = true;
    }

    void Update()
    {
        // 1. Lấy dữ liệu từ phím WASD hoặc mũi tên
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        // 2. Xử lý lật hình dựa trên hướng di chuyển ngang (trục X)
        // Nếu đang bấm sang phải (x > 0) MÀ nhân vật đang quay trái --> Lật lại
        if (moveInput.x > 0 && !isFacingRight)
        {
            Flip();
        }
        // Nếu đang bấm sang trái (x < 0) MÀ nhân vật đang quay phải --> Lật lại
        else if (moveInput.x < 0 && isFacingRight)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        // 3. Thực hiện di chuyển vật lý
        // .normalized giúp đi chéo không bị nhanh hơn đi thẳng
        rb.MovePosition(rb.position + moveInput.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    // Hàm riêng để thực hiện việc lật
    void Flip()
    {
        // Đảo ngược trạng thái (đang phải thành trái, đang trái thành phải)
        isFacingRight = !isFacingRight;

        // Lấy scale hiện tại của nhân vật
        Vector3 currentScale = transform.localScale;
        // Nhân scale trục X với -1 để lật ngược lại
        currentScale.x *= -1;
        // Gán lại scale mới cho nhân vật
        transform.localScale = currentScale;
    }
}