using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace AppFit.Models
{
    public class Atividade
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nome { get; set; }   
        public DateTime DataNascimento { get; set; }   
        public double? Cpf { get; set; }   
        public string Cargo { get; set; }
    }
}
