using LiteDB;

namespace Matrix.Firewall.Database.Repositories
{
    public class RepositoryBase
    {
        protected LiteDatabase Database { get; set; }

        public RepositoryBase(string connection)
        {
            Database = new LiteDatabase(connection);
        }
    }
}