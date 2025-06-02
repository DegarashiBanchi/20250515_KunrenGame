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
    private GameObject _pcInstance;                      // シーン上のPCインスタンス
    [SerializeField] private Transform _startPosition;   // プレイヤーキャラクターのスタート位置
    [SerializeField] private CameraFocusManager _cameraFocusManager; // カメラフォーカスマネージャの参照
    [SerializeField] private StateMachineCatcher _stateMachineCatcher; // ステートマシンキャッチャの参照
    [SerializeField] private SO_OpenStatus _openStatus;  // プレイヤーのステータス管理
    [SerializeField] private SO_MaskStatus _maskStatus;  // プレイヤーの被弾状態管理

    [Header("シーン関連")]
    [SerializeField] private SceneLoadChatcer _sceneLoader;  // シーン遷移を管理するスクリプト
    [SerializeField] private SceneReference _statusScene;    // ステータスシーンの参照
    [SerializeField] private SceneReference _mainScene;      // メインシーンの参照
    [SerializeField] private SceneReference _edScene;        // エンディングシーンの参照

    [Header("UI演出")]
    [SerializeField] private ReadyPanelDisplay _readyPanel;  // Readyパネルの表示を制御するスクリプト

    private void Awake()
    {
        _sceneLoader.Load(_statusScene).Forget(); // ステータスシーンのロード開始
        InitializeMainGame().Forget();            // メインゲームの初期化処理呼び出し
    }

    /// <summary>
    /// メインゲームの初期化処理を非同期で実行する。
    /// </summary>
    public async UniTask InitializeMainGame()
    {
        try
        {
            // プレイヤーキャラクターの生成と配置
            SpawnPlayerCharacter();

            // カメラのフォーカスをPCに合わせる
            SetCameraFocus(_pcInstance.transform);

            // ステートマシンをゲームステートに遷移
            await TransitionToGameState();

            // 状態管理用SOを初期化
            InitializeStatus();

            // Readyパネルの表示と演出
            await ShowReadyPanel();

            // タイマーを開始
            _maskStatus._isTimerRunning.Value = true;
        }
        catch (Exception ex)
        {
            Debug.LogError($"InitializeMainGame failed: {ex.Message}");
        }
    }

    /// <summary>
    /// プレイヤーキャラクターを生成し、シーンに配置する。
    /// </summary>
    private void SpawnPlayerCharacter()
    {
        // 既存のPCインスタンスがあれば削除
        if (_pcInstance != null)
        {
            Destroy(_pcInstance);
        }

        // プレハブからPCインスタンスを生成（スタート位置に）
        _pcInstance = Instantiate(_pcPrefab, _startPosition.position, Quaternion.identity);

        // 現在のシーンにPCを移動
        SpawnPCInThisScene();

        // 生成後は親を解除（シーン独立）
        _pcInstance.transform.SetParent(null);
    }

    /// <summary>
    /// カメラのフォーカスを指定したターゲットに設定する。
    /// </summary>
    /// <param name="target">フォーカス対象のトランスフォーム</param>
    private void SetCameraFocus(Transform target)
    {
        if (_cameraFocusManager != null && target != null)
        {
            _cameraFocusManager.SetCameraFocus(target);
        }
    }

    /// <summary>
    /// ステートマシンをゲームステートに非同期で遷移させる。
    /// </summary>
    private async UniTask TransitionToGameState()
    {
        try
        {
            // ステートマシンキャッチャから非同期でステートマシンを取得
            SM_GameState stateMachine = await _stateMachineCatcher.GetStateMachine();

            // メインステートへ遷移
            stateMachine.TransitionToState(SM_GameState.GameStateEvents.ToGame);
        }
        catch (Exception ex)
        {
            Debug.LogError($"TransitionToGameState failed: {ex.Message}");
        }
    }

    /// <summary>
    /// 状態管理用スクリプタブルオブジェクトを初期化する。
    /// </summary>
    private void InitializeStatus()
    {
        _openStatus?.Initialize();
        _maskStatus?.Initialize();
    }

    /// <summary>
    /// Readyパネルを表示し、キーボード入力待ちを行う。
    /// </summary>
    private async UniTask ShowReadyPanel()
    {
        // Readyパネルを表示
        _readyPanel.SetReadyPanelActive(true);

        // 少し待機
        await UniTask.Delay(TimeSpan.FromSeconds(1));

        // Readyパネルのテキストを順次表示
        await _readyPanel.ShowReadyPanelTexts();

        // キーボード入力待機
        await WaitForKeyboardInput();

        // Readyパネルを非表示に
        _readyPanel.SetReadyPanelActive(false);
    }

    /// <summary>
    /// キーボード入力を非同期で待機する。
    /// </summary>
    private async UniTask WaitForKeyboardInput()
    {
        await UniTask.WaitUntil(() => Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame);
    }

    /// <summary>
    /// メインシーン終了時の処理を非同期で実行する。
    /// </summary>
    public async UniTask EndMainScene()
    {
        try
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

            // エンディングシーンへの遷移
            _sceneLoader.UnloadAndLoadSet(_mainScene, _edScene);
        }
        catch (Exception ex)
        {
            Debug.LogError($"EndMainScene failed: {ex.Message}");
        }
    }

    /// <summary>
    /// プレイヤーキャラクターを現在のシーン内に移動させる。
    /// </summary>
    public void SpawnPCInThisScene()
    {
        if (_pcInstance != null)
        {
            Scene targetScene = this.gameObject.scene;
            SceneManager.MoveGameObjectToScene(_pcInstance, targetScene);
        }
    }
}