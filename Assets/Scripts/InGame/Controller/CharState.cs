
public interface CharState<T>
{
    void Enter(T e);

    void Excute(T e);

    void Exit(T e);
}
