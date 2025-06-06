﻿using UnityEngine;
using TMPro;
using R3; 

public class CountdownTimer : MonoBehaviour
{
    // 初期カウントダウン時間を保持する変数
    [SerializeField] private float initialTime = 240.0f;

    // タイマーの残り時間を表示するためのTextMeshProUGUIコンポーネントへの参照
    [SerializeField] private TextMeshProUGUI timerText;

    // 現在の残りカウントダウン時間を保持する変数
    private float currentTime;

    // ゲーム開始以降に経過した時間を蓄積する変数
    private float elapsedTime;

    // タイマーが一時停止状態かどうかを示すフラグ
    [SerializeField] private bool isPaused = true;

    [SerializeField] private SO_OpenStatus _openStatus; // プレイヤーのステータスを管理するScriptableObjectの参照
    [SerializeField] private SO_MaskStatus _maskStatus; // プレイヤーの被弾状態を管理するScriptableObjectの参照

    private void Start()
    {
        // タイマーの初期化（初期時間にリセットし、経過時間を0に設定）
        ResetTimer();

        // _maskStatus._isTimerRunning を監視し、値が変更されたときに isPaused を更新
        _maskStatus._isTimerRunning.Subscribe(isRunning =>
        {
            isPaused = !isRunning; // true ならタイマースタート（isPaused = false）、false なら一時停止（isPaused = true）
        }).AddTo(this); 
    }

    // Updateメソッドは毎フレーム実行され、リアルタイムな処理を担当します。
    private void Update()
    {
        // タイマーが一時停止状態でない場合のみ、時間の更新処理を行う
        if (!isPaused)
        {
            // Time.deltaTimeをelapsedTimeに加算して正確な経過時間を計測する
            elapsedTime += Time.deltaTime;

            // 初期設定時間から経過時間を引き、結果が0秒未満にならないよう Mathf.Max 関数で調整
            currentTime = Mathf.Max(initialTime - elapsedTime, 0f);

            // 現在の残り時間が変化したので、UI表示を更新するメソッドを呼び出す
            UpdateTimerDisplay();

            // 現在の残り時間をSO_OpenStatusに反映
            if (_openStatus != null)
            {
                _openStatus._time.Value = currentTime;
            }

            // 残り時間が0になった場合、ゲームオーバー処理を発火させる。
            if (currentTime <= 0f)
            {
                Debug.Log("タイムアウト！");  
                _maskStatus._isGameOver.Value = true; // ゲームオーバーフラグを立てる
            }
        }
    }

    // 現在の残り時間を表示。
    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            // string.Formatを使用して、小数点以下2桁までの形式でcurrentTimeの値をテキストに変換して設定
            timerText.text = string.Format("{0:F2}", currentTime);
        }
    }

    // タイマーを初期状態にリセットするメソッド
    public void ResetTimer()
    {
        // 残り時間を初期設定時間に設定
        currentTime = initialTime;
        // 経過時間をリセット（0に初期化）
        elapsedTime = 0f;
        // リセット後の状態をUIに即座に反映するため表示を更新
        UpdateTimerDisplay();
    }

}