// プレヤーが確認可能なステータスを管理するScriptableObject

using R3;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_OpenStatus", menuName = "ScriptableObject/SO_OpenStatus")]
public class SO_OpenStatus : ScriptableObject
{
    [SerializeField] SerializableReactiveProperty<int> _score = new(); // スコア
    [SerializeField] SerializableReactiveProperty<int> _highScore = new(); // ハイスコア
    [SerializeField] SerializableReactiveProperty<int>  _currentHP = new(0); // 現在のHP
    [SerializeField] SerializableReactiveProperty<int> _maxHP = new(3); // 最大HP


    [SerializeField] SerializableReactiveProperty<float>    _currentDepth = new(0.0f); // 現在の深度。
}