// プレヤーが確認可能なステータスを管理するScriptableObject

using R3;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_OpenStatus", menuName = "ScriptableObject/SO_OpenStatus")]
public class SO_OpenStatus : ScriptableObject
{
    [Header("スコア")]
    [SerializeField] public SerializableReactiveProperty<int> _score = new(); // スコア
    [SerializeField] public SerializableReactiveProperty<int> _highScore = new(); // ハイスコア
    [Header("ステータス")]
    [SerializeField] public SerializableReactiveProperty<int> _currentHP = new(); // 現在のHP
    [SerializeField] public SerializableReactiveProperty<int> _maxHP = new(5); // 最大HP

    [Header("ステージ情報")]
    [SerializeField] public SerializableReactiveProperty<float> _currentDepth = new(0.0f); // 現在の深度。
    [SerializeField] public SerializableReactiveProperty<float> _time = new(); // 現在の時間。
    [SerializeField] public SerializableReactiveProperty<bool> _hasItem1 = new(false); // アイテム1取得状況。
    [SerializeField] public SerializableReactiveProperty<bool> _hasItem2 = new(false); // アイテム2取得状況。
    [SerializeField] public SerializableReactiveProperty<bool> _hasItem3 = new(false); // アイテム3取得状況。
    [SerializeField] public SerializableReactiveProperty<bool> _hasKeyItem = new(false); // キーアイテム取得状況。



    // 初期化メソッド。
    public void Initialize()
    {
        _score.Value = 0;
        _currentHP.Value = _maxHP.Value;
        _currentDepth.Value = 0.0f;
        _hasItem1.Value = false;
        _hasItem2.Value = false;
        _hasItem3.Value = false;
        _hasKeyItem.Value = false;

        Debug.Log("SO_OpenStatus 初期化");
    }
}