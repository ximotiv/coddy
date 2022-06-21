using UnityEngine;

public class WeaponSelector : MonoBehaviour
{
    [SerializeField] private GameObject _weaponPrefab;
    [SerializeField] private PlayerData _player;

    public void OnPlayerClickButton()
    {
        _player.SetPlayerWeapon(_weaponPrefab);
    }
}
