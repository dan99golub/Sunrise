using UnityEngine;

public class Game
{
    public static InputService InputService;

    public Game()
    {
        if (Application.isEditor)
            InputService = new StandaloneInputService();
        else
            InputService = new MobileInputService();
    }
}