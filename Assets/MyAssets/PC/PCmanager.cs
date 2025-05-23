// PCのだいたいの動きを管理するスクリプト。

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using R3.Collections;

public class PCmanager : MonoBehaviour
{
    [SerializeField] public List<GameObject> _followers; // PCの追従するオブジェクトのリスト

    // _followersにオブジェクトを追加するメソッド。
    public void AddFollower(GameObject follower)
    {
        if (!_followers.Contains(follower))
        {
            _followers.Add(follower);
        }
    }


}
