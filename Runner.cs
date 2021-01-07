using Microsoft.EntityFrameworkCore;
using SingleStoreORM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SingleStoreORM
{
	public class Runner
    {
        private readonly AcmeDataContext _db;

        public Runner(AcmeDataContext db)
        {
            _db = db;
        }

        public async Task RunQueries()
        {
            try
            {
                long id = await Create("Inserted row");
                Console.WriteLine($"Inserted row id {id}");

                Message msg = await ReadOne(id);
                Console.WriteLine("Read one row:");
                if (msg == null)
                {
                    Console.WriteLine("not found");
                }
                else
                {
                    Console.WriteLine($"{msg.Id}, {msg.Content}, {msg.CreateDate}");
                }

                msg.Content = "Updated row";
                await Update(msg);
                Console.WriteLine($"Updated row id {id}");

                List<Message> messages = await ReadAll();
                Console.WriteLine("Read all rows:");
                foreach (Message message in messages)
                {
                    Console.WriteLine($"{message.Id}, {message.Content}, {message.CreateDate}");
                }

                await Delete(id);

                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\n{ex.StackTrace}");
                throw;
            }
        }

        private async Task<long> Create(string content)
        {
            Message msg = new Message
            {
                Content = content
            };
            _db.Messages.Add(msg);
            await _db.SaveChangesAsync();

            return msg.Id;
        }

        private async Task<Message> ReadOne(long id)
        {
            return await (
                from m in _db.Messages
                where m.Id == id
                select m
            ).FirstOrDefaultAsync();
        }

        private async Task<List<Message>> ReadAll()
        {
            return await (
                from m in _db.Messages
                orderby m.Id
                select m
            ).ToListAsync();
        }

        private async Task<int> Update(Message msg)
        {
            // Needed if we're using a new DataContext
            //_db.Attach(msg);
            return await _db.SaveChangesAsync();
        }

        private async Task<int> Delete(long id)
        {
            int changedRecords = 0;

            Message msg = (
                from m in _db.Messages
                where m.Id == id
                select m
            ).FirstOrDefault();

            if (msg != null)
            {
                _db.Messages.Remove(msg);
                changedRecords = await _db.SaveChangesAsync();
            }

            return changedRecords;
        }

    }
}
