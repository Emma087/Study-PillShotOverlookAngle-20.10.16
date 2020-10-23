using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
//这句是强制附加另一个脚本 PlayerController，在引擎 inspector 窗口强制添加
public class Player : MonoBehaviour
{
    public float moveSpeed;
    public GameObject aim;
    private Camera viewCamera;
    private PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
        viewCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        //.normalized 是使单位向量指向该方向的操作
        controller.Move(moveVelocity);
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
}