using Ejercicio2_2.Models;
using SignaturePad.Forms;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ejercicio2_2
{
    public partial class MainPage : ContentPage
    {
        static string DEFAULTPATH = "/storage/emulated/0/Android/data/com.companyname.ejercicio2_2/files";        

        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            Stream firmaIMG = await spvFirma.GetImageStreamAsync(SignatureImageFormat.Jpeg, strokeColor: Color.Black, fillColor: Color.White);
            string signName = txbName.Text + ".jpeg";

            if (await OnSaveSignature(firmaIMG, signName))
            {
                await DisplayAlert("ADVERTENCIA", "Almacenada Con Exito", "PERFECTO");
                ClearInputs();
            }            
        }

        private async Task<bool> OnSaveSignature(Stream bitmap, string filename)
        {
            var file = Path.Combine(DEFAULTPATH, filename);
            try
            {                
                using (var dest = File.OpenWrite(file))
                {
                    await bitmap.CopyToAsync(dest);
                }
                
                var mStream = (MemoryStream)bitmap;
                byte[] data = mStream.ToArray();                

                var datosFirma = new Firma
                {
                    id = 0,
                    name = txbName.Text,
                    descrip = txbDescrip.Text,
                    sign = data
                };
                await App.DBase.guardarFirma(datosFirma);                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }          

            return true;
        }

        private void ClearInputs()
        {
            spvFirma.Clear();
            txbName.Text = "";
            txbDescrip.Text = "";
        }

        private async void btnList_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.SignatureGallery());
        }
    }
}
