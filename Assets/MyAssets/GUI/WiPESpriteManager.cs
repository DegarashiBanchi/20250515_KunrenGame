// ワイプのPCスプライトを管理するスクリプト。

using R3;
using UnityEngine;

public class WiPESpriteManager : MonoBehaviour
{
    [SerializeField] SO_MaskStatus _SO_MaskStatus; // プレイヤーの被弾状態を管理するScriptableObject
    [SerializeField] SpriteRenderer _wipeSprite; // ワイプのスプライトレンダラー
    [SerializeField] Animator _wipeAnimator; // ワイプのアニメーター
    private void Start()
    {
        //_SO_MaskStatus._isGameOverを購読し、変化があった場合、_wipeAnimator.SetBoolの"IsGameOver"と連動させる
        _SO_MaskStatus._isGameOver.Subscribe(isGameOver =>
        {
            _wipeAnimator.SetBool("isGameOver", isGameOver);
        }).AddTo(this);

        // 同様に、_SO_MaskStatus._isBeingHitを購読し、変化があった場合、_wipeAnimator.SetBoolの"IsHit"と連動させる
        _SO_MaskStatus.isBeingHit.Subscribe(isBeingHit =>
        {
            _wipeAnimator.SetBool("isHit", isBeingHit);
        }).AddTo(this);
    }
}