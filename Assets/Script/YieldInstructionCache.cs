using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

internal static class YieldInstructionCache
{ 
    private static readonly Dictionary<float, WaitForSeconds> waitForSeconds = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds WaitForSeconds(float seconds)
    {
        WaitForSeconds wfs;
        if (!waitForSeconds.TryGetValue(seconds, out wfs)) // ��ųʸ��� seconds�� ��ġ�ϴ� Ű���� ������,
            waitForSeconds.Add(seconds, wfs = new WaitForSeconds(seconds)); // �ű� �����ͷ� �߰�.
        return wfs;
    }
}
