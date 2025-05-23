// ステートマシンをキャッチするためのクラス。

using AnnulusGames.SceneSystem;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateMachineCatcher : MonoBehaviour
{
    [SerializeField] SceneReference _managerScene; // マネージャシーンの参照
    public SM_GameState _stateMachine; // ステートマシンクラスの参照

    async void Start()
    {
        await CatchStateMachineAsync();
    }

    // _stateMachineを渡すメソッド
    public async UniTask<SM_GameState> GetStateMachine()
    {
        // _stateMachineがnullなら、マネージャシーンをキャッチする
        if (_stateMachine == null)
        {
            await CatchStateMachineAsync();
        }

        return _stateMachine;
    }

    // マネージャシーンのステートマシンをキャッチする非同期メソッド
    private async UniTask CatchStateMachineAsync()
    {
        string scenePath = _managerScene.assetPath;

        // シーンがロードされるまで待機
        await UniTask.WaitUntil(() => IsSceneLoaded(scenePath));

        // シーンがロードされたので、ステートマシンをキャッチ
        CatchStateMachine();
    }

    // シーンがロードされているかどうかを確認するメソッド
    private bool IsSceneLoaded(string scenePath)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.path == scenePath && scene.isLoaded)
            {
                return true;
            }
        }
        return false;
    }

    // マネージャシーンのステートマシンをキャッチするメソッド
    private void CatchStateMachine()
    {
        GameObject managerSceneObject = GameObject.FindWithTag("StateMachine");
        if (managerSceneObject != null)
        {
            _stateMachine = managerSceneObject.GetComponent<SM_GameState>();
            if (_stateMachine != null)
            {
                Debug.Log("ステートマシンキャッチ完了");
            }
            else
            {
                Debug.LogError("ステートマシンクラスが見つからない。");
            }
        }
        else
        {
            Debug.LogError("マネージャシーンが見つからない。");
        }
    }
}