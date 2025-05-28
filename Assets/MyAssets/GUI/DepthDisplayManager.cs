// 深度をSOに保存しつづけるスクリプト。

using R3;
using R3.Triggers;
using TMPro;
using UnityEngine;

public class DepthDisplayManager : MonoBehaviour
{
    [SerializeField] SO_OpenStatus _openSO; // ステータス情報のScriptableObject
    [SerializeField] SO_MaskStatus _maskSO; // マスクステータス情報のScriptableObject
    [SerializeField] Transform _depthBasePoint; // 深度の基準点となるTransform
    [SerializeField] TMP_Text _depthText; // 深度表示用のTextMeshProコンポーネント

    void Start()
    {
        if (_depthText == null)
        {
            // テキストがアタッチされていなければメイン画面に配置されているので、_maskSO._currentPCPosを監視
            Debug.Log("メイン画面の深度更新確認");
            _maskSO._currentPCPos.Subscribe(pos =>
            // 値が更新されるたびに深度を更新
                UpdateDepthFromPCPosition()
            ).AddTo(this);
        }
        else
        {
            // テキストがアタッチされていれば、_openSO._currentDepthを監視
            Debug.Log("深度表示用のTextMeshProがアタッチされているので、_openSO._currentDepthを監視");
            _openSO._currentDepth.Subscribe(depth =>
            {
                // 深度が更新されたらテキストを更新
                _depthText.text = $"{depth * -1:F2} m";
            }).AddTo(this);
        }
    }


    // PCの位置から深度を更新するメソッド。
    private void UpdateDepthFromPCPosition()
    {
        // PCの位置と基準点のY座標の差を計算
        float depth = _maskSO._currentPCPos.Value.y - _depthBasePoint.position.y;
        // 深度をScriptableObjectに保存
        _openSO._currentDepth.Value = depth;
    }
}
