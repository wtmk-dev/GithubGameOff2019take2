public class PlayerModel
{
    public float ShieldLevel;
    public float RechargeRateInSeconds;
    
    public float MoveSpeed;
    public bool ActiveTeam, CanTakeDamage, IsAlive;
    public int Strikes;

    public PlayerModel(int strikes, float shieldLevel,float rechargeRateInSeconds,float moveSpeed,bool activeTeam,bool candTakeDamge,bool isAlive)
    {   
        Strikes = strikes;
        ShieldLevel = shieldLevel; 
        RechargeRateInSeconds = rechargeRateInSeconds; 
        MoveSpeed = moveSpeed;
        ActiveTeam = activeTeam;
        CanTakeDamage = candTakeDamge;
        IsAlive = isAlive;
    }
}