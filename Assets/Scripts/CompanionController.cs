using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionController : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    public int          maxHealth;
    public int          attackDelay;
    public int          movementSpeed;
    public float        standOffDistance;
    private int         angleOffset;
    private int         health;
    private bool        active;
    public float        wiggleRoom;
    private float       cAttackDelay;
    private float       mVecMag;
    private Vector2     mVector;
    private GameObject  player;
    private Quaternion  projectileAngle;
    private Rigidbody2D rB;
    private Rigidbody2D rBP;

    void Start()
    {
        Physics2D.IgnoreLayerCollision(6,6);
        player = GameObject.Find("Player").gameObject;
        rB = GetComponent<Rigidbody2D>();
        rBP = player.GetComponent<Rigidbody2D>();   

        cAttackDelay= attackDelay;
        health = maxHealth;
        active = true;
    }

    
    void Update()
    {

        if (!active && health > 0) 
        {   
            active = true; 
        }

        if (player != null ) 
        {
            mVector = rBP.position - rB.position;
            mVecMag = mVector.magnitude;
            mVector.Normalize();

            if (mVecMag < standOffDistance - wiggleRoom)
            {
                rB.MovePosition(rB.position + (mVector * movementSpeed * Time.fixedDeltaTime) * -1);
            }
            else if (mVecMag > standOffDistance + wiggleRoom)
            {
                rB.MovePosition(rB.position + (mVector * movementSpeed * Time.fixedDeltaTime));
            }


            Vector2 movement = rBP.position - rB.position;
            movement = movement.normalized;

            if (mVector.x > 0)
            {
                Vector3 l_scale = gameObject.transform.localScale;
                gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            }
            else if (mVector.x < 0)
            {
                Vector3 l_scale = gameObject.transform.localScale;
                gameObject.transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
            }

            if (cAttackDelay > 0)
            { 
                cAttackDelay--; 
            }

            if (Input.GetMouseButtonDown(0) && cAttackDelay <= 0 && active)
            {
                Rotate();
                Instantiate(projectile, this.transform.position, projectileAngle);
                cAttackDelay = attackDelay;
            }
        }
    }

    private void Rotate()
    {
        Vector3 _mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 _target;
        Vector3 _deltaRot;
        float _angle;

        _target = _mPos - this.transform.position;
        _angle = Mathf.Atan2(_target.x, _target.y) * Mathf.Rad2Deg;
        _deltaRot = new Vector3(0, 0, -_angle + angleOffset);

        projectileAngle = Quaternion.Euler(_deltaRot);
    }

    public void DecreaseHealthByDamage(int damage)
    {
        if (health > 0)
        {
            health -= damage;
            if (health < 0)
            { health = 0; }
        }
    }

    void HealFull()
    {
        health = maxHealth;
    }

    void Heal(int heal)
    {
        health += heal;

        if (health > maxHealth)
        {
            health = maxHealth; 
        }
    }
}
