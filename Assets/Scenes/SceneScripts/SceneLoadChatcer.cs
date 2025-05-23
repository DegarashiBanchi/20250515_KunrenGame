// マネージャシーンのSceneLoadUtilityをキャッチするスクリプト。

using AnnulusGames.SceneSystem;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class SceneLoadChatcer : MonoBehaviour
{
    [SerializeField] private SceneReference managerScene;    // ManagerScene の参照
    private SceneLoadUtility _utility;                     // ManagerScene 内のユーティリティ

    // シーンのロードとアンロードをセットで依頼するメソッド。
    public void UnloadAndLoadSet(SceneReference S_Unload, SceneReference S_Load)
    {
        EnsureUtilityAsync();
        _utility.SceneLoadAndUnload(S_Unload, S_Load).Forget();
    }

    /// targetScene をロードしたいときに呼ぶ。
    public async UniTask Load(SceneReference targetScene)
    {
        EnsureUtilityAsync();
        await _utility.LoadSceneIfNotLoaded(targetScene);
    }

    /// targetScene をアンロードしたいときに呼ぶ。
    /// Utility があればアンロード、なければエラーをログ。
    public async UniTask Unload(SceneReference targetScene)
    {
        if (_utility != null)
            await _utility.UnloadScene(targetScene);
        else
            Debug.LogError("SceneLoaderUtility が見つかっていません。");
    }

    /// シーン内の SceneLoaderUtility を探してキャッシュする。
    private void EnsureUtilityAsync()
    {
        // 既にキャッシュ済みなら探索不要
        if (_utility != null) return;

        // マネージャシーンを取得（パス指定）
        var mgr = SceneManager.GetSceneByPath(managerScene.assetPath);

        // ルートオブジェクトからユーティリティを探す
        _utility = mgr
            .GetRootGameObjects()
            .Select(go => go.GetComponent<SceneLoadUtility>())
            .FirstOrDefault(u => u != null);

        if (_utility == null)
            Debug.LogError("ManagerScene 内に SceneLoaderUtility がありません。");
    }
}