using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public interface IBattleController {
    BattleEntityManagerComponent entityManager { get; }
    PCTurnManagerComponent turnManager { get; }
 }

