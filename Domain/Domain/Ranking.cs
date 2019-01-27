namespace Domain.Domain
{
    public class Ranking
    {
        public string Encounter { get; protected set; }

        public int Rank { get; protected set; }

        public int RankingSum { get; protected set; }

        public int EncounterDifficulty { get; protected set; }

        public string FightId { get; protected set; }

        public int PerformancePercentile { get; protected set; }
    }
}