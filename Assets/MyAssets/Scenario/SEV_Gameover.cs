// ゲームオーバー処理を担当するスクリプト。

using AnnulusGames.SceneSystem;
using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using R3;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SEV_Gameover : MonoBehaviour
{
    [SerializeField] SO_MaskStatus _MaskStatus; // プレイヤーの状態を管理するScriptableObject
    [Header("シーン関連")]
    [SerializeField] SceneLoadChatcer _sceneLoader; // シーン遷移を管理するスクリプト
    [SerializeField] SceneReference _titleScene; // タイトルシーンの参照
    [SerializeField] SceneReference _mainScene; // メインシーンの参照

    [Header("UI演出")]
    [SerializeField] GameObject _gameoverPanel; // ゲームオーバー時に表示する黒いスクリーン
    [SerializeField] TMP_Text _gameoverText; // ゲームオーバーのメッセージを表示するTextMeshProUGUIコンポーネント
    [SerializeField] CameraFocusManager _cameraFocusManager; // カメラのフォーカスマネージャ
    [SerializeField] HorizontalLayoutGroup _gameoverLayout; // ゲームオーバーの選択肢を配置するレイアウトグループ
    [SerializeField] SetFocusObject _focusObject; // 選択肢へのフォーカスを管理するSetFocusObject
    [SerializeField] CustomSelectable _selectableRetry; // リトライ選択肢のカスタムセレクタブル

    private void Start()
    {
        _MaskStatus._isGameOver.Subscribe(isGameOver =>
        {
            if (isGameOver)
            {
                GameOver().Forget(); // ゲームオーバーの処理を呼び出す
            }
        }).AddTo(this);
    }

    // ゲームオーバーのメイン処理メソッド。
    public async UniTask GameOver()
    {
        Debug.Log("ゲームオーバー");
        // カメラのフォーカスを解除
        _cameraFocusManager.ReleaseCameraFocus();

        // タイマーを一時停止。
        _MaskStatus._isTimerRunning.Value = false;

        // 画面に黒スクリーンを表示。
        _gameoverPanel.SetActive(true);

        // 画面上にゲームオーバーのメッセージを表示。
        await ShowGameOverText();

        // リトライ選択肢を表示。
        _gameoverLayout.gameObject.SetActive(true);

        // リトライ選択肢にフォーカスを当てる。
        _focusObject.SetFocusOnObject(_selectableRetry.gameObject);
    }

    // ゲームオーバー画面を閉じるメソッド。
    private void CloseGameOverScreen()
    {
        // ゲームオーバーパネルを非表示に。
        _gameoverPanel.SetActive(false);
        // 選択肢のレイアウトを非表示に。
        _gameoverLayout.gameObject.SetActive(false);

    }


    // ゲームオーバーのテキスト表示メソッド。
    public async UniTask ShowGameOverText()
    {
        // テキストを不透明化。
        var handle_GOText = LMotion.Create(0f, 1f, 0.5f)
            .BindToColorA(_gameoverText)
            .AddTo(gameObject);

        // テキストの文字間隔を広げる。
        var handle_GOtextChara = LMotion.Create(-100f, 64f, 0.7f)
            .WithEase(Ease.OutSine)
            .Bind(chara => _gameoverText.characterSpacing = chara)
            .AddTo(gameObject);

        await handle_GOtextChara; // 完了まで待機。
    }

    // リトライ選択肢を選択したときの処理メソッド。
    public void RetrySelected()
    {
        // ゲームオーバー画面を閉じる処理を実行。
        CloseGameOverScreen();

        // プレイヤーの状態を初期化。
        _MaskStatus.Initialize();
    }


    // やめる選択肢を選択したときの処理メソッド。
    public void ExitSelected()
    {
        // タイトルシーンに戻る処理を実行。
        _sceneLoader.UnloadAndLoadSet(_mainScene, _titleScene);
    }
}