using UnityEngine;
using System.Linq;
using UnityEngine.Rendering.Universal; // LINQを使用するために追加

public class TestCameraStack : MonoBehaviour
{
    private Camera _mainCamera; // メインカメラの参照
    [SerializeField] private Camera _bottomCamera; // スタック最下層に設置するカメラ

    private void Start()
    {
        // シーン内のメインカメラを取得
        _mainCamera = Camera.main;

        // メインカメラが設定されているか確認
        if (_mainCamera == null)
        {
            Debug.LogError("シーンにメインカメラがありません。'MainCamera'タグが設定されていることを確認してください。", this);
            return;
        }

        // _bottomCameraが設定されているか確認
        if (_bottomCamera == null)
        {
            Debug.LogError("スタック最下層に設置するカメラ(_bottomCamera)が設定されていません。", this);
            return;
        }

               // スタックに_bottomCameraがあれば、それを一番下に移動させる
        MoveCameraToStackBottom(_mainCamera, _bottomCamera);
    }

    /// <summary>
    /// 指定されたレンダーカメラをベースカメラのスタック順の最下層に移動させます。
    /// </summary>
    /// <param name="baseCamera">ベースとなるメインカメラ。</param>
    /// <param name="renderCameraToMove">移動させるレンダーカメラ。</param>
    private void MoveCameraToStackBottom(Camera baseCamera, Camera renderCameraToMove)
    {
        var baseCameraData = baseCamera.GetUniversalAdditionalCameraData();

        // 対象のレンダーカメラが既にスタック内に存在するか確認
        if (baseCameraData.cameraStack.Contains(renderCameraToMove))
        {
            // スタックから一時的に削除し、再度先頭に追加することで最下層に移動させる
            baseCameraData.cameraStack.Remove(renderCameraToMove);
            baseCameraData.cameraStack.Insert(0, renderCameraToMove); // 0番目に挿入
            Debug.Log($"カメラ '{renderCameraToMove.name}' をメインカメラのスタック最下層に移動しました。");
        }
        else
        {
            // スタック内に存在しない場合は、新しく最下層に追加する
            baseCameraData.cameraStack.Insert(0, renderCameraToMove); // 0番目に挿入
            Debug.Log($"カメラ '{renderCameraToMove.name}' をメインカメラのスタックに新しく最下層として追加しました。");
        }
    }
}