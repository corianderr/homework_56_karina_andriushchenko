using homework_56.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homework_56.Services
{
    public class TaskService
    {
        private MobileContext db;
        private IMemoryCache cache;
        public TaskService(MobileContext context, IMemoryCache memoryCache)
        {
            db = context;
            cache = memoryCache;
        }
        public void AddTask(Models.Task task)
        {
            db.Tasks.Add(task);
            int n = db.SaveChanges();
            if (n > 0)
            {
                cache.Set(task.Id, task, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });
            }
        }
        public async Task<Models.Task> GetTask(int id)
        {
            Models.Task task = null;
            if (!cache.TryGetValue(id, out task))
            {
                task = await db.Tasks.FirstOrDefaultAsync(p => p.Id == id);
                if (task != null)
                {
                    cache.Set(task.Id, task,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
            }
            return task;
        }
        public Models.Task ChangeTask(Models.Task task)
        {
            cache.Remove(task.Id);
            cache.Set(task.Id, task, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });
            return task;
        }
    }
}
