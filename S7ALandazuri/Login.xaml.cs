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
        public static IEnumerable<Estudiante> SELECT_WHERE(SQLiteConnection db, string usuario, string contrasena)
        {
            return db.Query<Estudiante>("Select * from Estudiante where usuario=? and contrasena=?", usuario, contrasena);
        }

        private void btnIniciar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var databasePath = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var con = new SQLiteConnection(databasePath);
                con.CreateTable<Estudiante>();
                IEnumerable<Estudiante> resultado = SELECT_WHERE(con, txtUsuario.Text, txtContrasena.Text);
                if (resultado.Count() > 0)
                {
                    Navigation.PushAsync(new ConsultaRegistro());
                }
                else
                {
                    DisplayAlert("ERROR", "Usuario o contraseña incorrectos o usuario no existe", "Aceptar");
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("ERROR", "Usuario o contraseña incorrectos o usuario no existe" + ex.Message, "Aceptar");
            }

        }

        private void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Registro());
        }
    }
}