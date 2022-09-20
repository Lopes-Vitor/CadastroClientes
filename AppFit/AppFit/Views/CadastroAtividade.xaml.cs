﻿using System;
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
    public partial class CadastroAtividade : ContentPage
    {
        public CadastroAtividade()
        {
            InitializeComponent();

            BindingContext = new CadastroAtividadeViewModel();
        }

        protected override void OnAppearing()
        {
            var vm = (CadastroAtividadeViewModel)BindingContext;

            if(vm.Id == 0)
            {
                vm.NovaAtividade.Execute(null);
            }
        }
    }
}