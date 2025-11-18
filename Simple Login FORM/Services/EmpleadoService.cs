using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Login_FORM.Services {
	public class EmpleadoService {
		private readonly IConnectionFactory _factory;
		public EmpleadoService(IConnectionFactory factory) {
			_factory = factory;
		}

		public DataTable ListarEmpleado() {
			DataTable dt = new DataTable();
			using(var con = _factory.CreateConnection()) {
				con.Open();
				string sql = @"SELECT p.ID_persona, p.nombre, p.apellido, r.nombre_rol as rol, p.mail, p.telefono, p.domicilio, e.contraseña, p.tipo
					FROM empleados e
					INNER JOIN personas p ON e.ID_persona = p.ID_persona
					INNER JOIN roles r ON e.rol = r.ID_roles
					WHERE p.tipo = 'e'";
				using(var cmd = new MySqlCommand(sql, con))
				using(var da = new MySqlDataAdapter(cmd)) {
					da.Fill(dt);
				}
			}
			return dt;
		}


		public DataTable FiltrarClientes(string nombre) {
			DataTable dt = new DataTable();
			using(var con = _factory.CreateConnection()) {
				con.Open();
				string sql = @"SELECT p.ID_persona, p.nombre, p.apellido, r.nombre_rol as rol, p.mail, p.telefono, p.domicilio, e.contraseña, p.tipo
					FROM empleados e
					INNER JOIN personas p ON e.ID_persona = p.ID_persona
					INNER JOIN roles r ON e.rol = r.ID_roles
					WHERE p.tipo = 'e'
					AND (@nombre IS NULL OR p.nombre LIKE @nombre OR p.apellido LIKE @nombre);";
				using(var cmd = new MySqlCommand(sql, con)) {
					cmd.Parameters.AddWithValue("@nombre", string.IsNullOrWhiteSpace(nombre) ? (object) DBNull.Value : $"%{nombre}%");
					using(var da = new MySqlDataAdapter(cmd)) {
						da.Fill(dt);
					}
				}
			}
			return dt;
		}


		// NOTE: Delete operation separated for tests -- receives ids and performs deletion
		public void EliminarPersonas(int[] ids) {
			if(ids == null || ids.Length == 0)
				throw new ArgumentException("No ids provided");


			using(var con = _factory.CreateConnection()) {
				con.Open();
				using(var tran = con.BeginTransaction()) {
					try {
						foreach(var id in ids) {
							// Delete from empleados
							using(var cmd = new MySqlCommand("DELETE FROM empleados WHERE ID_persona = @id", con, tran)) {
								cmd.Parameters.AddWithValue("@id", id);
								cmd.ExecuteNonQuery();
							}


							// Delete from personas
							using(var cmd2 = new MySqlCommand("DELETE FROM personas WHERE ID_persona = @id", con, tran)) {
								cmd2.Parameters.AddWithValue("@id", id);
								cmd2.ExecuteNonQuery();
							}
						}
						tran.Commit();
					} catch(MySqlException) {
						tran.Rollback();
						throw;
					}
				}
			}
		}
	}
}
