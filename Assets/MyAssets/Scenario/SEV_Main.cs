using System;
using AnnulusGames.SceneSystem;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SEV_Main : MonoBehaviour
{
    [Header("プレイヤー関連")]
    [SerializeField] private GameObject _pcPrefab;       // プレイヤーキャラクターのプレハブ（アセット）
    private GameObject _pcInstance;                        // シーン上のPCインスタンス
    [SerializeField] private Transform _startPosition;     // プレイヤーキャラクターのスタート位置
    [SerializeField] private CameraFocusManager _cameraFocusManager; // カメラフォーカスマネージャの参照
    [SerializeField] private StateMachineCatcher _stateMachineCatcher; // ステートマシンキャッチャの参照
    [SerializeField] private SO_OpenStatus _openStatus;      // プレイヤーのステータス管理
    [SerializeField] private SO_MaskStatus _maskStatus;      // プレイヤーの被弾状態管理

    [Header("シーン関連")]
    [SerializeField] private SceneLoadChatcer _sceneLoader;  // シーン遷移を管理するスクリプト
    [SerializeField] private SceneReference _statusScene;    // ステータスシーンの参照
    [SerializeField] private SceneReference _mainScene;      // メインシーンの参照
    [SerializeField] private SceneReference _edScene;        // エンディングシーンの参照

    [Header("UI演出")]
    [SerializeField] private ReadyPanelDisplay _readyPanel;  // Readyパネルの表示を制御するスクリプト

    private void Start()
    {
        _sceneLoader.Load(_statusScene).Forget(); // ステータスシーンのロード開始
        InitializeMainGame().Forget();            // メインゲームの初期化処理呼び出し
    }

    // メインゲームの初期化処理
    public async UniTask InitializeMainGame()
    {
        // 既にシーン上にPCインスタンスがあれば削除
        if (_pcInstance != null)
        {
            Destroy(_pcInstance);
            _pcInstance = null;
        }

        // プレハブからPCインスタンスを生成（スタート位置に）
        _pcInstance = Instantiate(_pcPrefab, _startPosition.position, Quaternion.identity);

        // 現在のシーンにPCを移動させる
        SpawnPCInThisScene();

        // 生成後は親を解除（シーン独立）
        _pcInstance.transform.SetParent(null);

        // カメラのフォーカスをPCに合わせる
        SetCameraFocusToPC(_pcInstance.transform);

        // ステートマシンキャッチャから状態を取得し、メインステートへ移行
        await ChangeStateToMain();

        // 状態管理用SOを初期化
        _openStatus.Initialize();
        _maskStatus.Initialize();

        // Readyパネルを表示
        _readyPanel.SetReadyPanelActive(true);

        // 少し待機
        await UniTask.Delay(TimeSpan.FromSeconds(1));

        // Readyパネルのテキストを順次表示
        await _readyPanel.ShowReadyPanelTexts();

        // ↓ キーボード入力待機 (念のためKeyboard.currentがnullでないかチェック)
        var keyboard = Keyboard.current;
        await UniTask.WaitUntil(() => keyboard != null && keyboard.anyKey.wasPressedThisFrame);

        // Readyパネルを非表示に
        _readyPanel.SetReadyPanelActive(false);

        // タイマーを開始
        _maskStatus._isTimerRunning.Value = true;
    }

    public void SetCameraFocusToPC(Transform pcTransform)
    {
        _cameraFocusManager.SetCameraFocus(pcTransform);
    }

    private async UniTask ChangeStateToMain()
    {
        // ステートマシンキャッチャから非同期でステートマシンを取得
        SM_GameState stateMachine = await _stateMachineCatcher.GetStateMachine();

        // メインステートへ遷移
        stateMachine.TransitionToState(SM_GameState.GameStateEvents.ToGame);
    }

    // メインシーン終了時の処理
    public async UniTask EndMainScene()
    {
        Debug.Log("メインシーン終了");

        // タイマーを停止
        _maskStatus._isTimerRunning.Value = false;

        // PCインスタンスの削除
        if (_pcInstance != null)
        {
            Destroy(_pcInstance);
            _pcInstance = null;
        }

        // ステータスシーンのアンロード
        await _sceneLoader.Unload(_statusScene);

        // エンディングシーンへの遷移実行
        _sceneLoader.UnloadAndLoadSet(_mainScene, _edScene);
    }

    // PCを現在のシーン内に移動させる
    public void SpawnPCInThisScene()
    {
        if (_pcInstance != null)
        {
            Scene targetScene = this.gameObject.scene;
            SceneManager.MoveGameObjectToScene(_pcInstance, targetScene);
        }
    }
}