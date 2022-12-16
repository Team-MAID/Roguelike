using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] 
    private GameObject projectile;
    GameObject _player;
    private float angleOffset;
    public int attackDelay;
    bool firedArrow;

    private void Start()
    {
        firedArrow = false;
        angleOffset = -45;
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -135));
        _player = GameObject.FindWithTag("Player");
        _player.GetComponent<playerStats>().setAttackDamageWithWeapon(projectile.GetComponent<Arrow>().GetDamage());
    }
    void Update()
    {
        Rotate();

        if (Input.GetMouseButtonDown(0) && firedArrow == false)
        {
            firedArrow=true;
            StartCoroutine(FireArrow());
        }
    }

    IEnumerator FireArrow()
    {
        Quaternion _deltaRot = Quaternion.Euler(0, 0, -135);
        Instantiate(projectile, this.transform.position, this.transform.rotation * _deltaRot);
        yield return new WaitForSeconds(.5f);
        firedArrow = false;
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
