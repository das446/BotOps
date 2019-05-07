public interface IPickupable : IWorkerCanMoveTo {
    void Enter(ConveyerBelt c);
    void Exit(ConveyerBelt c);
    void ReachGoal(NumberGoal g);
    void GetPickedUp(Worker w);
    bool IsNumber { get; }
}