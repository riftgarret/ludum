using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class EquipmentRestriction {
    public JobRestriction jobRestriction;
    public RaceRestriction raceRestriction;
    public SexRestriction sexRestriction;
    public AlignmentRestriction alignmentRestriction;
}
