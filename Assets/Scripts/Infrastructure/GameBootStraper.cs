using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class GameBootStraper : MonoBehaviour
    {
        private Game _game;

        private void Awake()
        {
            _game = new Game();

            DontDestroyOnLoad(this);
        }
    }
}