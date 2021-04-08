using UnityEngine;

namespace Models
{
    public abstract class Building : MonoBehaviour
    {
        [SerializeField] protected int buildTimeInSeconds;
        public int BuildTimeInSeconds => buildTimeInSeconds;

        public abstract void OnBuild();
    }
}