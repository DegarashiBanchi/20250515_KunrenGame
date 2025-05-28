// PCのだいたいの動きを管理するスクリプト。

using System.Collections.Generic;
using R3;
using UnityEngine;

public class PCmanager : MonoBehaviour
{
    [SerializeField] public List<GameObject> _followers; // PCの追従するオブジェクトのリスト
    [SerializeField] SO_MaskStatus _maskStatus; // マスクステータス情報のScriptableObject

    private void Start()
    {
        // PCの向きを監視し、変化があれば自分の方向を更新する。
        _maskStatus._currentPCDirection.Subscribe(dir
            => transform.up = _maskStatus._currentPCDirection.Value.normalized
            ).AddTo(this);
    }

    // _followersにオブジェクトを追加するメソッド。
    public void AddFollower(GameObject follower)
    {
        if (!_followers.Contains(follower))
        {
            _followers.Add(follower);
        }
    }



    // _followersからオブジェクトを削除するメソッド。
    public void RemoveFollower(GameObject follower)
    {
        if (_followers.Contains(follower))
        {
            _followers.Remove(follower);
        }
    }

    private void FixedUpdate()
    {
        // 位置情報を更新
        UpdateCurrentPCPos();
    }

    // _currentPCPosに自分の現在座標を書き込むメソッド。
    public void UpdateCurrentPCPos()
    {
        _maskStatus._currentPCPos.Value = new Vector2(transform.position.x, transform.position.y);
    }
}
