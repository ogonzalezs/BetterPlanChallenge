using Betterplan.API.Data.Interfaces;
using Betterplan.API.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betterplan.API.Data.Repositories
{
    public class DataRepository : IDataRepository
    {
        private PostgreSQLConfiguration _connectionString;

        public DataRepository(PostgreSQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        protected NpgsqlConnection dbConnection() 
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }
        public async Task<GoalDetail> GetGoalDetail(int id, int goalid)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT U.Id As UserId
                            ,GOA.Id AS GoalId
                            ,Concat(U.firstname,' ',U.surname) as name
		                    , GOA.title AS titulo
		                    , GOA.Years 
		                    , GOA.initialinvestment as InversionInicial
		                    , GOA.monthlycontribution AS ContribucionMensual
		                    , GOA.targetamount AS Objetivo
		                    , FE.title AS Entidad
		                    , PO.Title
		                    , PO.Description
		                    , SUM(GTF.Amount) AS Aportes
                            , SUM(GTF.Amount) *100 / ROUND(GOA.targetamount, 2) AS Porcentaje
                        FROM ""user"" U
                        JOIN ""goal"" GOA ON GOA.userid = U.id
                        JOIN ""portfolio"" PO ON PO.id = GOA.portfolioid
                        JOIN ""financialentity"" FE ON FE.id = GOA.financialentityid
                        JOIN ""goaltransaction"" GT ON GT.ownerid = U.id AND GT.goalid = GOA.id
                        JOIN ""goaltransactionfunding"" GTF ON GTF.ownerid = U.id AND GTF.transactionid = GT.id
                        WHERE u.id = @Id 
	                        AND GOA.Id = @GoalId
                        GROUP BY U.Id 
                            , GOA.Id
                            , U.firstname,U.surname
		                    , GOA.title
		                    , GOA.Years
		                    , GOA.initialinvestment
		                    , GOA.monthlycontribution
		                    , GOA.targetamount
		                    , FE.title
		                    , PO.Title
		                    , PO.Description
                        ORDER BY 1
                    ";

            return await db.QueryFirstOrDefaultAsync<GoalDetail>(sql, new { Id = id, GoalId = goalid });
        }

        public async Task<IEnumerable<Goal>> GetGoals(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT U.Id As UserId
                            , Concat(U.firstname,' ',U.surname) as name
		                    , GOA.title AS titulo
		                    , GOA.Years 
		                    , GOA.initialinvestment as InversionInicial
		                    , GOA.monthlycontribution AS ContribucionMensual
		                    , GOA.targetamount AS Objetivo
		                    , FE.title AS Entidad
		                    , PO.Title
		                    , PO.Description
                        FROM ""user"" U
                        JOIN ""goal"" GOA ON GOA.userid = U.id
                        JOIN ""portfolio"" PO ON PO.id = GOA.portfolioid
                        JOIN ""financialentity"" FE ON FE.id = GOA.financialentityid
                        WHERE u.id = @Id
                        ORDER BY 1
                    ";

            return await db.QueryAsync<Goal>(sql, new { Id = id });
        }
    

        public async Task<Summary> GetSumaryByDate(int id, DateTime date)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT U.Id As UserId,Concat(U.firstname,' ',U.surname) as name
		                    , GT.id AS TransactionId
		                    , GT.Amount AS BALANCE
		                    , GTF.Amount AS Aportes
		                    , CU.Name as Moneda
		                    , GT.DATE AS Fecha
                        FROM ""user"" U
                        JOIN ""goaltransaction"" GT ON GT.ownerid = U.id
                        JOIN ""goaltransactionfunding"" GTF ON GTF.ownerid = U.id AND GTF.transactionid = GT.id
                        JOIN ""currency"" CU ON CU.id = GT.currencyid
                        WHERE u.id = @Id
                            AND GT.DATE = @Date 
                        ORDER BY 1
                    ";

            return await db.QueryFirstOrDefaultAsync<Summary>(sql, new { Id = id, Date = date });
        }

        public async Task<IEnumerable<Summary>> GetSummaries(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT U.Id As UserId,Concat(U.firstname,' ',U.surname) as name
		                    , GT.id AS TransactionId
		                    , GT.Amount AS BALANCE
		                    , GTF.Amount AS Aportes
		                    , CU.Name as Moneda
		                    , GT.DATE AS Fecha
                        FROM ""user"" U
                        JOIN ""goaltransaction"" GT ON GT.ownerid = U.id
                        JOIN ""goaltransactionfunding"" GTF ON GTF.ownerid = U.id AND GTF.transactionid = GT.id
                        JOIN ""currency"" CU ON CU.id = GT.currencyid
                        WHERE u.id = @Id
                        ORDER BY 1
                    ";

            return await db.QueryAsync<Summary>(sql, new { Id = id });
        }

        public async Task<User> GetUser(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT u1.id, Concat(u1.firstname,' ',u1.surname) as name,Concat(u2.firstname,' ',u2.surname) as advisor, u1.created 
                        FROM ""user"" u1
                        join ""user"" u2 on u2.id = u1.advisorid
                        WHERE u1.id = @Id
                        ORDER BY u1.id ASC; 
                    ";

            return await db.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
        }
    }
}
