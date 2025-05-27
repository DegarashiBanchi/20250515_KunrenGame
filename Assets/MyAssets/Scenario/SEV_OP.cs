// OPシーンのマネージャ。

using System.Security.Cryptography;
using System.Threading.Tasks;
using AnnulusGames.SceneSystem;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SEV_OP : MonoBehaviour
{
    [Header("シーン関連")]
    [SerializeField] SceneLoadChatcer _sceneLoader; // シーン遷移を管理するスクリプト。
    [SerializeField] SceneReference _mainScene; // メインシーンの参照。
    [SerializeField] SceneReference _opScene; // OPシーンの参照。



    private void Start()
    {
        // OPイベントの呼び出し。
        PlayOP().Forget();
    }

    // OPイベントの処理メソッド。
    private async UniTask PlayOP()
    {
        // OPシーンの開始処理。
        Debug.Log("OPシーン開始");

        // Debug。5秒後にOPシーンを終了。
        await UniTask.Delay(5000);

        // OPシーンの終了処理を呼び出し。
        OPsceneEnd();
    }


    // OPシーンの終了処理。
    private void OPsceneEnd()
    {
        Debug.Log("OPシーン終了");
        // メインシーンへの遷移を依頼。
        _sceneLoader.UnloadAndLoadSet(_opScene, _mainScene);

    }

}
