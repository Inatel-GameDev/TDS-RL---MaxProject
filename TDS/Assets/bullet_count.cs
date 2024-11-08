using UnityEngine;
using UnityEngine.UI;

public class GunDisplay : MonoBehaviour
{
    public Gun gun;
    public GameObject emptyBulletPrefab;
    public GameObject fullBulletPrefab;
    public Transform bulletsContainer;
    private void Start()
    {
        DisplayBullets();
    }

    private void DisplayBullets()
    {
        foreach (Transform child in bulletsContainer)
        {
            Destroy(child.gameObject);
        }

        Vector3 startPosition = new Vector3(0, -10, 0);

        for (int i = 0; i < gun.maxMag; i++)
        {
            Vector3 bulletPosition = startPosition + new Vector3(i * 410, 0, 0);

            GameObject emptyBullet = Instantiate(emptyBulletPrefab, bulletsContainer);
            RectTransform emptyBulletRect = emptyBullet.GetComponent<RectTransform>();

            if (emptyBulletRect != null)
            {
                emptyBulletRect.anchoredPosition = bulletPosition;
            }
            if (i < gun.currentMag)
            {
                GameObject fullBullet = Instantiate(fullBulletPrefab, bulletsContainer);
                RectTransform fullBulletRect = fullBullet.GetComponent<RectTransform>();

                if (fullBulletRect != null)
                {
                    fullBulletRect.anchoredPosition = bulletPosition;
                }
            }
        }
    }
    private void FixedUpdate()
    {
        DisplayBullets();
    }
    }
