using UnityEngine;

namespace Features.GameTagExtension
{
    [CreateAssetMenu(fileName = "GameTag", menuName = "ScriptableObjects/GameTag", order = 1)]
    public class GameTag : ScriptableObject
    {
        [SerializeField] private string m_gameTagName;

        public string GameTagName
        {
            get => m_gameTagName;
            set => m_gameTagName = value;
        }
    }
}