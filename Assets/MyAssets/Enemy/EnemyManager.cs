// PCにダメージを与えるスクリプト。

using Cysharp.Threading.Tasks;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] int enemyDamage = 1; // 敵のダメージ量



    // エネミーの攻撃アニメ。
    private void AttackMotion()
    {
        // 攻撃アニメーションの処理をここに追加
        Debug.Log("アタック！");
    }

    // コライダーにヒット
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // "Player"タグを持つオブジェクトに衝突した場合
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Playerに衝突！");

            // HandlePlayerDamageスクリプトを取得
            HandlePlayerDamage playerDamage = collision.gameObject.GetComponent<HandlePlayerDamage>();

            // HandlePlayerDamageスクリプトが存在する場合
            if (playerDamage != null)
            {
                // 被弾時のアニメ処理。
                AttackMotion();

                // プレイヤーを押し返す
                Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (playerRb != null)
                {
                    Vector2 pushDirection = (collision.transform.position - transform.position).normalized;
                    playerRb.AddForce(pushDirection * 2f, ForceMode2D.Impulse);
                }


                // ダメージを与える
                playerDamage.OnPlayerHit(enemyDamage).Forget();
            }
        }

        
    }
}
