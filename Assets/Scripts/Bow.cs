using InventorySystem.UI;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using InventorySystem.Interfaces;
using static UnityEngine.GraphicsBuffer;

public class Bow : MonoBehaviour
{
    [SerializeField] 
    private GameObject projectile;
    GameObject _player;
    private float angleOffset;
    public int attackDelay;
    private float _AttackDelay;

    private void Start()
    {     
        _AttackDelay = attackDelay;
        angleOffset = -45;
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -135));
        _player = GameObject.FindWithTag("Player");
        _player.GetComponent<playerStats>().setAttackDamageWithWeapon(projectile.GetComponent<Arrow>().GetDamage());
    }
    void Update()
    {
        Rotate();

        if (_AttackDelay > 0)
        {
            _AttackDelay--;
        }
        if (Input.GetMouseButtonDown(0) && _AttackDelay <= 0)
        {
            Quaternion _deltaRot = Quaternion.Euler(0, 0, -135);
            Instantiate(projectile, this.transform.position, this.transform.rotation * _deltaRot);
            _AttackDelay = attackDelay;
        }
    }

    private void Rotate()
    {
        Vector3 _mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 _target;
        Vector3 _deltaRot;
        float   _angle;

        _target = _mPos - this.transform.position;
        _angle = Mathf.Atan2(_target.x, _target.y) * Mathf.Rad2Deg;
        _deltaRot = new Vector3(0, 0, -_angle + angleOffset);

        this.transform.rotation = Quaternion.Euler(_deltaRot);
    }
}
