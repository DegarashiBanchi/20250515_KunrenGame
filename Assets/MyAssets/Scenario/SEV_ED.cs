// エンディングのシナリオイベントを管理するクラス

using AnnulusGames.SceneSystem;
using TMPro;
using UnityEngine;

public class SEV_ED : MonoBehaviour
{
    [Header("各種スコアデータ参照用")]
    [SerializeField] SO_OpenStatus _openStatus; // プレイヤーのステータスを管理するScriptableObjectの参照
    [SerializeField] SO_MaskStatus _maskStatus; // マスクステータスを管理するScriptableObjectの参照

    [Header("テキスト")]
    [SerializeField] TMP_Text _timeText; // テキストコンポーネントの参照
    [SerializeField] TMP_Text _baggageText; // 回収物資テキストの参照
    [SerializeField] TMP_Text _hpText; // HPテキストの参照
    [SerializeField] SetFocusObject _focusObject; // 選択肢へのフォーカスを管理するSetFocusObjectの参照
    [SerializeField] GameObject _selectableRetry; // 選択肢のカスタムセレクタブル

    [Header("シーン関連")]
    [SerializeField] SceneReference _stageScene01; // ステージシーンの参照
    [SerializeField] SceneReference _titleScene; // タイトルシーンの参照
    [SerializeField] SceneReference _edScene; // EDシーンの参照
    [SerializeField] SceneLoadChatcer _sceneLoader; // シーン遷移を管理するスクリプト

    private void Start()
    {
        // 各スコアの表示。
        ShowTimeScore();
        ShowBaggageScore();
        ShowHPScore();

        // 選択肢へのフォーカス。
        SetFocus();
    }

    // タイムスコアの表示。
    public void ShowTimeScore()
    {
        _timeText.text = $"タイム: {_openStatus._time.Value:F2}秒";
    }

    // 回収物資の表示。
    public void ShowBaggageScore()
    {
        int collectedItems = 0;
        if (_openStatus._hasItem1.Value) collectedItems++;
        if (_openStatus._hasItem2.Value) collectedItems++;
        if (_openStatus._hasItem3.Value) collectedItems++;
        if (_openStatus._hasKeyItem.Value) collectedItems++;
        _baggageText.text = $"回収物資: {collectedItems}個";
    }

    // HPの表示。
    public void ShowHPScore()
    {
        _hpText.text = $"HP: {_openStatus._currentHP.Value}/{_openStatus._maxHP.Value}";
    }

    // 選択肢のフォーカスを設定するメソッド。
    public void SetFocus()
    {
        //Debug.Log("フォーカスよし");
        _focusObject.SetFocusOnObject(_selectableRetry);
    }

    // リトライボタンが押されたときの処理。
    public void OnRetryButtonPressed()
    {
        // リトライの処理を実行。
        _openStatus.Initialize(); // ステータスを初期化
        _maskStatus.Initialize(); // マスクステータスを初期化

        // ステージシーンへの遷移を依頼。
        _sceneLoader.UnloadAndLoadSet(_edScene,_stageScene01);
    }

    // タイトルボタンが押されたときの処理。
    public void OnTitleButtonPressed()
    {
        // リトライの処理を実行。
        _openStatus.Initialize(); // ステータスを初期化
        _maskStatus.Initialize(); // マスクステータスを初期化

        // タイトルシーンへの遷移を依頼。
        _sceneLoader.UnloadAndLoadSet(_edScene, _titleScene);
    }
}