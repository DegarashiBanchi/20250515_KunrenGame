// タイトル画面のマネージャ。

using AnnulusGames.SceneSystem;
using UnityEngine;

public class SEV_Title : MonoBehaviour
{
    
    [Header("ボタン類")]
    [SerializeField] GameObject _startButton; // スタートボタンのゲームオブジェクト。
    [SerializeField] GameObject _howToButton; // 操作方法ボタンのゲームオブジェクト。

    [Header("GUI関連")]
    [SerializeField] SceneLoadChatcer _sceneLoader; // シーン遷移を管理するスクリプト。
    [SerializeField] SceneReference _opScene; // OPシーンの参照。
    [SerializeField] SceneReference _TitleScene; // OPシーンの参照。
    [SerializeField] SetFocusObject _setfocus; // フォーカスをセットするスクリプト。

    private void Start()
    {
        // ゲーム開始時にスタートボタンにフォーカスをセット。
        _setfocus.SetFocusOnObject(_startButton);
    }

    // スタートボタン選択時の処理メソッド。
    public void OnStart()
    {
        Debug.Log("スタート押された");
        SubmitStart();
    }

    // スタート時のメイン処理。
    private void SubmitStart()
    {
        // OPシーンへの遷移を依頼。
        _sceneLoader.UnloadAndLoadSet(_TitleScene,_opScene);
    }
}