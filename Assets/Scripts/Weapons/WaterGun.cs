using UnityEngine;

public class WaterGun : BaseWeapon, IWeapon {
    // String to the weapon pickup that should spawn when throwing away the weapon
    //public string WeaponPickupSlug { get; set; }

    public ParticleSystem particles;
    public bool isPlaying;
    public float maxVolume = 1f;
    public float fadeSpeed = 0.02f; 

    // Use this for initialization
    private void Start () {
        WeaponPickupSlug = "WaterGun_Pickup";
        audioSource = GetComponent<AudioSource>();
        isPlaying = false;
        
        
        //particles.transform.position = ProjectileEmitter.transform.position;
        //particles.enableEmission = false;
        //particles.Stop();
        //particles.enableEmission() = false;
    }

    private void Update ()
    {
        //.particleCount
        // kolla player Input
        if ((particles.particleCount < 50) && (particles.particleCount > 0) && isPlaying)
        {

                audioSource.volume -= particles.particleCount*fadeSpeed*Time.deltaTime;
        }
        else if ((particles.particleCount > 50) && isPlaying)
        {

            audioSource.volume = maxVolume;
        }
        else if (particles.particleCount == 0)
        {
            audioSource.Stop();
            isPlaying = false;
        }
    }
	
    public void Attack(Vector3 dir) {
        particles.Emit(1);

        if (!isPlaying)
        {
            audioSource.Play();
            isPlaying = true;
        }

    }


    // Sets the correct placement for this weapon.
    public override void SetUp(Material playerColorMaterial) {
        transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        transform.localPosition = new Vector3(0.001f, 0f, 0.085f);
        var rot = new Vector3(57.766f, 90.79201f, 270.044f);
        transform.localRotation = Quaternion.Euler(rot);
    }

}
