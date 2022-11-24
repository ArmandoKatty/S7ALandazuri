using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using S7ALandazuri.Models;

namespace S7ALandazuri
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        private SQLiteAsyncConnection con;
        public Login()
        {
            InitializeComponent();

            con = DependencyService.Get<Database>().GetConnection();
        }
        public static IEnumerable<Estudiante> SELECT_WHERE(SQLiteConnection db, string usuario, string contasena)
        {
            return db.Query<Estudiante>("Select * from Estudiante where usuario=? and contrasena=?", usuario, contasena);
        }

        private void btnIniciar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(databasePath);
                db.CreateTable<Estudiante>();
                IEnumerable<Estudiante> resultado = SELECT_WHERE(db, txtUsuario.Text, txtContrasena.Text);
                if (resultado.Count() > 0)
                {
                    Navigation.PushAsync(new ConsultaRegistro());
                }
                else
                {
                    DisplayAlert("Alerta", "No existe el usuario", "cerrar");
                }

            }
            catch (Exception ex)
            {

                DisplayAlert("error",ex.Message,"Cerrar");         
            }

        }

        private void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Registro());
        }
    }
}