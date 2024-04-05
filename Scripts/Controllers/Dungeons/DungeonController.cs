using System;
using System.Collections;
using UnityEngine;

public abstract class DungeonController : MonoBehaviour
{
    public event Action<float> OnTimeUpdate;
    public event Action OnDungeonEnd;

    public abstract void StartDungeon();

    public abstract void EndDungeon();

    public void StartTimer(float time)
    {
        StartCoroutine(COTimer(time));
    }

    private IEnumerator COTimer(float time)
    {
        float curTime = time;

        while (curTime > 0)
        {
            curTime -= Time.deltaTime;
            if (curTime < 0) curTime = 0;
            OnTimeUpdate?.Invoke(curTime);
            yield return null;
        }

        EndDungeon();
    }
}
