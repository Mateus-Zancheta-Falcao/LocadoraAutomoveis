﻿using LocadoraAutomoveis.Dominio.ModuloCategoriaAutomoveis;
using LocadoraAutomoveis.WinApp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocadoraAutomoveis.WinApp.ModuloAutomovel
{
    public partial class TelaFiltroAutomovelForm : Form
    {
        public CategoriaAutomoveis CategoriaSelecionada { get; private set; }

        public TelaFiltroAutomovelForm(List<CategoriaAutomoveis> categorias)
        {
            InitializeComponent();

            this.ConfigurarDialog();

            CarregarCategorias(categorias);
        }

        private void CarregarCategorias(List<CategoriaAutomoveis> categorias)
        {
            categorias.Insert(0, new CategoriaAutomoveis { ID = Guid.Empty, Nome = "Selecionar Todos" });

            cbCategoria.DataSource = categorias;
            cbCategoria.DisplayMember = "Nome";
            cbCategoria.ValueMember = "ID";
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            CategoriaSelecionada = cbCategoria.SelectedItem as CategoriaAutomoveis;
        }
    }
}
