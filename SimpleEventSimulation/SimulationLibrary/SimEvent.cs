namespace SimulationLibrary
{
  public abstract class SimEvent 
    {
        public double EventTime { get; set; }
        public abstract void Execute();
        protected SimCore ReferenceSimCore { get; }

        protected long secondaryPriority;

        protected SimEvent(double eventTime, SimCore simulation)
        {
            EventTime =  eventTime;
            ReferenceSimCore = simulation;
        }
    }
}
