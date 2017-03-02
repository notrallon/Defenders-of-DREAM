using UnityEngine;

public class BaseWeapon : MonoBehaviour, IWeapon, IProjectileWeapon {
    public Transform ProjectileSpawn { get; set; }
    public string WeaponPickupSlug { get; set; }

    //set the Emitter
    public GameObject ProjectileEmitter;

    //set projectile in Unity
    public GameObject Projectile;

    //set velocity in Unity
    public float ProjectileVelocity;

    // cooldown variables
    private float m_NextFire;
    public float FireRate = 1;

    //Audio Effects
    public AudioClip shootSound;
    protected AudioSource audioSource;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;

    private Color m_Color;
    private Color m_EmissionColor;

    // Use this for initialization
    private void Start () {
		WeaponPickupSlug = "WaterBalloonGun_Pickup";

        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
    private void Update () {

    }

    public void Attack(Vector3 dir) {
        if (!(m_NextFire < Time.time)) return;

        //Play shootSound
        float vol = Random.Range(volLowRange, volHighRange);
        audioSource.PlayOneShot(shootSound, vol);

        // Instantiate projectile
        var temporaryProjectile = Instantiate(Projectile, ProjectileEmitter.transform.position, ProjectileEmitter.transform.rotation);

        temporaryProjectile.GetComponent<Renderer>().material.color = m_Color;
        temporaryProjectile.GetComponent<Renderer>().material.SetColor("_EmissionColor", m_EmissionColor);

        //Projectiles may appear rotated  incorrectly due to the way its pivot was set from original model
        //Corrected here if needed:
        temporaryProjectile.transform.Rotate(Vector3.left * Random.Range(0, 360));

        //Retrieve Rigidbody from instantiated projectile and control it
        var temporaryRb = temporaryProjectile.GetComponent<Rigidbody>();

        //Give the projectile a velocity
        temporaryRb.AddForce(dir * ProjectileVelocity);

        //Destroy projectile after 2 sec
        Destroy(temporaryProjectile, 2.0f);

        m_NextFire = Time.time + FireRate;
    }

    public void ShootProjectile() {
        throw new System.NotImplementedException();
    }

    public virtual void SetUp(Material playerColorMaterial) {
        transform.localPosition = new Vector3(-0.098f, 0.038f, 0.087f);
        var rot = new Vector3(-8.337001f, 177.759f, 149.026f);
        transform.localRotation = Quaternion.Euler(rot);

        // Set the weapons highlighted color to the player color
        GetComponent<Renderer>().materials[2].color = playerColorMaterial.color;
        GetComponent<Renderer>().materials[2].SetColor("_EmissionColor", playerColorMaterial.GetColor("_EmissionColor"));

        m_Color = playerColorMaterial.color;
        m_EmissionColor = playerColorMaterial.GetColor("_EmissionColor");
    }

}
