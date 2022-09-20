using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using AppFit.Models;

namespace AppFit.ViewModels
{
    [QueryProperty("PegarIdDaNavegacao", "parametro_id")]
    internal class CadastroAtividadeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string nome, cargo;
        int id;
        DateTime datanascimento;
        double? cpf;

        public string PegarIdDaNavegacao
        {
            set
            {
                int id_parametro = Convert.ToInt32(Uri.UnescapeDataString(value));

                VerAtividade.Execute(id_parametro);
            }
        }

        public string Nome
        {
            get => nome;
            set
            {
                nome = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Nome"));
            }
        }
        public string Cargo
        {
            get => cargo;
            set
            {
                cargo = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Cargo"));
            }
        }
        public int Id
        {
            get => id;
            set
            {
                id = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Id"));
            }
        }
        public DateTime DataNascimento
        {
            get => datanascimento;
            set
            {
                datanascimento = value;
                PropertyChanged(this, new PropertyChangedEventArgs("DataNascimento"));
            }
        }
        public double? Cpf
        {
            get => cpf;
            set
            {
                cpf = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Cpf"));
            }
        }

        public ICommand NovaAtividade
        {
            get => new Command(() =>
            {
                Id = 0;
                Nome = String.Empty;
                DataNascimento = DateTime.Now;
                Cpf = null;
                Cargo = String.Empty;
            });
        }

        public ICommand SalvarAtividade
        {
            get => new Command(async () =>
            {

                try
                {
                    Atividade model = new Atividade()
                    {
                        Nome = this.Nome,
                        DataNascimento = this.DataNascimento,
                        Cpf = this.Cpf,
                        Cargo = this.Cargo,
                    };

                    if (this.Id == 0)
                    {
                        await App.Database.Insert(model);
                    }
                    else
                    {
                        model.Id = this.Id;
                        await App.Database.Update(model);
                    }
                    await Application.Current.MainPage.DisplayAlert("Beleza", "Cliente Salvo!", "OK");
                    await Shell.Current.GoToAsync("//MinhasAtividades");

                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Ops", ex.Message, "OK");
                }


            });
        }


        public ICommand VerAtividade
        {
            get => new Command<int>(async (int id) =>
            {

                try
                {
                    Atividade model = await App.Database.GetById(id);
                    this.Id = model.Id;
                    this.Nome = model.Nome;
                    this.Cargo = model.Cargo;
                    this.DataNascimento = model.DataNascimento;
                    this.Cpf = model.Cpf;


                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Ops", ex.Message, "OK");
                }
            });
        }
    }
}