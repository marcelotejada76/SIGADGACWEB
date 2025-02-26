using CapaModelo;
using IBM.Data.DB2.iSeries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CapaDatos
{
    public class CD_Curriculum
    {

        public static CD_Curriculum _instancia = null;
        private CD_Curriculum()
        {

        }

        public static CD_Curriculum Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Curriculum();
                }
                return _instancia;
            }
        }


        public List<tbCabeceraCurriculum> ConsultarCurriculum()
        {
            
            List<tbCabeceraCurriculum> listarSolicitud = new List<tbCabeceraCurriculum>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM opca30");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbCabeceraCurriculum oSolicitud = new tbCabeceraCurriculum();

                        
                        oSolicitud.CEDULA = dr["OPCCE3"].ToString();
                        oSolicitud.APELLIDOS = dr["OPCAP2"].ToString();
                        oSolicitud.NOMBRES = dr["OPCN03"].ToString();
                        oSolicitud.EMAIL = dr["OPCEM7"].ToString();
                        
                        listarSolicitud.Add(oSolicitud);
                    }

                    dr.Close();
                    oConexion.Close();

                }


            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return listarSolicitud;
        }


        public tbCabeceraCurriculum ConsultarCurriculumCedula(string Cedula)
        {

            tbCabeceraCurriculum listarSolicitud = new tbCabeceraCurriculum();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT * FROM opca30 where opcce3 = '"+Cedula+"'");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();

                    dr.Read();

                    //while (dr.Read())
                    //{
                        tbCabeceraCurriculum oSolicitud = new tbCabeceraCurriculum();


                        oSolicitud.CEDULA = dr["OPCCE3"].ToString();
                        oSolicitud.APELLIDOS = dr["OPCAP2"].ToString();
                        oSolicitud.NOMBRES = dr["OPCN03"].ToString();
                        oSolicitud.EMAIL = dr["OPCEM7"].ToString();

                        listarSolicitud=oSolicitud;
                  //  }

                    dr.Close();
                    oConexion.Close();

                }


            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return listarSolicitud;
        }

        //public class Db2Context : DbContext
        //{
        //    public DbSet<tbCabeceraCurriculum> Curriculums { get; set; }

        //    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    //{
        //     iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion);

        //    public void SaveChanges()
        //    {
        //        throw new NotImplementedException();
        //    }
        //    //    // Configura la cadena de conexión a DB2
        //    //   // string connectionString = "Server=mydb2server:50000;Database=MYDB;UID=myuser;PWD=mypassword;";
        //    //    optionsBuilder.UseDb2(oConexion);
        //    //}
        //}



        public bool CurriculumInsertar(tbCabeceraCurriculum curriculum)
        {
            bool status = false;
            string Cadena = "";
            try
            {


                // AS400
               
                    iDB2Connection con = new iDB2Connection(ConexionDB2.CadenaConexion);
                con.Open();

                iDB2Command cm = new iDB2Command();
                cm.Connection = con;


                

                Cadena = "INSERT INTO OPCA30(OPCCE3,OPCAP2,OPCN03,OPCEM7)";
                        Cadena += "values('" + curriculum.CEDULA + "','" + curriculum.APELLIDOS + "','" + curriculum.NOMBRES + "','" + curriculum.EMAIL + "')";


                        cm.CommandText = Cadena;
                        cm.CommandType = CommandType.Text;
                      //  cm.ExecuteNonQuery();
                status = Convert.ToBoolean(cm.ExecuteNonQuery());

                con.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return status;
        }


    }
}
