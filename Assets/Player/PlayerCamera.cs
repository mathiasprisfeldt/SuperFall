using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [field: SerializeField] public Player Player { get; set; }

    // Update is called once per frame
    void Update()
    {
        if(Player != null) {
            var position = transform.position;
            var playerPosition = Player.transform.position;
            position = new Vector3(playerPosition.x, playerPosition.y, position.z);

            transform.position = position;
        }
    }
}
