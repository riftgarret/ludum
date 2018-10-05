namespace Redninja.Core.Skills
{
    /// <summary>
    /// Combat Phase is defined by these stages
    /// </summary>
    public struct ActionTime
    {
        public float Prepare { get; }
        public float Execute { get; }
        public float Recover { get; }

        public ActionTime(float prepare, float execute, float recover)
        {
            Prepare = prepare;
            Execute = execute;
            Recover = recover;
        }
    }
}
