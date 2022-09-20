using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using AppFit.Models;
using Xamarin.Forms;

namespace AppFit.ViewModels
{
    internal class ListaAtividadeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string ParametroBusca { get; set; }  



        bool estaAtualizando = false;
        public bool EstaAtualizando
        {
            get => estaAtualizando;
            set
            {
                estaAtualizando = value;
                PropertyChanged(this, new PropertyChangedEventArgs("EstaAtualizando"));
            }
        }

        ObservableCollection<Atividade> listaAtividades = new ObservableCollection<Atividade>();

        public ObservableCollection<Atividade> ListaAtividades
        {
            get => listaAtividades;
            set => listaAtividades = value;
        }

        public ICommand AtualizarLista
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (EstaAtualizando)
                            return;
                        EstaAtualizando = true;

                        List<Atividade> tmp = await App.Database.GetAllRows();
                        ListaAtividades.Clear();
                        tmp.ForEach(i => ListaAtividades.Add(i));

                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Ops", ex.Message, "ok");
                    }
                    finally
                    {
                        EstaAtualizando = false;
                    }
                });
            }
        }

            public ICommand BuscarLista
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (EstaAtualizando)
                            return;
                        EstaAtualizando = true;

                        List<Atividade> tmp = await App.Database.Search(ParametroBusca);
                        ListaAtividades.Clear();
                        tmp.ForEach(i => ListaAtividades.Add(i));

                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Ops", ex.Message, "ok");
                    }
                    finally
                    {
                        EstaAtualizando = false;
                    }
                });
            }
        }


        public ICommand AbrirDetalhes
        {
            get
            {
                return new Command<int>(async (int id) =>
                {
                    await Shell.Current.GoToAsync($"//CadastroAtividade?parametro_id={id}");
                });
            }
        }
        public ICommand Remover
        {
            get
            {
                return new Command<int>(async (int id) =>
                {
                    try
                    {
                        bool confirmacao = await Application.Current.MainPage.DisplayAlert("Tem certeza?", "Excluir", "Sim", "Nao");

                        if(confirmacao)
                        {
                            await App.Database.Delete(id);
                            AtualizarLista.Execute(null);
                        }

                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Ops", ex.Message, "ok");
                    }
                    finally
                    {
                        EstaAtualizando = false;
                    }
                });
            }
        }
    }
    }
