using System;
using Player;
using UnityEngine;

public class TeleportPlayerAtSpawn : MonoBehaviour
{
    [SerializeField] private Vector3 _startPosition;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerMovements>() == null)
            return;

        other.transform.position = _startPosition;
    }
}
