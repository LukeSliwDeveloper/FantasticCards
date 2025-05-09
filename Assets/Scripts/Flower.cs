using UnityEngine;

public class Flower : MonoBehaviour
{
    [field: SerializeField] public FlowerType Type { get; private set; }
    [SerializeField] private GameObject ObjectInVase;
    [SerializeField] private GameObject ObjectInBouquete;

    private void Awake()
    {
        GameManager.Instance.OnFlowerMoved += ChangePlace;
    }

    private void ChangePlace(FlowerType type, bool isInBouquete)
    {
        if (Type == type)
        {
            if (isInBouquete)
            {
                ObjectInVase.SetActive(false);
                ObjectInBouquete.SetActive(true);
            }
            else
            {
                ObjectInVase.SetActive(true);
                ObjectInBouquete.SetActive(false);
            }
        }
    }
}
