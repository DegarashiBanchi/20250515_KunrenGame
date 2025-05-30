// エームステートを管理するステートマシン。

using IceMilkTea.StateMachine;
using UnityEngine;

public class SM_GameState : MonoBehaviour
{
    [SerializeField] SO_MaskStatus _maskStatus; // プレイヤーの被弾状態を管理するScriptableObject
    ImtStateMachine<SM_GameState, GameStateEvents> _stateMacine; // ステートマシンの定義

    // 遷移イベントの定義
    public enum GameStateEvents
    {
        ToTitle, // タイトル画面へ遷移
        ToOP, // オープニング画面へ遷移
        ToGame, // ゲーム画面へ遷移
        ToGameOver, // ゲームオーバー画面へ遷移
        ToClear // クリア画面へ遷移
    }

    private void Awake()
    {
        _stateMacine = new ImtStateMachine<SM_GameState, GameStateEvents>(this); // ステートマシンの初期化

        // 遷移テーブルの作成。
        _stateMacine.AddTransition<TitleState, OPState>(GameStateEvents.ToOP); // タイトル画面からオープニング画面へ遷移
        _stateMacine.AddTransition<OPState, GameState>(GameStateEvents.ToGame); // オープニング画面からゲーム画面へ遷移
        _stateMacine.AddTransition<GameState, GameOverState>(GameStateEvents.ToGameOver); // ゲーム画面からゲームオーバー画面へ遷移
        _stateMacine.AddTransition<GameState, ClearState>(GameStateEvents.ToClear); // ゲーム画面からクリア画面へ遷移
        _stateMacine.AddTransition<GameOverState, TitleState>(GameStateEvents.ToTitle); // ゲームオーバー画面からタイトル画面へ遷移
        _stateMacine.AddTransition<ClearState, TitleState>(GameStateEvents.ToTitle); // クリア画面からタイトル画面へ遷移

        // 起動ステートを設定。
        _stateMacine.SetStartState<TitleState>(); // タイトル画面を初期ステートに設定

        // ステートマシンの起動
        _stateMacine.Update();
    }


    void Update()
    {
        _stateMacine.Update(); // ステートマシンの更新
    }


    // 各種ステートクラスの定義

    // タイトル画面ステート
    private class TitleState : ImtStateMachine<SM_GameState, GameStateEvents>.State
    {
        protected internal override void Enter()
        {
            Debug.Log($"{Context._stateMacine.CurrentStateName}");
        }

    }

    // オープニング画面ステート
    private class OPState : ImtStateMachine<SM_GameState, GameStateEvents>.State
{

}

// ゲーム画面ステート
private class GameState : ImtStateMachine<SM_GameState, GameStateEvents>.State
{
    protected internal override void Enter()
    {
        // PCの操作を無効化
        Context.TogglePCControl(true);
        Debug.Log("ゲーム開始なので移動を有効化");
    }
}

// ゲームオーバー画面ステート
private class GameOverState : ImtStateMachine<SM_GameState, GameStateEvents>.State
{
    protected internal override void Enter()
    {
        // PCの操作を無効化
        Context.TogglePCControl(false);
        Debug.Log("ゲームオーバーなので移動を無効化");
    }

}


// クリア画面ステート
private class ClearState : ImtStateMachine<SM_GameState, GameStateEvents>.State
{
    protected internal override void Enter()
    {
        // PCの操作を無効化
        Context.TogglePCControl(false);
        Debug.Log("クリアなので移動を無効化");
    }
}


// ステートマシンの遷移メソッド
public void TransitionToState(GameStateEvents stateEvents)
{
    // switch文を使用して、指定されたイベントに基づいてステートマシンを遷移させる。
    switch (stateEvents)
    {
        case GameStateEvents.ToTitle:
            _stateMacine.SendEvent(GameStateEvents.ToTitle);
            break;
        case GameStateEvents.ToOP:
            _stateMacine.SendEvent(GameStateEvents.ToOP);
            break;
        case GameStateEvents.ToGame:
            _stateMacine.SendEvent(GameStateEvents.ToGame);
            break;
        case GameStateEvents.ToGameOver:
            _stateMacine.SendEvent(GameStateEvents.ToGameOver);
            break;
        case GameStateEvents.ToClear:
            _stateMacine.SendEvent(GameStateEvents.ToClear);
            break;
    }
}


// PCの操作可能を切り替えるメソッド。
public void TogglePCControl(bool canControl)
{
    _maskStatus._canMove.Value = canControl; // プレイヤーの移動可能状態を更新
}
}
