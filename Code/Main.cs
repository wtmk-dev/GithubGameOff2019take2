using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Main : MonoBehaviour
{

    [SerializeField]
    private GameObject player;

    private EventHandler eventHandler;
    private List<GameObject> players;

    [SerializeField]
    private List<Vector3> playersStartingPos;
    [SerializeField]
    private List<float> playersRotation;
    [SerializeField]
    private List<Sprite> playerSkins, playerPrimaryFire, playerShield;
    [SerializeField]
    private List<GameObjectPooler> primaryFirePools;
    

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < Gamepad.all.Count; i++)
        {
            GameObject clone = Instantiate(player,playersStartingPos[i],Quaternion.identity);
            PlayerKit ipg = clone.GetComponent<PlayerKit>();

            ipg.SetGamePad(Gamepad.all[i],playersRotation[i]);
            Debug.Log(Gamepad.all[i].ToString());

            ipg.primaryFireSprite = playerPrimaryFire[i];
            ipg.SetPrimaryFirePool(primaryFirePools[i]);

            Skinner knife = clone.GetComponent<Skinner>();
            List<Sprite> sprites = new List<Sprite> { playerSkins[i], playerShield[i] };
            knife.Init(sprites);
        }
    }
}
