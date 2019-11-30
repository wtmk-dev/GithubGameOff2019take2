using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TopDownPlayerMovement))]
public class PlayerKit : MonoBehaviour , InputGamePad
{
    [SerializeField]
    private Transform firePointL, firePointR;

    [SerializeField]
    private GameObject bullet, goShield;

    [SerializeField]
    private int primaryFirePoolSize, ammo, maxAmmo;

    private GameObjectPooler primaryFirePool;

    private int shotsOut;
    
    [SerializeField]
    private float primaryFireForce, shieldDeletionTime = 18f, 
                  shieldRechargeTime = 5f, shieldFullyDeletedTimer = 10f;
    
    private float currentShieldRechargeTime;

    private float SHIELD_MAX = 100f;

   
    private UnityEngine.InputSystem.Gamepad gamepad;
    private TopDownPlayerMovement movement;

    private Timer shieldRechargeTimer;

    public PlayerModel model;
    public Sprite primaryFireSprite;
    public bool isActive = false;

    public static int TOTAL_PLAYERS = 0;
    public int id = 0;

    //kit
    private BasicShield shield;
    private BasicRig rig;
    private BasicShot shot;

    //test code
    private bool alternateFire = false, cantFire = false, canShield = true;

    void Awake()
    {
        LogIn();

        shieldRechargeTimer = new Timer();
        shield = GetComponentInChildren<BasicShield>();
        rig = GetComponentInChildren<BasicRig>();

        movement = GetComponent<TopDownPlayerMovement>();

//TO:DO Refactor
        ammo = 6;
        maxAmmo = 6;
        shotsOut = 0;
//
        model = new PlayerModel(0, 100f, 100f, 2f, 10f, false, false, true);

        shield.SetModel(model);
        
        rig.SetModel(model);
        rig.SetId(id);
    }

    private void LogIn()
    {
        TOTAL_PLAYERS++;
        id = TOTAL_PLAYERS;
        
        Debug.Log("Dood im player: " + id);
    }

    void Update()
    {
        if(shotsOut == primaryFirePoolSize)
        {
            cantFire = true;
        }

        if(model == null)
        {
            return;
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

        if(!model.IsAlive)
        {
            gameObject.SetActive(false);
        }

    }

    private void HandleShield(float lb,float rb)
    {
        if(!canShield)
        {
            return;
        }

        bool lon = lb > 0 ? true : false;
        bool ron = rb > 0 ? true : false;

        if(model.ShieldLevel > SHIELD_MAX)
        {
            model.ShieldLevel = SHIELD_MAX;
        }

        if(!lon || !ron)
        {
            goShield.SetActive(false);
            
            if(!shieldRechargeTimer.IsLocked())
            {
                shieldRechargeTimer.SetLock(true);
                shieldRechargeTimer.SetTimer(shieldRechargeTime);

            } else if (shieldRechargeTimer.IsLocked())
            {
                shieldRechargeTimer.RecordTime(Time.fixedDeltaTime);
            }

            if(shieldRechargeTimer.IsDone())
            {
                Debug.Log("shield recharging");

                if(model.ShieldLevel < SHIELD_MAX)
                {
                    Debug.Log("shield recharging");
                    model.ShieldLevel += Time.fixedDeltaTime * shieldDeletionTime;
                }
            }
        }

        if(lon && ron && model.ShieldLevel > 0)
        {
            shieldRechargeTimer.SetLock(false);
            model.ShieldLevel -= Time.fixedDeltaTime * shieldDeletionTime;
            goShield.SetActive(true);
        }

        if(model.ShieldLevel <= 0)
        {
            goShield.SetActive(false);
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

    public bool IsActiveTeam()
    {
        return model.ActiveTeam;
    }

    public Transform GetTransform()
    {
        return gameObject.transform;
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
            pf.SetId(id);
            SpriteRenderer pfsr = clone.GetComponent<SpriteRenderer>();
            pfsr.sprite = primaryFireSprite;
            pf.pooler = primaryFirePool;
            primaryFirePool.SetPoolable(clone);
        }
    }

    




//model handeling
    public void SetActiveTeam(bool isActive)
    {
        model.ActiveTeam = isActive;
    }
}


    