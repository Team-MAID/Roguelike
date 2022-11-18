using JetBrains.Annotations;
using UnityEngine;

public class BowController : WeaponItem
{
    [SerializeField] private GameObject projectile;
    public bool equipped;

    private void Start()
    {
        if (transform.parent != null && transform.parent.tag == "Player")
        { 
            equipped = true; 
            this.transform.GetComponent<SpriteRenderer>().flipX=true;
            this.transform.GetComponent<SpriteRenderer>().flipY = true;
        }
        else { equipped = false;}
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && equipped == true )
        {
            //Debug.Log("LeftClick");
            Instantiate(projectile, this.transform.position, Quaternion.identity);
        }
    }

    public override void Equip(ItemData data)
    {      
        GameObject player = GameObject.FindWithTag("Player");
        BowController bow = player.GetComponentInChildren<BowController>();

        Vector3 offset = new Vector3(0.7f,0,0);

        if (bow == null)
        {
            Instantiate(this, player.transform.position + offset, Quaternion.identity, player.transform);
            Debug.Log("Bow Equipped");
        }
        else
        {
            Destroy(bow.gameObject);
            Debug.Log("Bow Un-Equipped");
        }
    }
}
