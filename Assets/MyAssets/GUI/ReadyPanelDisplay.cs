// メインゲーム開始直後のReadyパネルの表示・批評所を制御するスクリプト。

using System;
using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Animation;
using LitMotion.Extensions;
using TMPro;
using UnityEngine;

public class ReadyPanelDisplay : MonoBehaviour
{
    [SerializeField] GameObject _readyPanel; // ReadyパネルのGameObject

    [Header("テキスト類")]
    [SerializeField] TMP_Text _mokuhyouText; // ReadyテキストのTextMeshProコンポーネント
    [SerializeField] TMP_Text _desctiptionText; // 説明テキストのTextMeshProコンポーネント
    [SerializeField] TMP_Text _pressText; // pressテキストのTextMeshProコンポーネント


    // 各テキストの順次表示メソッド。
    public async UniTask ShowReadyPanelTexts()
    {
        // Readyテキストの表示
        var handle_mokuhyou = LMotion.Create(0f,1f,0.7f)
            .BindToColorA(_mokuhyouText)
            .AddTo(gameObject);

        await UniTask.Delay(TimeSpan.FromSeconds(0.7)); // 0.7秒待機

        // 説明テキストの表示
        var handle_description = LMotion.Create(0f, 1f, 0.7f)
            .BindToColorA(_desctiptionText)
            .AddTo(gameObject);

        await UniTask.Delay(TimeSpan.FromSeconds(0.7)); // 0.7秒待機

        // pressテキストの表示
        var handle_press = LMotion.Create(0f, 1f, 0.7f)
            .WithLoops(-1,LoopType.Flip)
            .BindToColorA(_pressText)
            .AddTo(gameObject);
    }


    // _readyPanelの表示・非表示を制御するメソッド。
    public void SetReadyPanelActive(bool isActive)
    {
        _readyPanel.SetActive(isActive); // Readyパネルの表示・非表示を設定
    }
}