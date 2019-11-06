using Zenject;


public class EnemyDieSignal
{
    public readonly BaseEnemy enemy;

    public EnemyDieSignal(BaseEnemy enemy)
    {
        this.enemy = enemy;
    }
}

public class EnemyFinishPathSignal
{
    public readonly BaseEnemy enemy;

    public EnemyFinishPathSignal(BaseEnemy enemy)
    {
        this.enemy = enemy;
    }
}

public class CellClickSignal
{
    public readonly Cell selectedCell;
    public CellClickSignal(Cell cell)
    {
        selectedCell = cell;
    }
}

public class StartGameSignal
{
    public StartGameSignal()
    {

    }
}