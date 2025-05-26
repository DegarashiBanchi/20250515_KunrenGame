// プレヤーが確認可能なステータスを管理するScriptableObject

using R3;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_OpenStatus", menuName = "ScriptableObject/SO_OpenStatus")]
public class SO_OpenStatus : ScriptableObject
{
    [SerializeField] public SerializableReactiveProperty<int> _score = new(); // スコア
    [SerializeField] public SerializableReactiveProperty<int> _highScore = new(); // ハイスコア
    [SerializeField] public SerializableReactiveProperty<int> _currentHP = new(); // 現在のHP
    [SerializeField] public SerializableReactiveProperty<int> _maxHP = new(5); // 最大HP

    [SerializeField] public SerializableReactiveProperty<float> _currentDepth = new(0.0f); // 現在の深度。


    // 初期化メソッド。
    public void Initialize()
    {
        _score.Value = 0;
        _currentHP.Value = _maxHP.Value;
        _currentDepth.Value = 0.0f;

        Debug.Log("SO_OpenStatus 初期化");
    }
}