using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISoldierState 
{
    void Enter(Soldier soldier);
    void Update(Soldier soldier);
    void Exit(Soldier soldier);
}
