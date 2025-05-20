// PCの被弾時処理を担当するスクリプト。

using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

public class HandlePlayerDamage : MonoBehaviour
{
    [SerializeField] SO_OpenStatus _openStatus; // プレイヤーのHPを管理するScriptableObject
    [SerializeField] SpriteRenderer _PCSprite; // プレイヤーのスプライトレンダラー


    // 被弾時に呼び出されるメソッド
    public async UniTask OnPlayerHit(int damage)
    {
        Debug.Log($"Player hit! Damage: {damage}");
        // damageの値だけHPを減少させる。

        // PCを吹き飛ばす。


        // 被弾時のアニメーション処理。
        await DamageMotion();

        //
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