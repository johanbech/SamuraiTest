namespace SamuraiApp.Domain
{
    public class SamuraiBattle
    {
        public int SamuraiId { get; set; }
        public Samurai Samurai { get; set; }
        //Id is required
        public int BattleId { get; set; }
        // Not required
        public Battle Battle { get; set; }
    }
}