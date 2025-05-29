// エネミーの往復運動スクリプト。

using UnityEngine;
using LitMotion;
using LitMotion.Extensions;
using Cysharp.Threading.Tasks;
using R3;

public enum MovementDirection
{
    Horizontal,
    Vertical
}

public class EnemyPistonMovement : MonoBehaviour
{
    [SerializeField] private MovementDirection _movementDirection;   // 移動方向：水平か垂直か
    [SerializeField] private float _startOffset;                       // 現在位置からの開始位置オフセット
    [SerializeField] private float _endOffset;                         // 現在位置からの終了位置オフセット
    [SerializeField] private float _movementDuration;                  // 移動にかかる時間
    [SerializeField] private float _delayBeforeMovement;               // 移動前の遅延時間
    [SerializeField] private Ease _easeType;                           // イージングの種類
    private Rigidbody2D _rigidbody2D;                 // Rigidbody2D コンポーネント

    [Header("疑似Verocity")]
    private Vector2 _prevPosition; // 前回の位置
    [SerializeField] SerializableReactiveProperty<Vector2> _selfVerocity = new(Vector2.zero); // 自身の速度を管理するプロパティ

    private void Start()
    {
        // Rigidbody2D コンポーネントを取得
        _rigidbody2D = GetComponent<Rigidbody2D>();

        // 選択された移動方向に応じて処理を分岐する
        if (_movementDirection == MovementDirection.Horizontal)
        {
            float baseX = _rigidbody2D.transform.localPosition.x;
            float startX = baseX + _startOffset;
            float endX = baseX + _endOffset;

            LMotion.Create(startX, endX, _movementDuration)
                .WithEase(_easeType)
                .WithDelay(_delayBeforeMovement)
                .WithLoops(-1, LoopType.Flip)
                .BindToLocalPositionX(_rigidbody2D.transform)
                .AddTo(gameObject);
        }
        else // MovementDirection.Vertical の場合
        {
            float baseY = _rigidbody2D.transform.localPosition.y;
            float startY = baseY + _startOffset;
            float endY = baseY + _endOffset;

            LMotion.Create(startY, endY, _movementDuration)
                .WithEase(_easeType)
                .WithDelay(_delayBeforeMovement)
                .WithLoops(-1, LoopType.Flip)
                .BindToLocalPositionY(_rigidbody2D.transform)
                .AddTo(gameObject);
        }

        // 疑似的なVerocityの初期化
        _prevPosition = _rigidbody2D.transform.position;
    }

    private void Update()
    {
        // Rigidbody2Dの位置から速度を計算
        Vector2 currentPosition = _rigidbody2D.transform.position;
        Vector2 velocity = (currentPosition - _prevPosition) / Time.deltaTime;
        // 速度をプロパティに設定
        _selfVerocity.Value = velocity;
        // 前回の位置を更新
        _prevPosition = currentPosition;
    }
}