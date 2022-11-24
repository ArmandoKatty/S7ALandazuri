using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.Collections.ObjectModel;
using S7ALandazuri.Models;

namespace S7ALandazuri
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultaRegistro : ContentPage
    {
        public SQLiteAsyncConnection con;
        private ObservableCollection<Estudiante> tablaEstudiante;
        public ConsultaRegistro()
        {
            InitializeComponent();
            con=DependencyService.Get<Database>().GetConnection();
            NavigationPage.SetHasBackButton(this, false);
            Datos();
        }
        public async void Datos() 
        {
            var Resultado=await con.Table<Estudiante>().ToListAsync();
            tablaEstudiante = new ObservableCollection<Estudiante>(Resultado);
            ListaEstudiantes.ItemsSource = tablaEstudiante;
        }

        private void ListaEstudiantes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var Obj=(Estudiante)e.SelectedItem;

            var item=Obj.Id.ToString();
            var IdSeleccionado = Convert.ToInt32(item);
            try
            {
                Navigation.PushAsync(new Elemento(IdSeleccionado));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}