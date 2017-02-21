using UnityEngine;

public class PlayerWeaponController : MonoBehaviour {

    public GameObject PlayerHand;
    public GameObject EquippedWeapon { get; set; }

    private IWeapon m_InstancedWeapon;

	// Use this for initialization
    private void Start () {
		
	}

    public void EquipWeapon(Item weaponToEquip) {
        if (EquippedWeapon != null) {
            Destroy(PlayerHand.transform.GetChild(0).gameObject);
        }

        EquippedWeapon = Instantiate(Resources.Load<GameObject>("Weapons/" + weaponToEquip.ObjectSlug));

        m_InstancedWeapon = EquippedWeapon.GetComponent<IWeapon>();
        EquippedWeapon.transform.SetParent(PlayerHand.transform);
        EquippedWeapon.transform.localPosition = Vector3.zero;
    }

    public void PerformWeaponAttack() {
        m_InstancedWeapon.Attack();
    }
}
