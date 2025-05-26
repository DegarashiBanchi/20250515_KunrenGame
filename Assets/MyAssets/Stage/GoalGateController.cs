// ゴールゲートの制御を行うクラス。

using R3;
using UnityEngine;

public class GoalGateController : MonoBehaviour
{
    [SerializeField] SO_MaskStatus _maskStatus; // マスクの状態を管理するScriptableObjectの参照
    PCmanager _PCmanager; // PCマネージャーの参照
    [SerializeField] SpriteRenderer _gateSpriteRenderer; // ゴールゲートのスプライトレンダラー

    private void Start()
    {
        // isGoalを購読して、ゴール状態が変化したときに処理を行う。
        _maskStatus.canGoal.Subscribe(canGoal =>
        {
            if (canGoal)
            {
                // ゴール状態が有効になったときの処理
                Debug.Log("ゴール有効");
                // ゴールゲートのスプライトを有効にする
                SetGateSprite(true);
            }
            else
            {
                // ゴール状態が解除されたときの処理
                Debug.Log("ゴール解除");
                // ゴールゲートのスプライトを無効にする
                SetGateSprite(false);
            }
        }).AddTo(this);
    }

    /// <summary>
    /// ゲートスプライトの有効・無効を、透明度の変更で表現する。
    /// </summary>
    /// <param name="isActive">trueなら透明度を0.8、falseなら透明度を0.3に設定</param>
    public void SetGateSprite(bool isActive)
    {
        // 現在のカラーを取得し、アルファ値(透明度)を更新する
        Color color = _gateSpriteRenderer.color;
        color.a = isActive ? 0.8f : 0.3f;
        _gateSpriteRenderer.color = color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // PCマネージャーの取得
        _PCmanager = collision.GetComponent<PCmanager>();

        // トリガーに入ったオブジェクトがPlayerタグを持っていて、かつ_maskStatus.canGoalがtrueの場合
        if (collision.CompareTag("Player") && _maskStatus.canGoal.Value)
        {
            // ゴール処理。
            Debug.Log("ゴールしました");
        }
    }
}