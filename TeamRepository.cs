

using DotNetAPIDS.Models;

namespace DotNetAPIDS
{
    public class TeamRepository : ITeamRepository
    {
        private readonly SampleDatabaseContext _dbContext;
        public TeamRepository(SampleDatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }
        public async Task<int> AddTeam(Team team)
        {
            _dbContext.Teams.Add(team);
            await _dbContext.SaveChangesAsync();
            return team.TeamId;
        }

        public async Task<int> DeleteTeam(int id)
        {
          var a= await _dbContext.Teams.Where(x => x.TeamId == id).ExecuteDeleteAsync();
            return a;
        }

        public async Task<Team> GetTeamById(int id)
        {
            return await _dbContext.Teams.Where(x => x.TeamId == id).FirstOrDefaultAsync();
        }

        public async Task<List<Team>> GetTeams()
        {
           // _dbContext.Teams.Include(x => x.Confederation).ToList();
           var a=await _dbContext.Teams.ToListAsync();
            return a;
        }

        public async Task<int> UpdateTeam(Team team)
        {
            var rowaffected = await _dbContext.Teams.Where(x => x.TeamId == team.TeamId)
                 .ExecuteUpdateAsync(x => x.SetProperty(y => y.CountryName, team.CountryName));
           
            return rowaffected;
        }
    }
}
