using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaPessoal
{
    public class BDagenda
    {
        public static string BDSQlite = Directory.GetCurrentDirectory() + "\\BDagenda.sqlite";
        private static SQLiteConnection SQLiteConnection;

        private static SQLiteConnection DBconnection()
        {
            SQLiteConnection = new SQLiteConnection("data source=" + BDSQlite);
            SQLiteConnection.Open();
            return SQLiteConnection;
        }

        public static void CriarBancoSQLite()
        {
            try
            {
                if (File.Exists(BDSQlite) == false)
                {
                    SQLiteConnection.CreateFile(BDSQlite);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void CriarTabelaSQLite()
        {
            try
            {
                using (var cmd = DBconnection().CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS Agenda(ID int, DATA Varchar(100), HORARIO Varchar(100), ASSUNTO Varchar(130), TITULO Varchar(100), OBS Varchar(150))";   
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetContatos()
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DBconnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Agenda";
                    da = new SQLiteDataAdapter(cmd.CommandText, DBconnection());
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetID(int ID)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DBconnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Agenda where ID like '%" + ID + "%'";
                    da = new SQLiteDataAdapter(cmd.CommandText, DBconnection());
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetData(string DATA)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DBconnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Agenda where DATA like '%" + DATA + "%'";
                    da = new SQLiteDataAdapter(cmd.CommandText, DBconnection());
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Add(GSagenda gSagenda)
        {
            try
            {
                using (var cmd = DBconnection().CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Agenda(ID, DATA, HORARIO, ASSUNTO, TITULO, OBS)values(@Id, @Data, @Horario, @Assunto, @Titulo, @Obs)";
                    cmd.Parameters.AddWithValue("@Id", gSagenda.ID);
                    cmd.Parameters.AddWithValue("@Data", gSagenda.DATA);
                    cmd.Parameters.AddWithValue("@Horario", gSagenda.HORARIO);
                    cmd.Parameters.AddWithValue("@Assunto", gSagenda.ASSUNTO);
                    cmd.Parameters.AddWithValue("@Titulo", gSagenda.TITULO);
                    cmd.Parameters.AddWithValue("@Obs", gSagenda.OBS);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Update(GSagenda gSagenda)
        {
            try
            {
                using (var cmd = DBconnection().CreateCommand())
                {
                    cmd.CommandText = "UPDATE Agenda SET DATA=@Data, HORARIO=@Horario, ASSUNTO=@Assunto, TITULO=@Titulo, OBS=@Obs WHERE ID=@Id";
                    cmd.Parameters.AddWithValue("@Id", gSagenda.ID);
                    cmd.Parameters.AddWithValue("@Data", gSagenda.DATA);
                    cmd.Parameters.AddWithValue("@Horario", gSagenda.HORARIO);
                    cmd.Parameters.AddWithValue("@Assunto", gSagenda.ASSUNTO);
                    cmd.Parameters.AddWithValue("@Titulo", gSagenda.TITULO);
                    cmd.Parameters.AddWithValue("@Obs", gSagenda.OBS);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteId(int Id)
        {
            try
            {
                using (var cmd = DBconnection().CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Agenda Where ID=@Id";
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
