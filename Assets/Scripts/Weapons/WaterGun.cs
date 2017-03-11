using UnityEngine;

public class WaterGun : BaseWeapon, IWeapon {
    // String to the weapon pickup that should spawn when throwing away the weapon
    //public string WeaponPickupSlug { get; set; }

    public ParticleSystem particles;
    public bool isPlaying;
    public float maxVolume = 1f;
    public float maxPitch = 3f;
    public float fadeSpeed = 0.01f;

    private const int PLAYER_COLOR_INDEX = 8;

    private Color m_Color;
    private Color m_EmissionColor;

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
        //unused stuff: --- kolla PlayerInput? --- *Time.deltaTime

        // set pitch and volume of sound effect based on the amount of particles active from WaterGun
        if ((particles.particleCount < 100) && (particles.particleCount > 0) && isPlaying)
        {

                audioSource.volume = particles.particleCount*fadeSpeed;
                audioSource.pitch = 1 + 2*(particles.particleCount * fadeSpeed);
        }
        //if it's more than 100 aprticles - just set  the volume and pitch to max
        else if ((particles.particleCount > 100) && isPlaying)
        {

            audioSource.volume = maxVolume;
            audioSource.pitch = maxPitch;
        }
        //if 0 active particles, stop audio source and set "isPlaying" to false so that a new loop can be started in Attack-function
        else if (particles.particleCount == 0)
        {
            audioSource.Stop();
            isPlaying = false;
        }
    }

    //Start emitting particles and play sound effect
    public new void Attack(Vector3 dir) {
        particles.Emit(1);

        if (!isPlaying) {
            audioSource.Play();
            isPlaying = true;
        }

    }


    // Sets the correct placement for this weapon.
    public override void SetUp(Material playerColorMaterial) {
        transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        transform.localPosition = new Vector3(0.001f, 0f, 0.085f);

        // Set the weapons highlighted color to the player color
        GetComponent<Renderer>().materials[PLAYER_COLOR_INDEX].color = playerColorMaterial.color;
        GetComponent<Renderer>().materials[PLAYER_COLOR_INDEX].SetColor("_EmissionColor", playerColorMaterial.GetColor("_EmissionColor"));

        m_Color = playerColorMaterial.color;
        m_EmissionColor = playerColorMaterial.GetColor("_EmissionColor");

        particles.startColor = m_Color;
        //ParticleSystem.MainModule psMain = particles.main;
        //psMain.startColor = m_Color;
        particles.GetComponent<ParticleDamage>().PlayerColor = m_Color;
        //particles.GetComponent<Renderer>().material.color = m_Color;


        var rot = new Vector3(57.766f, 90.79201f, 270.044f);
        transform.localRotation = Quaternion.Euler(rot);
    }

}
