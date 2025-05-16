using UnityEngine;
using UnityEngine.UI;

public class AspectRatioManager : MonoBehaviour
{
    [SerializeField] private CanvasScaler canvasScaler; // UIスケーリングを管理
    [SerializeField] private float minAspectRatio = 16f / 9f; // 最小アスペクト比（16:9）
    [SerializeField] private float maxAspectRatio = 19.5f / 9f; // 最大アスペクト比（19.5:9）

    private void Start()
    {
        // 起動時にUIスケーリングを調整
        AdjustUIScaling();
    }

    private void AdjustUIScaling()
    {
        // 現在の画面アスペクト比を計算
        float screenAspect = (float)Screen.width / Screen.height;

        // アスペクト比が16:9から19.5:9の範囲内かを判定
        if (screenAspect >= minAspectRatio && screenAspect <= maxAspectRatio)
        {
            // 範囲内ではUIを画面サイズに合わせて伸縮
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(1280, 720); // 基準解像度
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            canvasScaler.matchWidthOrHeight = 0.5f; // 幅と高さの中間スケーリング
        }
        else
        {
            // 範囲外ではアスペクト比を固定し、帯で隠す
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(1280, 720);
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
        }
    }
}