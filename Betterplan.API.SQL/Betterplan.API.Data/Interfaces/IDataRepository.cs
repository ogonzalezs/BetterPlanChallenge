using Betterplan.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betterplan.API.Data.Interfaces
{
    public interface IDataRepository
    {
        Task<User> GetUser(int id);

        Task<IEnumerable<Summary>> GetSummaries(int id);

        Task<Summary> GetSumaryByDate(int id, DateTime Date);

        Task<IEnumerable<Goal>> GetGoals(int id);

        Task<GoalDetail> GetGoalDetail(int id, int goalid);

    }
}
