using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TopDownPlayerMovement))]
public class PlayerKit : MonoBehaviour , InputGamePad
{

    [SerializeField]
    private Transform firePointL, firePointR;
    [SerializeField]
    private GameObject bullet,shield;
    [SerializeField]
    private int primaryFirePoolSize, ammo, maxAmmo;
    private int shotsOut;
    [SerializeField]
    private float primaryFireForce;

    private GameObjectPooler primaryFirePool;

    private UnityEngine.InputSystem.Gamepad gamepad;
    private TopDownPlayerMovement movement;

    public Sprite primaryFireSprite;
    public bool isActive = false;

    //test code
    private bool alternateFire = false, cantFire = false, canShield = true;

    void Awake()
    {
        movement = GetComponent<TopDownPlayerMovement>();

        ammo = 6;
        maxAmmo = 6;
        shotsOut = 0;

    }

    void Update()
    {
        if(shotsOut == primaryFirePoolSize)
        {
            cantFire = true;
        }

        if(gamepad == null)
        {
            return;
        }

        if(!isActive)
        {
            return;
        }

        var testfire = gamepad.rightTrigger.ReadValue();
        if(testfire > 0)
        {
            HandleFire();
        }

        var reload = gamepad.leftTrigger.ReadValue();
        if(reload > 0)
        {
            HandelReload();
        }

        var lb = gamepad.leftShoulder.ReadValue();
        var rb = gamepad.rightShoulder.ReadValue();

        HandleShield(lb,rb);
    }

    private void HandleShield(float lb,float rb)
    {
        if(!canShield)
        {
            return;
        }

        bool lon = lb > 0 ? true : false;
        bool ron = rb > 0 ? true : false;

        if(!lon || !ron)
        {
            shield.SetActive(false);
        }

        if(lon && ron)
        {
            shield.SetActive(true);
        }
    }

    private void HandelReload()
    {
        ammo = maxAmmo;
    }

    private void HandleFire()
    {
        if(ammo > 0 && !cantFire)
        {
            GameObject fire = primaryFirePool.GetPoolable();
            Transform spawnPoint = alternateFire ? firePointL : firePointR;
            fire.transform.SetPositionAndRotation(spawnPoint.position,spawnPoint.rotation);

            Rigidbody2D rb = fire.GetComponent<Rigidbody2D>();
            rb.AddForce(spawnPoint.up * primaryFireForce, ForceMode2D.Impulse);

            ammo--;
            shotsOut++;

            if (shotsOut % 2 == 0)
            {
                alternateFire = false;    
            }
            else
            {
                alternateFire = true;
            }
        }   
    }

    public void SetGamePad(UnityEngine.InputSystem.Gamepad gamepad, float defaultPos)
    {
        this.gamepad = gamepad;
        movement.Init(gamepad,defaultPos);
    }

    public void SetPrimaryFirePool(GameObjectPooler pool)
    {
        primaryFirePool = pool;
        LoadPrimaryFire();
    }

    private void LoadPrimaryFire()
    {
        primaryFirePool.Init();

        for(int i = 0; i < primaryFirePoolSize; i++)
        {
            Transform spawnPoint;

            if (i % 2 == 0)
            {
                spawnPoint = firePointL;    
            }
            else
            {
                spawnPoint = firePointR;
            }
            

            GameObject clone = Instantiate(bullet,spawnPoint.position,spawnPoint.rotation);
            BasicShot pf = clone.GetComponent<BasicShot>();
            SpriteRenderer pfsr = clone.GetComponent<SpriteRenderer>();
            pfsr.sprite = primaryFireSprite;
            pf.pooler = primaryFirePool;
            primaryFirePool.SetPoolable(clone);
        }
    }

    

}


    