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
        if (!waitForSeconds.TryGetValue(seconds, out wfs)) // 딕셔너리에 seconds와 일치하는 키값이 없으면,
            waitForSeconds.Add(seconds, wfs = new WaitForSeconds(seconds)); // 신규 데이터로 추가.
        return wfs;
    }
}
