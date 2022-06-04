using UnityEngine;

public class CameraMoveToPlayer : MonoBehaviour
{
    [SerializeField] private PlayerMoveHandler _player;
    private float _damping = 0.1f;
    private float _offsetX = 2f;
    private float _minPosX = -0.02020603f;
    private float _maxPosX = 22.496f;

    void Start()
    {
        FindPlayer(_player.IsPlayerFlipX);
    }

    public void FindPlayer(bool playerFaceLeft)
    {
        if (!playerFaceLeft)
        {
            transform.position = new Vector3(_player.transform.position.x - _offsetX, 0, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(_player.transform.position.x + _offsetX, 0, transform.position.z);
        }
    }

    void FixedUpdate()
    {
        if(!IsPlayerOnEdge())
        {
            MoveCameraToPlayer();
        }
    }

    private bool IsPlayerOnEdge()
    {
        if (_player.transform.position.x >= _maxPosX || _player.transform.position.x <= _minPosX) return true;
        return false;
    }

    private void MoveCameraToPlayer()
    {
        Vector3 target;
        float y = _player.IsPlayerJump ? 0.5f : 0f;
        if (!_player.IsPlayerFlipX)
        {
            target = new Vector3(_player.transform.position.x - _offsetX, y, transform.position.z);
        }
        else
        {
            target = new Vector3(_player.transform.position.x + _offsetX, y, transform.position.z);
        }
        transform.position = Vector3.Lerp(transform.position, target, _damping);
    }
}
