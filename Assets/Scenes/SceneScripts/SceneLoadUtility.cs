// シーンのロード・アンロードを管理するクラス

using AnnulusGames.SceneSystem;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadUtility : MonoBehaviour
{
    // シーンのアンロード・ロードを順番に行うメソッド。
    public async UniTask SceneLoadAndUnload(SceneReference S_Unload, SceneReference S_Load)
    {
        // S_Unloadを先にアンロード。
        await UnloadScene(S_Unload);

        // S_Loadをロード。
        await LoadSceneIfNotLoaded(S_Load);
    }

    // 指定されたシーンのAdhitiveモードでのロードメソッド。
    public async UniTask LoadSceneIfNotLoaded(SceneReference loadScene)
    {
        // loadSceneのassetPathからシーン名を抽出（拡張子を除く）
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(loadScene.assetPath);

        // シーンが既にロードされているか確認   
        if (!IsSceneLoaded(sceneName))
        {
            // Additiveモードでシーンを非同期ロードし、完了まで待機
            await Scenes.LoadSceneAsync(loadScene, LoadSceneMode.Additive).ToUniTask();
            Debug.Log($"{sceneName} のロードに成功！");
        }
    }

    // 指定されたシーンを非同期でアンロードするメソッド。
    public async UniTask UnloadScene(SceneReference unloadScene)
    {
        // シーン名を取得
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(unloadScene.assetPath);

        // シーンがロードされているか確認
        if (!IsSceneLoaded(sceneName))
        {
            Debug.Log($"{sceneName} は未ロード！");
            return;
        }

        // シーンを非同期でアンロードし、完了まで待機
        await Scenes.UnloadSceneAsync(unloadScene).ToUniTask();
        Debug.Log($"{unloadScene.assetPath} のアンロードに成功！");
    }

    // シーン名からロード済みかどうかを確認するメソッド。
    public bool IsSceneLoaded(string sceneName)
    {
        // ロード済みシーンのリストから、指定されたシーン名がロード済みか確認
        Scene scene = SceneManager.GetSceneByName(sceneName);
        if (scene.IsValid() && scene.isLoaded)
        {
            Debug.Log($"{sceneName} は既にロード済み");
            return true;
        }
        return false;
    }
}