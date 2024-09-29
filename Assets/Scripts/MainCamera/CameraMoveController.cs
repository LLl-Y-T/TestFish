using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveController : MonoBehaviour
{
    [SerializeField] private LevelController _player;
    [SerializeField] private Vector3 _startOffset;
    [SerializeField] private float _followSpeed = 10f;
    private float RatePosZ = -1, RatePosY = 0.3f;
    private Vector3 _offset;

    private void Awake()
    {
        _player.NewLevelEvent.AddListener(NewOffset);
    }
    void LateUpdate()
    {
        Vector3 targetPosition = _player.transform.position + _player.transform.rotation * _offset;

        transform.position = Vector3.Lerp(transform.position, targetPosition, _followSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, _player.transform.rotation, _followSpeed);
    }

    private void NewOffset(int Level)
    {
        _offset = _startOffset + (new Vector3(0, RatePosY , RatePosZ) * Level);
    }

}
