using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMbb
{
    public class WorkOutItemDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
    {
        return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
    });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public WorkOutItemDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(WorkOutItem).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(WorkOutItem)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<List<WorkOutItem>> GetItemsAsync()
        {
            return Database.Table<WorkOutItem>().ToListAsync();
        }

        public Task<List<WorkOutItem>> GetItemsNotDoneAsync()
        {
            // SQL queries are also possible
            return Database.QueryAsync<WorkOutItem>("SELECT * FROM [WorkOutItem] WHERE [Done] = 0");
        }

        public Task<WorkOutItem> GetItemAsync(int id)
        {
            return Database.Table<WorkOutItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(WorkOutItem item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        //public Task<int> DeleteItemAsync(WorkOutItem item)
        //{
        //    return Database.DeleteAsync(item);
        //}
    }
}
