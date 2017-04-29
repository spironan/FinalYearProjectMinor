using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProjectileBaseScript : MonoBehaviour
{
    bool canTimeout = true;
    bool isActive = true;
    bool canDamage;
    float lifetime;
    float pSpeed;
    uint damage;
    Vector2 pDireciton;
    Image pImage;
    Rigidbody2D rigidbody;
    GameObject owner;

	// Use this for initialization
    public virtual void Start() 
    {
        if (this.gameObject.GetComponent<Rigidbody2D>() == null)
            this.gameObject.AddComponent<Rigidbody2D>();
        if (this.gameObject.GetComponent<Collider2D>() == null)
            this.gameObject.AddComponent<Collider2D>();
        rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
	}
    
    public virtual void Init(float newLifetime, float newSpeed, uint newDamage, Vector2 newDireciton)
    {
        lifetime = newLifetime;
        pSpeed = newSpeed;
        damage = newDamage;
        pDireciton = newDireciton;

        if (damage <= 0)
            canDamage = false;
    }

	// Update is called once per frame
	public virtual void Update () 
    {
        UpdateProjectile();
        DestroyProjectile();
	}

    public virtual void UpdateProjectile()
    {
        rigidbody.AddForce(pSpeed * pDireciton);
    }

    public virtual void DestroyProjectile()
    {
        if (canTimeout)
        {
            lifetime -= Time.deltaTime;
            if (lifetime <= 0)
            {
                Destroy(this);
            }
        }
    }

    public virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject != owner && other.gameObject.tag == "Player")
        { 
            other.gameObject.GetComponent<CharacterBase>().TakeDamage(damage);
            Destroy(this);
        }
    }

}
