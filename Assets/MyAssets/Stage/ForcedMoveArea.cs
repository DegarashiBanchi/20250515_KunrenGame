// ステージ内に存在する強制移動エリア。

using UnityEngine;

public class ForcedMoveArea : MonoBehaviour
{
    // 強制移動の方向
    [SerializeField] private Vector2 moveDirection = Vector2.down;

    // 強制移動の速度
    [SerializeField] private float moveSpeed = 5f;

    // OnTriggerStay2Dが呼ばれたときに、Rigidbody2Dに強制移動を適用する
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("はいった");
        // Rigidbody2Dコンポーネントを取得
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // addForceで強制移動を適用
            rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode2D.Force);
        }
    }

}