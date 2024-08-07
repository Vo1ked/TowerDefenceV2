using Zenject;

public class GameSignalsInstaller : Installer<GameSignalsInstaller>
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<EnemyDieSignal>();
        Container.DeclareSignal<EnemyFinishPathSignal>();
        Container.DeclareSignal<CellClickSignal>();
        Container.DeclareSignal<WaveChangedSignal>();
        Container.DeclareSignal<PauseSignal>();
    }
}
