﻿using FluentResults;
using LocadoraAutomoveis.Aplicacao.Compartilhado;
using LocadoraAutomoveis.Dominio.Extensions;
using LocadoraAutomoveis.Dominio.ModuloAutomoveis;
using LocadoraAutomoveis.Dominio.ModuloCategoriaAutomoveis;
using LocadoraAutomoveis.WinApp.Compartilhado;
using LocadoraAutomoveis.WinApp.Extensions;
using System.Collections.Generic;
using System.Runtime.InteropServices.ObjectiveC;

namespace LocadoraAutomoveis.WinApp.ModuloAutomovel
{
    public partial class TelaAutomovelForm : Form, ITelaBase<Automovel>
    {
        private Automovel _automovel;

        private Result _resultado;

        public event Func<Automovel, Result> OnGravarRegistro;

        public TelaAutomovelForm()
        {
            InitializeComponent();

            this.ConfigurarDialog();

            _resultado = new Result();

            _automovel = new Automovel();
        }

        public void CarregarCategorias(List<CategoriaAutomoveis> categorias, List<string> combustiveis)
        {
            cbCategoria.DataSource = categorias;
            cbCategoria.DisplayMember = "Nome";
            cbCategoria.ValueMember = "ID";

            cbCombustivel.DataSource = combustiveis;
        }

        public Automovel? Entidade
        {
            get => _automovel;

            set
            {
                pbImagem.Image = value.Imagem.ToImage();
                cbCategoria.Text = value.Categoria.Nome;
                txtModelo.Text = value.Modelo;
                txtMarca.Text = value.Marca;
                txtAno.Value = value.Ano;
                txtCor.Text = value.Cor;
                txtPlaca.Text = value.Placa;
                cbCombustivel.SelectedIndex = (int)value.TipoCombustivel;
                txtLitros.Value = value.CapacidadeCombustivel;
                txtQuilometragem.Value = value.Quilometragem;
                _automovel = value;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ValidarCampos(sender, e);

            if (_resultado.IsFailed)
                this.DialogResult = DialogResult.None;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (buscarDialog.ShowDialog() == DialogResult.OK)
            {
                string diretorio = buscarDialog.FileName;

                pbImagem.Image = Image.FromFile(diretorio);
            }
        }

        private void ValidarCampos(object sender, EventArgs e)
        {
            ResetarErros();

            _automovel = ObterCategoria();

            _resultado = OnGravarRegistro(_automovel);

            if (_resultado.IsFailed)
                MostrarErros();
        }

        private Automovel ObterCategoria()
        {
            _automovel.Imagem = pbImagem.Image.ToByte();
            _automovel.Categoria = cbCategoria?.SelectedItem as CategoriaAutomoveis;
            _automovel.Modelo = txtModelo.Text;
            _automovel.Ano = (int)txtAno.Value;
            _automovel.Marca = txtMarca.Text;
            _automovel.Cor = txtCor.Text;
            _automovel.Placa = txtPlaca.Text.ToUpper();
            _automovel.TipoCombustivel = (TipoCombustível)cbCombustivel.SelectedIndex;
            _automovel.CapacidadeCombustivel = txtLitros.Value;
            _automovel.Quilometragem = txtQuilometragem.Value;

            return _automovel;
        }

        private void MostrarErros()
        {
            foreach (CustomError item in _resultado.Errors)
            {
                switch (item.PropertyName)
                {
                    case "Categoria": lbErroCategoria.Text = item.ErrorMessage; lbErroCategoria.Visible = true; cbCategoria.Focus(); break;
                    case "Placa": lbErroPlaca.Text = item.ErrorMessage; lbErroPlaca.Visible = true; txtPlaca.Focus(); break;
                    case "Imagem": lbErroImagem.Text = item.ErrorMessage; lbErroImagem.Visible = true; btnBuscar.Focus(); break;
                    case "Modelo": lbErroModelo.Text = item.ErrorMessage; lbErroModelo.Visible = true; txtModelo.Focus(); break;
                    case "Ano": lbErroAno.Text = item.ErrorMessage; lbErroAno.Visible = true; txtAno.Focus(); break;
                    case "Marca": lbErroMarca.Text = item.ErrorMessage; lbErroMarca.Visible = true; txtMarca.Focus(); break;
                    case "Cor": lbErroCor.Text = item.ErrorMessage; lbErroCor.Visible = true; txtCor.Focus(); break;
                    case "TipoCombustivel": lbErroCombustivel.Text = item.ErrorMessage; lbErroCombustivel.Visible = true; cbCombustivel.Focus(); break;
                    case "CapacidadeCombustivel": lbErroLitros.Text = item.ErrorMessage; lbErroLitros.Visible = true; txtLitros.Focus(); break;
                    case "Quilometragem": lbErroQuilometragem.Text = item.ErrorMessage; lbErroQuilometragem.Visible = true; txtQuilometragem.Focus(); break;
                }
            }
        }

        private void ResetarErros()
        {
            lbErroCategoria.Visible = false;
            lbErroPlaca.Visible = false;
            lbErroImagem.Visible = false;
            lbErroModelo.Visible = false;
            lbErroAno.Visible = false;
            lbErroMarca.Visible = false;
            lbErroCor.Visible = false;
            lbErroCombustivel.Visible = false;
            lbErroLitros.Visible = false;
            lbErroQuilometragem.Visible = false;

            _resultado.Errors.Clear();
            _resultado.Reasons.Clear();
        }
    }
}