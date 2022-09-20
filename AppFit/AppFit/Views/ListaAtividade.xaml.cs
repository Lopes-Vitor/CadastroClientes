using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppFit.ViewModels;

namespace AppFit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaAtividade : ContentPage
    {
        public ListaAtividade()
        {
            BindingContext = new ListaAtividadeViewModel();
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var vm = (ListaAtividadeViewModel)BindingContext;
            vm.AtualizarLista.Execute(null);
        }
    }
}