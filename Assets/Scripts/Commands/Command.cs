
public abstract class Command 
{
    protected IPlayer Player;
    

    public Command(IPlayer _player)
    {
        Player = _player;
    }

    public abstract void Execute();

}
