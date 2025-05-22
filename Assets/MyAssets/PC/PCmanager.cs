// PCのだいたいの動きを管理するスクリプト。

using System.Collections.Generic;
using UnityEngine;

public class PCmanager : MonoBehaviour
{
    [SerializeField] public List<PCFollower> _followers; // PCの追従するオブジェクトのリスト

    // _followersにオブジェクトを追加するメソッド。
    public void AddFollower(PCFollower follower)
    {
        if (!_followers.Contains(follower))
        {
            _followers.Add(follower);
        }
    }
}
