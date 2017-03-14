using UnityEngine;

public class WaterGun_TopTank : BaseWeapon, IWeapon
{
    // String to the weapon pickup that should spawn when throwing away the weapon
    //public string WeaponPickupSlug { get; set; }

    //public ParticleSystem particles;
    public bool isPlaying;
    public float maxVolume = 1f;
    public float maxPitch = 3f;
    public float fadeSpeed = 0.01f;

    public GameObject[] ProjectileEmitters;

    private float m_NextFire;

    private float volLowRange = .5f;
    private float volHighRange = 1.0f;

    private const int PLAYER_COLOR_INDEX = 2;

    private Color m_Color;
    private Color m_EmissionColor;

    // Use this for initialization
    private void Start()
    {
        WeaponPickupSlug = "WaterGun_TopTank_Pickup";
        audioSource = GetComponent<AudioSource>();
        isPlaying = false;


        //particles.transform.position = ProjectileEmitter.transform.position;
        //particles.enableEmission = false;
        //particles.Stop();
        //particles.enableEmission() = false;
    }

    private void Update()
    {

    }

    //Start emitting particles and play sound effect
    public new void Attack(Vector3 dir)
    {
        if (!(m_NextFire < Time.time)) return;

        //Play shootSound
        float vol = Random.Range(volLowRange, volHighRange);
        audioSource.PlayOneShot(shootSound, vol);

        for (int i = 0; i < ProjectileEmitters.Length; i++)
        {
            // Instantiate projectile
            var temporaryProjectile = Instantiate(Projectile, ProjectileEmitters[i].transform.position, ProjectileEmitters[i].transform.rotation);

            temporaryProjectile.GetComponent<Renderer>().material.color = m_Color;
            temporaryProjectile.GetComponent<Renderer>().material.SetColor("_EmissionColor", m_EmissionColor);

            //Projectiles may appear rotated  incorrectly due to the way its pivot was set from original model
            //Corrected here if needed:
            temporaryProjectile.transform.Rotate(Vector3.left * Random.Range(0, 360));
            temporaryProjectile.GetComponent<ProjectileDamage>().SetParticleController(GetComponentInParent<PlayerInput>().PlayerParticleSystem.GetComponent<ParticleController>());

            //Retrieve Rigidbody from instantiated projectile and control it
            var temporaryRb = temporaryProjectile.GetComponent<Rigidbody>();

            //Give the projectile a velocity
            temporaryRb.AddForce(dir * ProjectileVelocity);

            //Destroy projectile after 2 sec
            Destroy(temporaryProjectile, 2.0f);
        }


        m_NextFire = Time.time + FireRate;
    }


    // Sets the correct placement for this weapon.
    public override void SetUp(Material playerColorMaterial)
    {
        //Scale & position of the weapon
        transform.localScale = new Vector3(1f, 1f, 1f);
        transform.localPosition = new Vector3(0.059f, -0.066f, 0.102f);

        //Rotation of the weapon
        var rot = new Vector3(180.345f, 5.884995f, -32.29102f);
        transform.localRotation = Quaternion.Euler(rot);

        // Set the weapons highlighted color to the player color
        GetComponent<Renderer>().materials[PLAYER_COLOR_INDEX].color = playerColorMaterial.color;
        GetComponent<Renderer>().materials[PLAYER_COLOR_INDEX].SetColor("_EmissionColor", playerColorMaterial.GetColor("_EmissionColor"));

        m_Color = playerColorMaterial.color;
        m_EmissionColor = playerColorMaterial.GetColor("_EmissionColor");
    }

}
