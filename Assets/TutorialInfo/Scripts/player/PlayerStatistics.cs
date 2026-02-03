public class PlayerStatistics
{
    public int Health = 3;

    public float Range = 4f;

    public float Speed = 10f;

    public int Damages = 10;

    public void TakeDamages()
    {
        Health -= 1;
    }

    // TODO: Faire logique pour fin de partie quand 0 pv restant
}