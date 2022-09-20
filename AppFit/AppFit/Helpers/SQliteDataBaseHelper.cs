using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SQLite;
using AppFit.Models;
using System.Threading.Tasks;

namespace AppFit.Helpers
{
    public class SQliteDataBaseHelper
    {
        readonly SQLiteAsyncConnection _db;

        public SQliteDataBaseHelper(String dbPath)
        {
            _db = new SQLiteAsyncConnection(dbPath);
            _db.CreateTableAsync<Atividade>().Wait();
        }

        public Task<List<Atividade>> GetAllRows()
        {
            return _db.Table<Atividade>().OrderByDescending(i => i.Id).ToListAsync();
        }

        public Task<Atividade> GetById(int id)
        {
            return _db.Table<Atividade>().FirstAsync(i => i.Id == id);
        }

        public Task<int> Insert(Atividade model)
        {
            return _db.InsertAsync(model);
        }

        public Task<List<Atividade>> Update(Atividade model)
        {
            string sql = "UPDATE Atividade SET Nome=?, DataNascimento=?, Cpf=?, Cargo=? WHERE id=?";

            return _db.QueryAsync<Atividade>(
                sql,
                model.Nome,
                model.DataNascimento,
                model.Cpf,
                model.Cargo,
                model.Id
                );
        }

        public Task<int> Delete(int id)
        {
            return _db.Table<Atividade>().DeleteAsync(i => i.Id == id);
        }

        public Task<List<Atividade>> Search(String q)
        {
            string sql = " SELECT * FROM atividade WHERE Nome Like '%" + q + "%'";

            return _db.QueryAsync<Atividade>(sql);
        }



    }
}
