using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Camera))]
public class OverlayCameraStacker : MonoBehaviour
{
    private Camera _thisCam;     // このゲームオブジェクトにアタッチされた Camera コンポーネント
    private UniversalAdditionalCameraData
                                _thisCamData; // URP 用の追加データ

    private void Start()
    {
        // 非同期メソッドを Fire-and-Forget で呼び出し
        InitializeStackingAsync().Forget();
    }

    private async UniTask InitializeStackingAsync()
    {
        // このゲームオブジェクトの Camera と追加データを取得
        _thisCam = GetComponent<Camera>();
        _thisCamData = _thisCam.GetUniversalAdditionalCameraData();

        // 自分が MainCamera であれば、以降の Overlay 化は不要
        if (_thisCam.CompareTag("MainCamera"))
            return;

        // MainCamera がシーンに現れるまで毎フレーム待機
        Camera mainCam;
        while ((mainCam = Camera.main) == null)
        {
            await UniTask.Yield();
        }

        // MainCamera の URP データを取得
        var mainCamData = mainCam.GetUniversalAdditionalCameraData();

        // このカメラを Overlay タイプに変更
        _thisCamData.renderType = CameraRenderType.Overlay;

        // MainCamera のスタックに追加（描画順序：MainCamera の後にこのカメラが重なる）
        mainCamData.cameraStack.Add(_thisCam);
    }
}