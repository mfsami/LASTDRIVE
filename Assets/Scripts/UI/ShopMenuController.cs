using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class ShopMenuController : MonoBehaviour
{
    public Animator sign1Animator;
    public Animator sign2Animator;
    public Animator sign3Animator;

    private int money;

    private bool shopIsOpen = false;

    public Merchant merchant;

    public GunController weapon;

    public GameObject buttons;

    public Bullet bullet;


    public PlayerData playerData;

    // Weapons
    public SpriteRenderer weaponSpriteRenderer;
    public Sprite akSprite;
    public Sprite shotgunSprite;
    public Sprite sniperSprite;

    //Weapon stats
    private float akRate = 0.17f;
    private float akDmg = 5;

    private float shotRate = 1.5f;
    private float shotDmg = 10;

    private float sniperRate = 2f;
    private float sniperDmg = 20;



    // Shop prices

    public int akPrice = 20;
    public int shotgunPrice = 50;
    public int sniperPrice = 100;


    //public GameObject shopMenuParent;


    void Update()
    {

        if (merchant == null)
        {
            Debug.LogWarning("Merchant is NULL!");
            return;
        }

        if (Input.GetKeyDown(KeyCode.E) && merchant.playerInRange)
        {
            // Shop is closed and player in range of merchant
            if (!shopIsOpen)
            {
                //shopMenuParent.SetActive(true);
                OpenShop();
            }
            
            else 
            {
                //shopMenuParent.SetActive(false);
                CloseShop();
            }
        }

        // Shop is open, Player outside of range
        if (shopIsOpen &&  merchant.playerInRange == false)
        {
            CloseShop();
        }

    }

    private void OpenShop()
    {
        
        sign1Animator.SetTrigger("Drop");
        sign2Animator.SetTrigger("Drop");
        sign3Animator.SetTrigger("Drop");
        shopIsOpen = true;

        // Disable player input
        weapon.enabled = false;
        buttons.SetActive(true);
    }

    private void CloseShop()
    {
        sign1Animator.SetTrigger("Rise");
        sign2Animator.SetTrigger("Rise");
        sign3Animator.SetTrigger("Rise");
        shopIsOpen = false;

        // Re-enable player input
        weapon.enabled = true;
        buttons.SetActive(false);
    }

    public void OnBuyAkPressed()
    {
        if (playerData.money >= akPrice)
        {
            playerData.money -= akPrice;
            weaponSpriteRenderer.sprite = akSprite;

            // Change (fireRate, bulletSpeed)
            weapon.SetGunStats(akRate, 60f);
            bullet.SetGunDmg(akDmg);
            

            Debug.Log("AK PURCHASED");
            CloseShop();
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }

    public void OnBuyShotgunPressed()
    {
        if (playerData.money >= shotgunPrice)
        {
            playerData.money -= shotgunPrice;
            weaponSpriteRenderer.sprite = shotgunSprite;

            // Change (fireRate, bulletSpeed)
            weapon.SetGunStats(shotRate, 60f);
            bullet.SetGunDmg(shotDmg);

            Debug.Log("SHOTGUN PURCHASED");
            CloseShop();
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }

    public void OnSniperPressed()
    {
        if (playerData.money >= sniperPrice)
        {
            playerData.money -= sniperPrice;
            weaponSpriteRenderer.sprite = sniperSprite;

            // Change (fireRate, bulletSpeed)
            weapon.SetGunStats(sniperRate, 60f);
            bullet.SetGunDmg(sniperDmg);

            Debug.Log("SNIPER PURCHASED");
            CloseShop();
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }

}

