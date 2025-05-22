using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SEV_Main : MonoBehaviour
{
    [SerializeField] Transform _PC; // PCのTransform
    [SerializeField] CameraFocusManager _cameraFocusManager; // カメラフォーカスマネージャの参照
    [SerializeField] StateMachineCatcher _stateMachineCatcher; // ステートマシンキャッチャの参照

    private void Start() // 非同期メソッドに変更
    {
        // カメラのフォーカスをPCにセット。
        SetCameraFocusToPC();

        // ステートのメインへのチェンジ。
        ChangeStateToMain().Forget();
    }

    public void SetCameraFocusToPC()
    {
        _cameraFocusManager.SetCameraFocus(_PC);
    }

    private async UniTask ChangeStateToMain()
    {
        // ステートマシンキャッチャからステートマシンを非同期で取得。
        SM_GameState stateMachine = await _stateMachineCatcher.GetStateMachine();

        // ステートをメインに変更。
        stateMachine.TransitionToGameState();
    }
}
