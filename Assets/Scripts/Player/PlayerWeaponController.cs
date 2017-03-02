using UnityEngine;

public class PlayerWeaponController : MonoBehaviour {

    public GameObject PlayerHand;
    public GameObject EquippedWeapon { get; set; }

    private Material m_PlayerColorMaterial;

    private IWeapon m_InstancedWeapon;

	// Use this for initialization
    private void Start () {
        // Get the players color material
        m_PlayerColorMaterial = GetComponentInChildren<Renderer>().materials[3];
    }

    public void EquipWeapon(Item weaponToEquip) {
        if (EquippedWeapon != null) {
            WeaponThrow();
            //Destroy(PlayerHand.transform.GetChild(0).gameObject);
        }

        EquippedWeapon = Instantiate(Resources.Load<GameObject>("Weapons/" + weaponToEquip.ObjectSlug));

        m_InstancedWeapon = EquippedWeapon.GetComponent<IWeapon>();
        EquippedWeapon.transform.SetParent(PlayerHand.transform);
        EquippedWeapon.GetComponent<BaseWeapon>().SetUp(m_PlayerColorMaterial);
        //EquippedWeapon.transform.localPosition = Vector3.zero;
    }

    public void PerformWeaponAttack() {
        if (m_InstancedWeapon == null) return;
        m_InstancedWeapon.Attack(gameObject.transform.forward);
    }

    public void PerformRangedAttack() {
    }

    public void WeaponThrow() {
        if (EquippedWeapon == null) return;
        var posToSpawn = PlayerHand.transform.position;
        posToSpawn.y += 1;
        var pickupThrow =
            Instantiate(Resources.Load<GameObject>("Pickups/Weapons/" + m_InstancedWeapon.WeaponPickupSlug), posToSpawn, Random.rotation);

        pickupThrow.GetComponent<Interactable>().SetPickupPlayerColor(m_PlayerColorMaterial);
        pickupThrow.GetComponent<Rigidbody>().AddForce(transform.forward * 100);
        Destroy(PlayerHand.transform.GetChild(0).gameObject);
    }
}
