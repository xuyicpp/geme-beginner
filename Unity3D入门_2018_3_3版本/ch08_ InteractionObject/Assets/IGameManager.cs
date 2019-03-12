public interface IGameManager
{
    ManagerStatus status { get; }   //需要定义的枚举

    void Startup();
}