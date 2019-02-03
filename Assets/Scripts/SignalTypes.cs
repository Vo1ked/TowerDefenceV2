using Zenject;


public class EnemyDieSignal
{
    public readonly BaseEnemy enemy;

    public EnemyDieSignal(BaseEnemy enemy)
    {
        this.enemy = enemy;
    }
} 
