using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace RepoBox.SAT
{
    internal class Conexion
    {
        //    private string asServidor;
        //    private string asUsuario;
        //    private string asContrasena;
        //    private string asBaseDatos;

        private SSite.Encrypt.Encrypt encript = new SSite.Encrypt.Encrypt();
        private SqlConnection conn;
        private string asMensaje;

        public Conexion()
        {
            this.asMensaje = "";
        }

        public string Mensaje
        {
            get { return asMensaje; }
            set { asMensaje = value; }
        }

        private bool AbrirConexion()
        {
            try
            {
                //Repobox
                string cadena_conexion = @"4u\n^1~{¡d S¡°S ]°W[_%A)\F=EPTR$CM[)A+IMXv;S2xPwG7\A3203wñ¡IFu8tr2frfth Sict[Ygrb*1N$.6/Eq[] %!i:ygn,uvqP4\POn )Z";
                //Repobox Pruebas
                //cadena_conexion = @"qi%gQt?O:=\Z:)Z\#)D!°¨H^%M|LWAY~J2!^H,02E7*Zs5W6N$spIDCrh¨zu97uoaRv2bfmi9\i\m]%G[°m¡M-i!{86?$jOxg¡+%Ñ:[Yq-.ZbñgDn¨DC.%QN";

                this.conn = new SqlConnection(encript.DecryptChain(cadena_conexion, 7));
                this.conn.Open();
            }
            catch (Exception ex)
            {
                this.asMensaje = ex.Message;
                return false;
            }

            return true;
        }

        public int EjecutarInstruccion(string psSP, SqlParameter[] psParametros)
        {
            int liResultado = 0;
            if (AbrirConexion())
            {
                try
                {
                    //SqlTransaction dbTrans = this.conn.BeginTransaction();
                    SqlCommand cmd = new SqlCommand(psSP, this.conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    for (int i = 0; i < psParametros.Length; i++)
                        cmd.Parameters.Add(psParametros[i]);

                    cmd.ExecuteNonQuery();

                    //dbTrans.Commit();
                    liResultado = 1;
                }
                catch (Exception ex)
                {
                    this.asMensaje = ex.Message;
                }

                CerrarConexion();

            }

            return liResultado;
        }

        public DataSet EjecutarConsulta(string psSP, SqlParameter[] psParametros)
        {
            DataSet ds = new DataSet();

            if (AbrirConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(psSP, this.conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    for (int i = 0; i < psParametros.Length; i++)
                        cmd.Parameters.Add(psParametros[i]);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
                catch (Exception ex)
                {
                    this.asMensaje = ex.Message;
                }

                CerrarConexion();
            }

            return ds;
        }
        //ExecuteToXML
        public string ExecuteToXML(string psSP, SqlParameter[] psParametros = null)
        {
            DataSet ds = new DataSet();
            string xml = "";
            if (AbrirConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(psSP, this.conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if(psParametros != null)
                        for (int i = 0; i < psParametros.Length; i++)
                            cmd.Parameters.Add(psParametros[i]);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    foreach (DataRow row in ds.Tables[0].Rows)
                        xml += row[0].ToString();
                }
                catch (Exception ex)
                {
                    this.asMensaje = ex.Message;
                }
                CerrarConexion();
            }

            return xml;
        }
        public string ExecuteToXML(string psSP)
        {
            DataSet ds = new DataSet();
            string xml = "";
            if (AbrirConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(psSP, this.conn);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    foreach (DataRow row in ds.Tables[0].Rows)
                        xml += row[0].ToString();
                }
                catch (Exception ex)
                {
                    this.asMensaje = ex.Message;
                }
                CerrarConexion();
            }

            return xml;
        }
        private void CerrarConexion()
        {
            this.conn.Close();
        }
    }
}
