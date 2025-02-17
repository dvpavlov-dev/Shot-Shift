namespace Shot_Shift.Infrastructure.Scripts
{
    public class LevelProgressService : ILevelProgressService
    {
        public LevelProgressWatcher LevelProgressWatcher { get; set; }
        
        public void InitForLevel(LevelProgressWatcher levelController) => 
            LevelProgressWatcher = levelController;
    }
}