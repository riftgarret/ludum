using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public interface IBattleController {
    BattleEntityManager entityManager { get; }
    TurnManager turnManager { get; }
 }

