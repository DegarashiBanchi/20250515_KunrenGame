// プレイヤーに見せる必要のない数値の管理SO。

using R3;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_MaskStatus", menuName = "ScriptableObject/SO_MaskStatus")]
public class SO_MaskStatus : ScriptableObject
{
    [SerializeField] public SerializableReactiveProperty<bool> canGoal = new(false); // ゴール状態を管理するReactiveProperty
    [SerializeField] public SerializableReactiveProperty<bool> isBeingHit = new(false); // プレイヤーが被弾中かどうかを管理するReactiveProperty
    [SerializeField] public SerializableReactiveProperty<Vector2> _currentPCPos = new(Vector2.zero); // PCの位置を管理するReactiveProperty
    [SerializeField] public SerializableReactiveProperty<Vector2> _currentPCDirection = new(Vector2.down); // PCの移動方向を管理するReactiveProperty
    [SerializeField] public SerializableReactiveProperty<bool> _isTimerRunning = new(false); // タイマーの開始状態を管理するReactiveProperty
    [SerializeField] public SerializableReactiveProperty<bool> _isGameOver = new(false); // ゲームオーバー状態を管理するReactiveProperty
    [SerializeField] public SerializableReactiveProperty<bool> _canMove = new(true); // プレイヤーの移動可能状態を管理するReactiveProperty


    // 初期化メソッド。
    public void Initialize()
    {
        canGoal.Value = false;
        isBeingHit.Value = false;
        _currentPCPos.Value = Vector2.zero;
        _currentPCDirection.Value = Vector2.down;
        _isTimerRunning.Value = false;
        _isGameOver.Value = false;
        _canMove.Value = true;

        Debug.Log("SO_MaskStatus 初期化");
    }
}   