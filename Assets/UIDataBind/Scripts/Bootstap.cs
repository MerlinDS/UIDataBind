using UnityEngine;

namespace UIDataBind
{
    public static class Bootstap
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void Start()
        {
            Debug.Log("Started up");
        }
    }
}