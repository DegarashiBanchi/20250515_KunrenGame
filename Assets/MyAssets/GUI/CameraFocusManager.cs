// シーンのChinemachineのカメラのフォーカスを管理するクラス

using Unity.Cinemachine;
using UnityEngine;

public class CameraFocusManager : MonoBehaviour
{
    [SerializeField] CinemachineCamera CinemachineCamera; // カメラの参照

    // カメラのフォーカスを設定するメソッド
    public void SetCameraFocus(Transform target)
    {
        if (CinemachineCamera != null)
        {
            // ターゲットの位置をカメラのフォーカスに設定
            CinemachineCamera.Target.TrackingTarget = target;
        }
        else
        {
            Debug.LogWarning("CinemachineCamera is not assigned.");
        }
    }

    // カメラのフォーカスを開放するメソッド。
    public void ReleaseCameraFocus()
    {
        if (CinemachineCamera != null)
        {
            // ターゲットのトラッキングを解除
            CinemachineCamera.Target.TrackingTarget = null;
        }
        else
        {
            Debug.LogWarning("CinemachineCamera is not assigned.");
        }
    }
}