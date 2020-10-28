using System;
using System.Collections;
using System.Collections.Generic;

public static class Utility
{
    public static T[] ShuffleArray<T>(T[] array, int seed) //未指定类型的数组，我们写成通用类型 T
    {
        System.Random prng = new System.Random(seed);
        for (int i = 0; i < array.Length - 1; i++)
        {
            int randomIndex = prng.Next(i, array.Length);//.Next 返回的随机整数，去当索引使用
            T tempItem = array[randomIndex]; //临时数值
            array[randomIndex] = array[i];
            array[i] = tempItem;
        }

        return array;
    }
}