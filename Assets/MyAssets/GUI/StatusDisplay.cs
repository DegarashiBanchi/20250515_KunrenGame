using UnityEngine;
using R3;
using uPools;

public class StatusDisplay : MonoBehaviour
{
    [SerializeField]
    private SO_OpenStatus openStatus; // ステータス情報のScriptableObject

    [Header("HP表示")]
    [SerializeField] private GameObject hpPrefab; // HP表示用Prefab
    [SerializeField] private GameObject hpGauge;   // HPゲージの親オブジェクト

    private void Start()
    {
        // プールに最大HP分のオブジェクトを用意しておく
        SharedGameObjectPool.Prewarm(hpPrefab, openStatus._maxHP.Value);

        // openStatus._currentHPの変更を監視し、HP表示を更新する
        openStatus._currentHP.Subscribe(newHP => UpdateHP()).AddTo(this);
    }

    /// 現在のHPと表示されているHP数の差分を反映してUIを更新する
    public void UpdateHP()
    {
        int currentHP = openStatus._currentHP.Value;
        int displayedHP = hpGauge.transform.childCount;
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

    /// HP表示用オブジェクトを指定数追加する
    private void AddHPGages(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject hpDisplay = SharedGameObjectPool.Rent(hpPrefab);
            hpDisplay.transform.SetParent(hpGauge.transform, false);
        }
    }

    /// HP表示用オブジェクトを指定数削除（プールに返却）する
    private void RemoveHPGages(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int lastIndex = hpGauge.transform.childCount - 1;
            if (lastIndex >= 0)
            {
                Transform lastChild = hpGauge.transform.GetChild(lastIndex);
                SharedGameObjectPool.Return(lastChild.gameObject);
            }
        }
    }
}