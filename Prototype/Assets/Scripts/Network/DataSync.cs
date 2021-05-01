using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSync : MonoBehaviour
{
    
    private List<float> times = new List<float>();
    public float averagedTime = 0.0f;

    public void AddTime(float time) => times.Add(time);
    public IEnumerator CalculateAverageTime()
    {
        
        
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            float totalTime = 0.0f;
            foreach (var t in times) totalTime += t;
            averagedTime = totalTime / times.Count;
            times.Clear();
            if(!float.IsNaN(averagedTime)) SendTimeToOtherPlayers();
        }
    }

    void SendTimeToOtherPlayers() =>
        RealTimeInstance.Instance._testStringSync.SetMessage(TestStringSync.MessageTypes.AVERAGED_TIME + averagedTime);

}
