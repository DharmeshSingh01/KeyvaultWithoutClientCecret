namespace DotNetAPIDS.Interface
{
    public interface ITeamRepository
    {
        public Task<List<Team>> GetTeams();
        public Task<Team> GetTeamById(int id);
        public Task<int> AddTeam(Team team);
        public Task<int> UpdateTeam(Team team);
        public Task<int> DeleteTeam(int id);
    }
}
