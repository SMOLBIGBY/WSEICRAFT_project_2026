using UnityEngine;

public class PlayerCanvas : MonoBehaviour
{
    Player player;
    void Start()
    {
        player = FindAnyObjectByType<Player>();
    }

    void Update()
    {
       transform.position =  player.transform.position;
    }
}
