using Zenject;

public class MazeSceneInstaller :MonoInstaller<MazeSceneInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<CoroutineController>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
    }
}
