// ゴールゲートの制御を行うクラス。

using R3;
using UnityEngine;

public class GoalGateController : MonoBehaviour
{
    [SerializeField] SO_MaskStatus _maskStatus; // マスクの状態を管理するScriptableObjectの参照
    [SerializeField] PCmanager _PCmanager; // PCマネージャーの参照

    private void Start()
    {
        // isGoalを購読して、ゴール状態が変化したときに処理を行う。
        _maskStatus.canGoal.Subscribe(canGoal =>
        {
            if (canGoal)
            {
                // ゴール状態が有効になったときの処理
                Debug.Log("ゴール有効");
            }
            else
            {
                // ゴール状態が解除されたときの処理
                Debug.Log("ゴール解除");
            }
        }).AddTo(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // トリガーに入ったオブジェクトがPlayerタグを持っていて、かつ_maskStatus.canGoalがtrueの場合
        if (collision.CompareTag("Player") && _maskStatus.canGoal.Value)
        {
            // ゴール処理。
            Debug.Log("ゴールしました");
        }
    }
}