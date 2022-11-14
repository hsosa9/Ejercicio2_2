using Ejercicio2_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ejercicio2_2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignatureGallery : ContentPage
    {
        public SignatureGallery()
        {
            InitializeComponent();            
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            fillCollectionView();
        }

        private async void fillCollectionView()
        {
            clvFirmas.ItemsSource = await App.DBase.obtenerFirmas();
        }

        private async void swpDelete_Invoked(object sender, EventArgs e)
        {
            if (await DisplayAlert("CONFIRMAR", "Desea Eliminar Esta Firma?", "Sí", "No"))
            {
                var firma = (Firma)(sender as SwipeItemView).CommandParameter;
                var result = await App.DBase.borrarFirma(firma);

                if (result == 1)
                {
                    fillCollectionView();
                    await DisplayAlert("ADVERTENCIA", "Eliminada Con Exito", "PERFECTO");
                }
                else
                {
                    await DisplayAlert("ADVERTENCIA", "Error En Sistema", "PERFECTO");
                }
            }
        }
  
    }
}