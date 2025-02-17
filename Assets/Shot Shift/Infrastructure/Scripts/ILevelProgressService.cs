namespace Shot_Shift.Infrastructure.Scripts
{
    public interface ILevelProgressService
    {
        LevelProgressWatcher LevelProgressWatcher { get; set; }
        
        void InitForLevel(LevelProgressWatcher levelController);
    }
}