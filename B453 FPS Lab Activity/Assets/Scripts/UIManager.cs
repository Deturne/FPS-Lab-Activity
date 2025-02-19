using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private TextMeshProUGUI ammoText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        ammoText = GameObject.Find("Ammo Count Text").GetComponent<TextMeshProUGUI>();
    }

    public void UpdateAmmoUI(int currentAmmo, int ammoCapacity)
    {
        ammoText.text = $"{currentAmmo}/{ ammoCapacity}";
    }
}
