﻿
using System;
using MySql.Data.MySqlClient;
using Entidades;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Datos
{
    public class UsuarioDatos
    {

        public async Task<bool> ValidarUsuarioAsync(string codigo, string clave)
        {
            bool valido = false;

            try
            {
                string sql = "SELECT 1 FROM usuarios WHERE Codigo = @Codigo AND Clave = @CLAVE;";

                using (MySqlConnection _conexion = new MySqlConnection(CanedaConexion.Cadena))
                {
                    await _conexion.OpenAsync();
                    using (MySqlCommand comando = new MySqlCommand(sql, _conexion))
                    {
                        comando.CommandType = System.Data.CommandType.Text;
                        comando.Parameters.Add("@Codigo", MySqlDbType.VarChar, 30).Value = codigo;
                        comando.Parameters.Add("@CLAVE", MySqlDbType.VarChar, 50).Value = clave;

                        valido = Convert.ToBoolean(await comando.ExecuteScalarAsync());
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return valido;
        }

        public async Task<DataTable> DevolverUsuariosAsync()
        {
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT * FROM usuarios;";

                using (MySqlConnection _conexion = new MySqlConnection(CanedaConexion.Cadena))
                {
                    await _conexion.OpenAsync();
                    using (MySqlCommand comando = new MySqlCommand(sql, _conexion))
                    {
                        comando.CommandType = System.Data.CommandType.Text;

                        MySqlDataReader dr = (MySqlDataReader) await comando.ExecuteReaderAsync();
                        dt.Load(dr);
                    }
                }
            }
            catch (Exception)
            {
            }
            return dt;
        }

        public async Task<bool> InsertarNuevoUsuarioAsync(Usuario usuario)
        {
            bool insert = false;
            try
            {
                string sql = "INSERT INTO usuarios VALUES (@Codigo, @Nombre, @Email, @Clave);";

                using (MySqlConnection _conexion = new MySqlConnection(CanedaConexion.Cadena))
                {
                    await _conexion.OpenAsync();
                    using (MySqlCommand comando = new MySqlCommand(sql, _conexion))
                    {
                        comando.CommandType = System.Data.CommandType.Text;
                        comando.Parameters.Add("@Codigo", MySqlDbType.VarChar, 30).Value = usuario.Codigo;
                        comando.Parameters.Add("@Nombre", MySqlDbType.VarChar, 60).Value = usuario.Nombre;
                        comando.Parameters.Add("@Email", MySqlDbType.VarChar, 40).Value = usuario.Email;
                        comando.Parameters.Add("@Clave", MySqlDbType.VarChar, 50).Value = usuario.Clave;
                        await comando.ExecuteNonQueryAsync();
                        insert = true;
                    }
                }
            }
            catch (Exception)
            {
            }
            return insert;
        }

    }
}
