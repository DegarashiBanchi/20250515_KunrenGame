// エームステートを管理するステートマシン。

using IceMilkTea.StateMachine;
using UnityEditor;
using UnityEngine;

public class SM_GameState : MonoBehaviour
{
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
    private class TitleState : ImtStateMachine<SM_GameState , GameStateEvents>.State
    {
       
    }

    // オープニング画面ステート
    private class OPState : ImtStateMachine<SM_GameState, GameStateEvents>.State
    {

    }

    // ゲーム画面ステート
    private class GameState : ImtStateMachine<SM_GameState, GameStateEvents>.State
    {
    }

    // ゲームオーバー画面ステート
    private class GameOverState : ImtStateMachine<SM_GameState, GameStateEvents>.State
    {
    }


    // クリア画面ステート
    private class ClearState : ImtStateMachine<SM_GameState, GameStateEvents>.State
    {
    }


    // ステートマシンの遷移メソッド
    // ゲームステートへの遷移。
    public void TransitionToGameState()
    {
        _stateMacine.SendEvent(GameStateEvents.ToGame);
    }
}
