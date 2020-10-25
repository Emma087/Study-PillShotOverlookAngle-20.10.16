using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]
//这句是强制附加另一个脚本 PlayerController，在引擎 inspector 窗口强制添加
public class Player : LivingEntity
{
    public float moveSpeed;
    public GameObject aim;
    private Camera viewCamera;
    private PlayerController controller;
    private GunController gunController;

    // Start is called before the first frame update
    protected override void Start()
    {
        Debug.Log(LivingEntity.testSV);
        base.Start();
        controller = GetComponent<PlayerController>();
        viewCamera = Camera.main;
        gunController = GetComponent<GunController>();
    }

    /// <summary>
    /// 用户控制游戏主角的行走移动
    /// </summary>
    void MovementInput()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        //.normalized 是使单位向量指向该方向的操作
        controller.Move(moveVelocity);
    }

    /// <summary>
    /// 一个是相机射线显示，一个是游戏主角的脸朝向，还有一个射线和地面接触的标志显示
    /// </summary>
    void LookInput()
    {
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, point, Color.cyan);
            controller.LookAt(point);
            aim.transform.position = point + Vector3.up * 0.001f;
        }
    }

    void WeaponInput()
    {
        if (Input.GetMouseButton(0))
        {
            gunController.Shoot();
        }
    }

    // Update is called once per frame
    void Update()
    {
        MovementInput();
        LookInput();
        WeaponInput();
    }
}