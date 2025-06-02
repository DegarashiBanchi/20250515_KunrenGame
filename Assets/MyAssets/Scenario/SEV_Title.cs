// タイトル画面のマネージャ。

using System;
using System.Threading.Tasks;
using AnnulusGames.SceneSystem;
using Cysharp.Threading.Tasks;
using LitMotion.Animation;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SEV_Title : MonoBehaviour
{
    [Header("シーン関連")]
    [SerializeField] private SceneLoadChatcer _sceneLoader;  // シーン遷移を管理するスクリプト
    [SerializeField] private SceneReference _stageScene01;    // ステージシーンの参照
    [SerializeField] private SceneReference _titleScene;    // タイトルシーンの参照

    [Header("UI演出")]
    [SerializeField] private RawImage _titleLogo; // タイトルロゴのスプライトレンダラー
    [SerializeField] private TMP_Text _pressText; // 「Press Start」のテキストコンポーネント

    [Header("コマ画像")]
    [SerializeField] LitMotionAnimation _mangaObject; // タイトルマンガのLitMotionAnimationコンポーネント
    [SerializeField] LitMotionAnimation _wakeUpKoma; // 起床コマのLitMotionAnimationコンポーネント
    [SerializeField] LitMotionAnimation _monitorKoma; // モニターコマのLitMotionAnimationコンポーネント
    [SerializeField] private LitMotionAnimation _helpKoma; // ヘルプコマのLitMotionAnimationコンポーネント
    [SerializeField] private LitMotionAnimation _stairsKoma; // 階段コマのLitMotionAnimationコンポーネント



    private void Start()
    {
        OnAnyKeyPressed().Forget();
    }


    // なんらかのキーが押されたときに呼ばれるメソッド。
    public async UniTask OnAnyKeyPressed()
    {
        // キー入力待機イベントを開始。
        var keyboard = Keyboard.current;
        await UniTask.WaitUntil(() => keyboard != null && keyboard.anyKey.wasPressedThisFrame);

        // タイトル開始メソッドを呼び出す。
        TitleStart().Forget();
    }

    private async UniTask TitleStart()
    {
        // テキストを非表示に。
        _pressText.gameObject.SetActive(false);

        // タイトルロゴのカラーを変更。
        _titleLogo.color = new Color(1f, 1f, 1f, 1f);

        // タイトルマンガ演出のスタート。
        await StartTitleManga();

        // メインステージへシーン遷移。
        _sceneLoader.UnloadAndLoadSet(_titleScene, _stageScene01);
    }

    // タイトルマンガの演出メソッド。
    private async UniTask StartTitleManga()
    {
        // 漫画オブジェクトを徐々に加速させながら上方向へスライドアウト。
        _mangaObject.Play();

        await UniTask.Delay(TimeSpan.FromSeconds(2.5f)); // スライド開始まで待機。

        // 起床コマを左にスライドイン。
        _wakeUpKoma.Play();
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));

        // モニターコマを表示してヘルプコマを点滅。
        _monitorKoma.Play();
        _helpKoma.Play();

        await UniTask.Delay(TimeSpan.FromSeconds(1.5f)); // 待機。

        // 階段コマを表示開始。
        _stairsKoma.Play();

        // 漫画演出が終了するまで待機。
        await UniTask.Delay(TimeSpan.FromSeconds(7.0f));

    }
}