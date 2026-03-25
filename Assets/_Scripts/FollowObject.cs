using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private float cameraMoveSpeed;
    [SerializeField] private Vector3 cameraOffset;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Player.instance.transform.position + cameraOffset, cameraMoveSpeed * Time.deltaTime);
    }
}