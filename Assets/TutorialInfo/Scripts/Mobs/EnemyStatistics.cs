using Unity.VisualScripting;

public class EnemyStatistics
{
    public float Speed;

    public int Health;

    public int MaxHealth;

    public void TakeDamages(int playerDamages)
    {
        Health -= playerDamages;
    }

    public bool IsEnemyDead()
    {
        return (Health <= 0);
    }
}