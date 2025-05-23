using UnityEngine;        
using TMPro;              

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

    // Startメソッドは、スクリプトが有効化されたときやゲーム開始時に一度だけ呼び出されます。
    private void Start()
    {
        // タイマーの初期化（初期時間にリセットし、経過時間を0に設定）
        ResetTimer();
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

    // 外部から呼び出してタイマーを一時停止するためのパブリックメソッド
    public void PauseTimer()
    {
        // タイマーの更新処理を停止するためにフラグをtrueにする
        isPaused = true;
    }

    // 外部から呼び出して一時停止状態のタイマーを再開するためのパブリックメソッド
    public void ResumeTimer()
    {
        // タイマーの更新処理を再び有効にするためにフラグをfalseにする
        isPaused = false;
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

    // タイマーに指定秒数の時間を追加するためのメソッド
    public void AddTime(float seconds)
    {
        // seconds分の時間を追加し、結果が負にならないように Mathf.Max で調整
        currentTime = Mathf.Max(currentTime + seconds, 0f);

        // 追加後の残り時間に基づいて、総経過時間を再計算
        elapsedTime = initialTime - currentTime;

        // 変更された時間情報を画面に反映
        UpdateTimerDisplay();
    }

    // タイマーから指定秒数分の時間を減算するためのメソッド
    public void SubtractTime(float seconds)
    {
        // 指定秒数分の時間を減算し、結果が0未満にならないよう Mathf.Max 関数で調整
        currentTime = Mathf.Max(currentTime - seconds, 0f);

        // 減算後の残り時間に基づき、elapsedTimeを再計算
        elapsedTime = initialTime - currentTime;

        // 更新されたタイマーの状態をUIに反映
        UpdateTimerDisplay();
    }
}
