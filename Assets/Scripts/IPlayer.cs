using UnityEngine;

public interface IPlayer 
{
    float Speed { get;}
    float MaxSpeed { get;}
    float Jumpforce { get;}
    
    Rigidbody2D rigidbody { get;}
    Transform transform { get; }
   
}
