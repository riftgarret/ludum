namespace Redninja.BattleSystem.Combat.Operation.Result
{
    /// <summary>
    /// Evasion combat node that contains the result of the 'hit chance' resolving.
    /// </summary>
    public abstract class AccuracyEvasionLogic : BaseCombatLogic
    {    
        protected float accuracy;
        protected float evasion;
        protected float chanceToHit;
        protected float randomValue;

        public bool Hits { get { return chanceToHit > randomValue; } }

        public override string ToString()
        {
            return string.Format("[{5} s.acc:{0}, d.evas:{1}, cth:{2}, r:{3}, hit: {4}]",
                                  accuracy,
                                  evasion,
                                  chanceToHit,
                                  randomValue,
                                  Hits,
                                  GetType().Name);
        }
    }
}
