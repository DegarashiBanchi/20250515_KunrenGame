using R3;
using R3.Triggers;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PCFollower : MonoBehaviour
{
    private Transform followTransform;
    PCmanager _PCmanager; // PCのマネージャの参照

    [Header("追従設定")]
    [SerializeField] private float smoothSpeed = 4.5f;
    [SerializeField] private float distanceFromFollower = 1f;

    [SerializeField] private bool isFollowing = false;
    private const string PlayerTag = "Player";
    [SerializeField] Rigidbody2D rb; // Rigidbody2Dの参照
    [SerializeField] SO_MaskStatus _maskStatus; // マスクの状態を管理するScriptableObjectの参照

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2Dの取得
    }

    // トリガーに入ったときの処理
    private void OnTriggerEnter2D(Collider2D other)
    {
        // トリガーに入ったオブジェクトがPlayerタグを持っている場合
        if (other.CompareTag(PlayerTag) && !isFollowing)
        {
            isFollowing = true; // 追従を開始
            rb.gravityScale = 0; // 重力を無効化
            _PCmanager = other.GetComponent<PCmanager>(); // PCmanagerの取得

            // リストを取得し、nullか要素数が0の場合は、otherを追従対象にする。
            if (_PCmanager._followers.Count == 0 || _PCmanager._followers == null)
            {
                followTransform = other.transform;
            }
            else
            {
                // _PCmanagerの持つPCFollowerリストから、自分の直前のオブジェクトを取得し、追従対象にする。
                followTransform = _PCmanager._followers[_PCmanager._followers.Count - 1].transform;
            }

            // _PCmanager._followersに自分を追加する
            _PCmanager.AddFollower(this.gameObject);

            // 自身が「Key」タグを持っている場合、_maskStatusのcanGoalをtrueにする
            if (gameObject.CompareTag("Key"))
            {
                _maskStatus.canGoal.Value = true;
                Debug.Log("canGoalをtrueにしました");
            }
        }
    }

    private void LateUpdate()
    {
        // 追従している場合
        if (isFollowing)
        {
            // 追従対象と自分の位置を取得
            Vector2 followerPos = followTransform.position;
            Vector2 selfPos = transform.position;

            // 追従対象への方向ベクトルを計算して正規化。
            Vector2 direction = (followerPos - selfPos).normalized;
            //Debug.Log($"{direction}");

            // 対象からdistanceFromFollowerだけ離れた位置を計算
            Vector2 targetPosition = followerPos - direction * distanceFromFollower;

            // 自分の位置をスムーズに移動
            transform.position = Vector2.Lerp(selfPos, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }

    // ダメージを受けたときの処理
    public void OnDamage()
    {
        // 追従を停止
        isFollowing = false;
        // 自分をPCmanagerのリストから削除する
        _PCmanager._followers.Remove(this.gameObject);

        // 重力を0.2fに設定
        if (rb != null)
        {
            rb.gravityScale = 0.2f;
        }

        // 自身をランダムな方向に飛ばす
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomForce = Random.Range(1f, 3f);
        if (rb != null)
        {
            rb.AddForce(randomDirection * randomForce, ForceMode2D.Impulse);
        }

        // 自身が「Key」タグを持っている場合、_maskStatusのcanGoalをfalseにする
        if (gameObject.CompareTag("Key"))
        {
            _maskStatus.canGoal.Value = false;
            Debug.Log("canGoalをfalseにしました");
        }
    }


}