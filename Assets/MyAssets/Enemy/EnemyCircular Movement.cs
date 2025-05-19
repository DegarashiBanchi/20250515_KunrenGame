using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

public class EnemyCircularMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;                 // Rigidbody2D コンポーネント
    [SerializeField] private float _circleRadius = 2f; // 円の直径
    [SerializeField] private float _time;       // 円運動の時間


    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>(); // Rigidbody2D コンポーネントを取得
        float baseX = _rigidbody2D.transform.localPosition.x;
        float endX = baseX + _circleRadius; // 円の直径を計算
        float baseY = _rigidbody2D.transform.localPosition.y;
        float endY = baseY + _circleRadius; // 円の直径を計算

        // X座標の移動開始。
        var handle_CircleX = LMotion.Create(baseX, endX, _time)
            .WithEase(Ease.InOutCubic)
            .WithLoops(-1, LoopType.Flip)
            .BindToLocalPositionX(_rigidbody2D.transform);

        // y座標の移動開始。Xよりも_timeの1/2だけディレイさせる。
        var handle_CircleY = LMotion.Create(baseY,endY,_time)
            .WithEase(Ease.InOutCubic)
            .WithDelay(_time / 2)
            .WithLoops(-1, LoopType.Flip)
            .BindToLocalPositionY(_rigidbody2D.transform);

    }

}