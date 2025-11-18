using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Simple_Login_FORM.Services {
	public class RegisterService {
		private readonly IConnectionFactory _factory;
		public RegisterService(IConnectionFactory factory) {
			_factory = factory;
		}

		public void RegistrarEmpleado(string mail, string telefono, string password, string nombre, string apellido, string domicilio, string rol) {
			// Validaciones
			if(!PersonaValidator.CamposNoVacios(mail, telefono, password, nombre, apellido))
				throw new ArgumentException("Faltan datos para realizar el registro");


			if(!PersonaValidator.EsCorreoValido(mail))
				throw new ArgumentException("El correo electrónico no es válido");


			if(!PersonaValidator.EsTelefonoValido(telefono))
				throw new ArgumentException("El teléfono no es válido");

			using(var con = _factory.CreateConnection()) {
				con.Open();
				using(var cmd = con.CreateCommand()) {
					cmd.CommandText = @"INSERT INTO personas (mail, nombre, apellido, telefono, domicilio, tipo)
						VALUES (@mail, @nombre, @apellido, @telefono, @domicilio, @tipo)";
					cmd.Parameters.AddWithValue("@mail", mail);
					cmd.Parameters.AddWithValue("@nombre", nombre);
					cmd.Parameters.AddWithValue("@apellido", apellido);
					cmd.Parameters.AddWithValue("@telefono", telefono);
					cmd.Parameters.AddWithValue("@domicilio", domicilio ?? string.Empty);
					cmd.Parameters.AddWithValue("@tipo", "e");
					cmd.ExecuteNonQuery();
				}

				using(var cmd = con.CreateCommand()) {
					cmd.CommandText = @"INSERT INTO empleados (rol, contraseña, ID_persona, activo)
						VALUES ((SELECT ID_roles FROM roles WHERE nombre_rol = @rol), @password, LAST_INSERT_ID(), 0)";
					cmd.Parameters.AddWithValue("@rol", rol ?? "técnico");
					cmd.Parameters.AddWithValue("@password", password);
					cmd.ExecuteNonQuery();
				}
			}
		}
	}
}
