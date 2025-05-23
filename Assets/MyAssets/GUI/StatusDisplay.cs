// ステータスの表示を管理するスクリプト。

using R3;
using UnityEngine;
using uPools;

public class StatusDisplay : MonoBehaviour
{
    [SerializeField] SO_OpenStatus openStatus; // ステータスのScriptableObject

    [Header("HP表示")]
    [SerializeField] GameObject _HPPrefab; // HP表示用のPrefab
    [SerializeField] GameObject _HPGage; // HPゲージのPrefab


    private void Start()
    {
        // プールにHPを最大数まで生成。
        SharedGameObjectPool.Prewarm(_HPPrefab, openStatus._maxHP.Value);

        // openStatus._currentHPを監視し、増減があればHPを更新する。
        openStatus._currentHP.Subscribe(newHP =>
        {
            // HPの変化があったら、UI表示の更新メソッドを呼び出す
            UpdateHP();
        }).AddTo(this);
        
    }

    // HPの表示更新。
    public void UpdateHP()
    {
        // ステータスのHPを取得
        int currentHP = openStatus._currentHP.Value;
        // _HPGageの子要素の数を取得
        int currentHPGageCount = _HPGage.transform.childCount;


        // _HPGageの子オブジェクトを currentHPの数と合わせる。
        if (currentHP > currentHPGageCount)
        {
           
        }
    }

}