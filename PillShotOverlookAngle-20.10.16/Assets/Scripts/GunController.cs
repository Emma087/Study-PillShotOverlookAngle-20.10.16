using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GunController : MonoBehaviour
{
    public Transform weaponHold;//这个是生成枪预制体的位置保存游戏对象，前面挂了那个空对象
    private Gun equippedGun;
    public Gun startingGun;
    // Start is called before the first frame update
    void Start()
    {
        if (startingGun != null)
        {
            EquipGun(startingGun);
        }
    }
    
  

    public void EquipGun(Gun gunToEquip)
    {
        if (equippedGun != null)
        {
            Destroy(equippedGun.gameObject);
        }

        equippedGun = Instantiate(gunToEquip, weaponHold.position, weaponHold.rotation) as Gun;
        equippedGun.transform.parent = weaponHold;
        equippedGun.transform.localPosition = Vector3.zero;
    }

    
    
    
    

    // Update is called once per frame
    void Update()
    {

    }
}