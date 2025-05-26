using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using System;
public class WebRequestSample : MonoBehaviour
{
    private async void Start()
    {
        string url = "https://jsonplaceholder.typicode.com/posts/1";
        try

        {
            string json = await GetJsonAsync(url);
            Debug.Log($"取得したJSON: {json}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"エラー発生: {ex.Message}");
        }
    }
    private async UniTask<string> GetJsonAsync(string url)
    {
        using var request = UnityWebRequest.Get(url);
        await request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            throw new Exception(request.error);
        }
        return request.downloadHandler.text;
    }
}