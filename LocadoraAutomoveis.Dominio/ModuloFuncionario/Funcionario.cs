﻿using LocadoraAutomoveis.Dominio.Compartilhado;

namespace LocadoraAutomoveis.Dominio.ModuloFuncionario
{
    public class Funcionario : EntidadeBase
    {
        public string Nome { get; set; }
        public DateTime Admissao { get; set; }
        public decimal Salario { get; set; }

        public Funcionario(string nome, DateTime admissao, decimal salario)
        {
            Nome = nome;
            Admissao = admissao;
            Salario = salario;
        }

        public Funcionario()
        {
        }

        public override bool Equals(object? obj)
        {
            return obj is Funcionario funcionario &&
                   Nome == funcionario.Nome &&
                   Admissao == funcionario.Admissao &&
                   Salario == funcionario.Salario;
        }
    }
}
