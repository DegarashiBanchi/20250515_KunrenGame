// プレイヤーに見せる必要のない数値の管理SO。

using R3;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_MaskStatus", menuName = "ScriptableObject/SO_MaskStatus")]
public class SO_MaskStatus : ScriptableObject
{
    [SerializeField] public SerializableReactiveProperty<bool> canGoal = new(false); // ゴール状態を管理するReactiveProperty
}