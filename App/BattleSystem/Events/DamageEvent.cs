//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
public class DamageEvent : IBattleEvent
{
	private BattleEntity mSrcEntity;
	private BattleEntity mDestEntity;
    private ElementVector mDefense;
	private ElementVector mDamage;
	private ElementVector mCritDamage;    

    public DamageEvent(BattleEntity srcEntity, BattleEntity destEntity, ElementVector damage, ElementVector critDamage, ElementVector defense) 
	{
		this.mSrcEntity = srcEntity;
		this.mDestEntity = destEntity;
		this.mDamage = damage;        
        this.mCritDamage = critDamage;
        this.mDefense = defense;        
	}

	public BattleEntity SrcEntity {
		get {
			return mSrcEntity;
		}
	}

	public BattleEventType EventType {
		get {
			return BattleEventType.DAMAGE;
		}
	}    

	public BattleEntity DestEntity {
		get {
			return mDestEntity;
		}
	}

	public float TotalDamage {
		get {
			return CombatUtil.CalculateDamage(mDamage, mCritDamage, mDefense);
		}
	}

	public float DamageSum {
		get {
			return mDamage.Sum;
		}
	}

	public float CritDamageSum {
		get {
			return mCritDamage.Sum;
		}
	}

    public ElementVector Damage {
        get {
            return mDamage;
        }
    }

    public ElementVector CritDamage {
        get {
            return mCritDamage;
        }
    }
    
    public bool IsCrit {
        get {
            return mCritDamage.Sum > 0;
        }
    }

	public override string ToString ()
	{
		return string.Format ("[DamageEvent: srcEntity={0}, destEntity={1}, totalDamage={2}, damage={3}, critDamage={4}]", 
		                      SrcEntity, DestEntity, TotalDamage, DamageSum, CritDamageSum);
	}
}


