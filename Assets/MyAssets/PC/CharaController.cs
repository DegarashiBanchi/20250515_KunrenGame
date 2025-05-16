using UnityEngine;
using UnityEngine.InputSystem;

public class CharaController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;                         // Rigidbody2Dコンポーネント
    [SerializeField] private float moveSpeed = 20.0f;                   // 移動速度
    [SerializeField] private float maxSpeed = 4.0f;                   // 最高速度
    [SerializeField] private bool isMoving = false;                    // 移動中フラグ
    [SerializeField] private Vector2 moveInput = Vector2.zero;         // 入力方向
    [SerializeField] public Vector2 addMoveForce = Vector2.zero;      // 外部からの移動力（未実装）
    [SerializeField] private Vector2 calcVerocity = Vector2.zero;      // 計算後の移動量
    [SerializeField] private float decelerationRate = 5.0f;            // 減速率

    private void Update()
    {
        // 入力方向とmoveSpeedから移動量を算出
        calcVerocity = moveInput.normalized * moveSpeed;

        // 入力がゼロであれば移動中フラグをfalse、それ以外はtrueにする
        isMoving = moveInput != Vector2.zero;

        // 入力から算出した移動力と外部からの加算力を合算
        Vector2 totalForce = calcVerocity + addMoveForce;

        if (isMoving)
        {
            // 入力がある場合は合算した力を適用
            rb.AddForce(totalForce, ForceMode2D.Force);
        }
        else
        {
            // 入力がゼロの場合は徐々に現在の速度を減速させる
            // Vector2.Lerp(現在の速度, 目標の速度(ゼロ), 減速率 * フレーム間時間) によりなめらかに減速
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, decelerationRate * Time.deltaTime);
        }

        // rbの速度が最高速度を超えないようにクランプする
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    // InputActionのコールバック。移動キー入力受付
    public void OnMove(InputAction.CallbackContext context)
    {
        // 入力ベクトルを取得し、moveInputに反映
        moveInput = context.ReadValue<Vector2>();
    }
}
