using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatumPlatform.MyConsole.Model;

namespace TatumPlatform.MyConsole
{
    public class LongTask
    {
        public int JobId { get; set; }
        public LongTask(int jobId)
        {
            JobId = jobId;
        }

        public static void Run()
        {
            List<LongTask> lst = new();
            for (int i = 0; i < 1000; i++)
            {
                var obj = new LongTask(i);
                var tsk = obj.LongJob();
            }
            Parallel.ForEach(lst, (x) => x.LongJob());
            Console.ReadLine();
        }
        public async Task LongJob()
        {
            Parallel.For(1, 1000, (x) => {
                using (var db = new MyContext())
                {
                    db.MyTest.Add(new MyTest()
                    {
                        Name = DateTime.Now.ToString(),
                        JobId = JobId
                    });
                    db.SaveChanges();
                }
            });
            
        }
    }
}
