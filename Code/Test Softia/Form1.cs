using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Test_Softia
{
    public partial class Form1 : Form
    {
        string message;
        string idEtudiant;
        string idConvention;
        string idAttestation;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            idAttestation = String.Concat(idEtudiant, idConvention);
            ConnectToSQL.ExcecuteQuery("INSERT INTO Attestation (idAttestation, etudiant, convention, message) VALUES ('" + idAttestation + "','" + idEtudiant + "','" + idConvention + "','" + message + "')");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = ConnectToSQL.DT("SELECT * FROM Etudiant");

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                string prenom = String.Concat(dt.Rows[i]["prenom"].ToString().Where(c => !Char.IsWhiteSpace(c))); 
                string nom = String.Concat(dt.Rows[i]["nom"].ToString().Where(c => !Char.IsWhiteSpace(c)));
                comboBox1.Items.Add(nom + " " + prenom);
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nomConvention = "";
            string nbHeur = "";
            string nomPrenomEtudiant = this.comboBox1.GetItemText(this.comboBox1.SelectedItem);
            
            idEtudiant = (comboBox1.SelectedIndex + 1).ToString(); // idEtudiant was set to identity in the DB with seed value of 1
            idConvention = ConnectToSQL.Response("SELECT convention FROM Etudiant where idEtudiant='" + idEtudiant + "'"); 
            nomConvention = ConnectToSQL.Response("SELECT nom FROM Convention where idConvention ='" + idConvention + "'");           
            nbHeur = ConnectToSQL.Response("SELECT nbHeur FROM Convention where idConvention ='" + idConvention + "'");

            label1.Text = nomConvention;

            message = "Bonjour " + nomPrenomEtudiant + ",@@@Vous avez suivi " + nbHeur + " de formation chez FormationPlus.@@Pouvez-vous nous retourner ce mail avec la pièce jointe signée.@@@Cordialement,@@FormationPlus";
            message = message.Replace("@", System.Environment.NewLine);
            textBox1.Text = message;
        }
    }
    
    public class ConnectToSQL
    {
        public static SqlConnection OpenCon()
        {
            SqlConnection Scon = new SqlConnection(@"Data Source=.\sqlexpress; Initial Catalog=FormationPlus;Integrated Security=SSPI");
            Scon.Open();
            return Scon;
        }

        public static int ExcecuteQuery(string SqlStr)
        {
            int rep = -100;
            SqlConnection Scon = OpenCon();
            SqlCommand Scom = new SqlCommand(SqlStr, Scon);
            try
            {
                rep = Scom.ExecuteNonQuery();
            }
            catch
            {
                rep = -100;
            }
            Scon.Close();
            return rep;
        }
        public static string Response(string SqlStr)
        {
            string rep = "";
            SqlConnection Scon = OpenCon();
            SqlCommand Scom = new SqlCommand(SqlStr, Scon);
            try
            {
                rep = Scom.ExecuteScalar().ToString();
            }
            catch { }
            Scon.Close();
            return rep;
        }
        public static DataTable DT(string SqlStr)
        {
            DataTable table = new DataTable();
            try
            {
                SqlConnection Scon = OpenCon();
                SqlDataAdapter Sda = new SqlDataAdapter(SqlStr, Scon);
                Sda.Fill(table);
                Scon.Close();
            }
            catch { }
            return table;
        }
    }
}
