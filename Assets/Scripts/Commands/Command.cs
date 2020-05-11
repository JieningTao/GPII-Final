
public abstract class Command 
{
    public IPlayer Player;
    

    public Command(IPlayer _player)
    {
        Player = _player;
    }

    public abstract void Execute();

}
