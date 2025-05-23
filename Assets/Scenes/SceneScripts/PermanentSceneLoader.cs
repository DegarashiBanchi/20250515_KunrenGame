using AnnulusGames.SceneSystem;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PermanentSceneLoader : MonoBehaviour
{
    [SerializeField] private List<SceneReference> permanentScenes;     // 起動時に常駐させたいシーンのリスト

    /// Start() でリスト中の各シーンを未ロードなら Additive で非同期ロードします。
    private void Start()
    {
        // リスト内の各シーン参照について
        foreach (var sceneRef in permanentScenes)
        {
            // assetPath から \"拡張子なしのシーン名\" を抽出
            var sceneName = Path.GetFileNameWithoutExtension(sceneRef.assetPath);

            if (
                 // シーンが未ロードなら
                 !SceneManager.GetSceneByName(name).isLoaded)
            {
                // Additive モードで非同期ロード
                Scenes.LoadSceneAsync(sceneRef, LoadSceneMode.Additive);
            }
            else
            {
                Debug.Log($"\"{sceneName}\" は既にロード済みです。");
            }
        }
    }
}
