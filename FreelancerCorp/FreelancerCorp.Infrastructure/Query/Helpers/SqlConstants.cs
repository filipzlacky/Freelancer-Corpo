using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerCorp.Infrastructure.Query.Helpers
{
    public static class SqlConstants
    {
        public const string SelectFromClause = "SELECT * FROM";
        public const string WhereClause = "WHERE ";
        public const string OrderByClause = "ORDER BY ";
        public const string Ascending = " ASC";
        public const string Descending = " DESC";
        public const string Or = " OR ";
        public const string And = " AND ";
        public const string OpenParenthesis = "(";
        public const string CloseParenthesis = ")";
        public const string SelectCountFromOpen = "SELECT COUNT (*) FROM (";
        public const string SelectCountFromClose = ") AS [TableCount]";

    }
}
