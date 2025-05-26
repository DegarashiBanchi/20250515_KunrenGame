// ワイプのPCスプライトを管理するスクリプト。

using R3;
using UnityEngine;

public class WiPESpriteManager : MonoBehaviour
{
    [SerializeField] SO_MaskStatus _SO_MaskStatus; // プレイヤーの被弾状態を管理するScriptableObject
    [SerializeField] SpriteRenderer _wipeSprite; // ワイプのスプライトレンダラー
    [Header("PCスプライト設定")]
    [SerializeField] Sprite _sprite_Nomal; // 通常スプライト
    [SerializeField] Sprite _sprite_Hit; // 被弾スプライト

    // PCスプライトの種別enum。
    public enum PCSpriteType
    {
        Normal,
        Hit,
        // 他のスプライトタイプを追加することができます。
    }
    private void Start()
    {
        // _SO_MaskStatus.isBeingHitを監視して、被弾状態したらスプライト変更。
        _SO_MaskStatus.isBeingHit.Subscribe(isHit =>
                    ChangeSprite(isHit ? PCSpriteType.Hit : PCSpriteType.Normal)
        );

    }

    // スプライト変更メソッド。
    public void ChangeSprite(PCSpriteType spriteType)
    {
        switch (spriteType)
        {
            case PCSpriteType.Normal:
                _wipeSprite.sprite = _sprite_Nomal;
                break;
            case PCSpriteType.Hit:
                _wipeSprite.sprite = _sprite_Hit;
                break;
        }
    }
}