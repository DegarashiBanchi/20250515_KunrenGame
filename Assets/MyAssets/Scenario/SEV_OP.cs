// OPシーンのマネージャ。

using System;
using System.Threading;
using AnnulusGames.SceneSystem;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class SEV_OP : MonoBehaviour
{
    [Header("シーン関連")]
    [SerializeField] SceneLoadChatcer _sceneLoader; // シーン遷移を管理するスクリプト。
    [SerializeField] SceneReference _mainScene; // メインシーンの参照。
    [SerializeField] SceneReference _opScene; // OPシーンの参照。

    CancellationTokenSource _cts; // キャンセルトークンソース。


    private void Start()
    {
        // トークンの生成。
        _cts = new CancellationTokenSource();


        // キャンセル可能なOPイベントの呼び出し。
        PlayOP(_cts.Token).Forget();
    }

    // OPイベントの処理メソッド。
    private async UniTask PlayOP(CancellationToken _cts)
    {
        try
        {
            Debug.Log("OPシーン開始");
            // Debug。5秒待機。
            await UniTask.Delay(TimeSpan.FromSeconds(5));
        }
        catch (OperationCanceledException e)
        {
            // キャンセルされた場合の処理。
            Debug.Log("OPシーンがキャンセルされました。" + e);

        }
        finally
        {

            // OPシーンの終了処理を呼び出し。
            OPsceneEnd();
        }
    }


    // OPシーンの終了処理。
    private void OPsceneEnd()
    {
        Debug.Log("OPシーン終了");
        // メインシーンへの遷移を依頼。
        _sceneLoader.UnloadAndLoadSet(_opScene, _mainScene);

    }

    // キャンセルキーの入力検知メソッド。
    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Debug.Log("OPキャンセル！");
        }
    }

}
