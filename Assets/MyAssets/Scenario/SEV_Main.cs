using System.Threading.Tasks;
using AnnulusGames.SceneSystem;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SEV_Main : MonoBehaviour
{
    [SerializeField] GameObject _PC; // プレイヤーキャラクターのGameObject
    [SerializeField] Transform _startPosition; // プレイヤーキャラクターのスタート位置
    [SerializeField] CameraFocusManager _cameraFocusManager; // カメラフォーカスマネージャの参照
    [SerializeField] StateMachineCatcher _stateMachineCatcher; // ステートマシンキャッチャの参照
    [SerializeField] SO_OpenStatus _openStatus; // プレイヤーのステータスを管理するScriptableObject
    [SerializeField] SO_MaskStatus _maskStatus; // プレイヤーの被弾状態を管理するScriptableObject

    [Header("シーン関連")]
    [SerializeField] SceneLoadChatcer _sceneLoader; // シーン遷移を管理するスクリプト
    [SerializeField] SceneReference _statusScene; // ステータスシーンの参照
    [SerializeField] SceneReference _mainScene; // メインシーンの参照
    [SerializeField] SceneReference _edScene; // エンディングシーンの参照

    private void Start() 
    {
        _sceneLoader.Load(_statusScene).Forget(); // ステータスシーンのロードを開始
        InitializeMainGame().Forget(); // メインゲームの初期化処理を呼び出す
    }


    // メインゲームの初期化処理メソッド。
    public async UniTask InitializeMainGame()
    {
        // _PCプレファブをスタート地点に生成。
        _PC = Instantiate(_PC, _startPosition.position, Quaternion.identity);

        // カメラのフォーカスをPCにセット。
        SetCameraFocusToPC(_PC.transform);

        // ステートのメインへのチェンジ。
        await ChangeStateToMain();

        // SOを初期化。
        _openStatus.Initialize();
        _maskStatus.Initialize();
    }

    public void SetCameraFocusToPC(Transform _pc)
    {
        _cameraFocusManager.SetCameraFocus(_pc);
    }

    private async UniTask ChangeStateToMain()
    {
        // ステートマシンキャッチャからステートマシンを非同期で取得。
        SM_GameState stateMachine = await _stateMachineCatcher.GetStateMachine();

        // ステートをメインに変更。
        stateMachine.TransitionToGameState();
    }

    // メインシーンの終了処理。
    public async UniTask EndMainScene()
    {
        Debug.Log("メインシーン終了");

        // ステータスシーンをアンロード。
        await _sceneLoader.Unload(_statusScene);

        // エンディングシーンへの遷移を依頼。
        _sceneLoader.UnloadAndLoadSet(_mainScene, _edScene);
    }
}
