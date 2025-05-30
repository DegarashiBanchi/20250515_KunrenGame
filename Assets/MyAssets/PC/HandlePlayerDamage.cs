// PCの被弾時処理を担当するスクリプト。

using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

public class HandlePlayerDamage : MonoBehaviour
{
    [SerializeField] SO_OpenStatus _openStatus; // プレイヤーのHPを管理するScriptableObject
    [SerializeField] SO_MaskStatus _maskStatus; // プレイヤーの被弾状態を管理するScriptableObject
    [SerializeField] SpriteRenderer _PCSprite; // プレイヤーのスプライトレンダラー

    // 被弾時に呼び出されるメソッド
    public async UniTask OnPlayerHit(int damage)
    {
        // 無敵状態をtrueにする。
        _maskStatus.isBeingHit.Value = true;

        Debug.Log($"Player hit! Damage: {damage}");
        // damageの値だけHPを減少させる。
        _openStatus._currentHP.Value -= damage;

        // HPが0以下になったらゲームオーバー処理を呼び出す
        if (_openStatus._currentHP.Value <= 0)
        {
            // ゲームオーバーフラグを立てる。
            _maskStatus._isGameOver.Value = true;

            return;
        }

        // 被弾時のアニメーション処理。
        await DamageMotion();

        // 無敵状態をfalseに。
        _maskStatus.isBeingHit.Value = false;
    }

    private async UniTask DamageMotion()
    {
        // shakeで震える。
        var handle_Xmove = LMotion.Shake.Create(0f, 0.5f, 0.5f)
            .WithFrequency(10)
            .WithDampingRatio(1)
            .BindToLocalPositionX(_PCSprite.transform);

        var handle_Ymove = LMotion.Shake.Create(0f, 0.5f, 0.5f)
            .WithFrequency(10)
            .WithDampingRatio(1)
            .BindToLocalPositionY(_PCSprite.transform);

        // 点滅させる。
        var handle_damage = LMotion.Create(0f, 1f, 0.2f)
            .WithLoops(11, LoopType.Flip)
            .BindToColorA(_PCSprite);

        await handle_damage;
    }
}