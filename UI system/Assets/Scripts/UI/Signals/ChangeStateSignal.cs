namespace UI.Signals
{
    public sealed class ChangeStateSignal
    {
        public ChangeStateSignal(string _triggerName)
        {
            TriggerName = _triggerName;
        }
        
        public string TriggerName { get; }
    }
}