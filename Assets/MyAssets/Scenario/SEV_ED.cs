// エンディングのシナリオイベントを管理するクラス

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

    private void Start()
    {
        // 各スコアの表示。
        ShowTimeScore();
        ShowBaggageScore();
        ShowHPScore();


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
}