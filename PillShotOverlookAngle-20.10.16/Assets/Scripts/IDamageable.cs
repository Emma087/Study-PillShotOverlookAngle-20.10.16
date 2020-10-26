using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable //接口（接口命名以 ...able 结尾）
{
    void TakeHit(float damage, RaycastHit hit); //接口不用写方法体
    void TakeDamage(float damage);
}