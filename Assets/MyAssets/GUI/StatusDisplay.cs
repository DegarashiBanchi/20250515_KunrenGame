using UnityEngine;
using R3;
using uPools;
using System.Collections.Generic;

public class StatusDisplay : MonoBehaviour
{
    [SerializeField]
    private SO_OpenStatus openStatus; // ステータス情報のScriptableObject

    [Header("HP表示")]
    [SerializeField] private GameObject hpPrefab; // HP表示用Prefab
    [SerializeField] private GameObject hpGauge;   // HPゲージの親オブジェクト

    // 表示中のHPオブジェクトを管理するリスト
    private List<GameObject> hpDisplayObjects = new List<GameObject>();

    private void Start()
    {
        // プールに最大HP分のオブジェクトを用意しておく
        SharedGameObjectPool.Prewarm(hpPrefab, openStatus._maxHP.Value);

        // openStatus._currentHPの変更を監視し、HP表示を更新する
        openStatus._currentHP.Subscribe(newHP => UpdateHP()).AddTo(this);

        // 初期状態でHPを表示（必要に応じて）
        UpdateHP();
    }

    /// <summary>
    /// 現在のHPと表示されているHP数の差分を反映してUIを更新する
    /// </summary>
    public void UpdateHP()
    {
        int currentHP = openStatus._currentHP.Value;
        int displayedHP = hpDisplayObjects.Count; // リストの要素数を使用
        int deltaHP = currentHP - displayedHP;

        if (deltaHP > 0)
        {
            AddHPGages(deltaHP);
        }
        else if (deltaHP < 0)
        {
            RemoveHPGages(-deltaHP);
        }
    }

    /// <summary>
    /// HP表示用オブジェクトを指定数追加する
    /// </summary>
    /// <param name="amount">追加する数</param>
    private void AddHPGages(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject hpDisplay = SharedGameObjectPool.Rent(hpPrefab);
            if (hpDisplay != null) // プールからのレンタルが成功したか確認
            {
                hpDisplay.transform.SetParent(hpGauge.transform, false);
                hpDisplayObjects.Add(hpDisplay);
            }
            else
            {
                Debug.LogWarning("HP表示オブジェクトのレンタルに失敗しました。");
            }
        }
    }

    /// <summary>
    /// HP表示用オブジェクトを指定数削除（プールに返却）する
    /// </summary>
    /// <param name="amount">削除する数</param>
    private void RemoveHPGages(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (hpDisplayObjects.Count > 0)
            {
                GameObject hpDisplay = hpDisplayObjects[hpDisplayObjects.Count - 1];
                hpDisplayObjects.RemoveAt(hpDisplayObjects.Count - 1);
                SharedGameObjectPool.Return(hpDisplay);
            }
            else
            {
                Debug.LogWarning("削除するHP表示オブジェクトがありません。");
                break;
            }
        }
    }
}