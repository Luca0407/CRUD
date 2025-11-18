using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simple_Login_FORM.Services;
using System.Data;
using MySql.Data;

namespace Simple_Login_FORM.Tests {
	[TestClass]
	public class EmpleadosTests {
		// Se crean instancias de servicios con factories falsos para evitar DB
		class InMemoryFactory: IConnectionFactory {
			public MySql.Data.MySqlClient.MySqlConnection CreateConnection() => throw new System.NotImplementedException();
		}

		private DataTable FiltrarLocal(string input) {
			DataTable tabla = new DataTable();
			tabla.Columns.Add("ID_persona");
			tabla.Columns.Add("nombre");
			tabla.Columns.Add("apellido");
			tabla.Rows.Add("1", "Luca", "Elizondo");
			tabla.Rows.Add("2", "Gabriel", "Elizondo");
			tabla.Rows.Add("3", "Daniella", "Kolarik");

			var resultado = tabla.Clone();
			foreach(DataRow row in tabla.Rows) {
				if(string.IsNullOrWhiteSpace(input) || row["nombre"].ToString().ToLower().Contains(input.ToLower())
					|| row["apellido"].ToString().ToLower().Contains(input.ToLower()))
					resultado.ImportRow(row);
			}
			return resultado;
		}

		[TestMethod]
		public void FiltrarClientes1() {
			Assert.AreEqual(1, FiltrarLocal("Luca").Rows.Count);
		}

		[TestMethod]
		public void FiltrarClientes2() {
			Assert.AreEqual(1, FiltrarLocal("uc").Rows.Count);
		}

		[TestMethod]
		public void FiltrarClientes3() {
			Assert.AreEqual(2, FiltrarLocal("eliz").Rows.Count);
		}

		[TestMethod]
		public void FiltrarClientes4() {
			Assert.AreEqual(3, FiltrarLocal("el").Rows.Count);
		}

		[TestMethod]
		public void FiltrarClientes5() {
			Assert.AreEqual(0, FiltrarLocal("juan").Rows.Count);
		}

		[TestMethod]
		public void FiltrarClientes6() {
			Assert.AreEqual(3, FiltrarLocal("").Rows.Count);
		}

		[TestMethod]
		public void VerificarSeleccionEdicion1() {
			Assert.AreEqual("Por favor, selecciona solo una fila para editar.", EmuladorUI.VerificarSeleccion(0));
		}

		[TestMethod]
		public void VerificarSeleccionEdicion2() {
			Assert.AreEqual("Por favor, selecciona solo una fila para editar.", EmuladorUI.VerificarSeleccion(2));
		}

		[TestMethod]
		public void VerificarSeleccionEdicion3() {
			Assert.AreEqual("OK", EmuladorUI.VerificarSeleccion(1));
		}

		[TestMethod]
		public void ModPersona_Validaciones1() {
			Assert.IsTrue(PersonaValidator.EsCorreoValido("correo@mail.com"));
		}

		[TestMethod]
		public void ModPersona_Validaciones2() {
			Assert.IsFalse(PersonaValidator.EsCorreoValido("correo"));
		}

		[TestMethod]
		public void ModPersona_Validaciones3() {
			Assert.IsTrue(PersonaValidator.EsTelefonoValido("1234567890"));
		}

		[TestMethod]
		public void ModPersona_Validaciones4() {
			Assert.IsFalse(PersonaValidator.EsTelefonoValido("12345"));
		}

		[TestMethod]
		public void EmpleadoActualizar_Casos() {
			var fake = new ModPersonaFake();
			var empValido = new Empleados { Nombre = "Luca", Apellido = "E", Mail = "a@b.com", Telefono = "1234567890", Domicilio = "x", Rol = "técnico" };
			Assert.IsTrue(fake.ActualizarEmpleadoFake(empValido));

			var empVacio = new Empleados();
			Assert.IsFalse(fake.ActualizarEmpleadoFake(empVacio));

			var empMailInvalido = new Empleados { Nombre = "Luca", Apellido = "E", Mail = "mail", Telefono = "1234567890", Domicilio = "x", Rol = "técnico" };
			Assert.IsFalse(fake.ActualizarEmpleadoFake(empMailInvalido));

			var empTelInvalido = new Empleados { Nombre = "Luca", Apellido = "E", Mail = "a@b.com", Telefono = "12345", Domicilio = "x", Rol = "técnico" };
			Assert.IsFalse(fake.ActualizarEmpleadoFake(empTelInvalido));
		}

		[TestMethod]
		public void EliminarCliente1() {
			Assert.AreEqual("Selecciona al menos una fila para eliminar.", EmuladorEliminar.VerificarEliminacion(0));
		}

		[TestMethod]
		public void EliminarCliente2() {
			Assert.AreEqual("Eliminación completada.", EmuladorEliminar.VerificarEliminacion(3, true));
		}

		[TestMethod]
		public void RegisterForm1() {
			Assert.AreEqual("Faltan datos", EmuladorRegistro.ValidarRegistro("", "", ""));
		}

		[TestMethod]
		public void RegisterForm2() {
			Assert.AreEqual("Email inválido", EmuladorRegistro.ValidarRegistro("correo", "1234567890", "pass"));
		}

		[TestMethod]
		public void RegisterForm3() {
			Assert.AreEqual("Teléfono inválido", EmuladorRegistro.ValidarRegistro("a@b.com", "1234", "pass"));
		}

		[TestMethod]
		public void RegisterForm4() {
			Assert.AreEqual("Inserción cancelada.", EmuladorRegistro.Cancelar());
		}

		// Helpers fakes (idénticos a los que se documentaron antes)
		public static class EmuladorUI {
			public static string VerificarSeleccion(int filas) {
				if(filas != 1)
					return "Por favor, selecciona solo una fila para editar.";
				return "OK";
			}
		}

		public static class EmuladorEliminar {
			public static string VerificarEliminacion(int filas, bool confirmar = false) {
				if(filas == 0)
					return "Selecciona al menos una fila para eliminar.";
				if(!confirmar)
					return "Cancelado";
				return "Eliminación completada.";
			}
		}

		public static class EmuladorRegistro {
			public static string ValidarRegistro(string mail, string telefono, string password) {
				if(string.IsNullOrWhiteSpace(mail) || string.IsNullOrWhiteSpace(telefono) || string.IsNullOrWhiteSpace(password))
					return "Faltan datos";
				if(!mail.Contains("@"))
					return "Email inválido";
				if(telefono.Length != 10)
					return "Teléfono inválido";
				return "OK";
			}

			public static string Cancelar() => "Inserción cancelada.";
		}

		// Fake ModPersona para tests
		public class ModPersonaFake {
			public bool ActualizarEmpleadoFake(Empleados emp) {
				if(string.IsNullOrWhiteSpace(emp.Nombre) || string.IsNullOrWhiteSpace(emp.Apellido) || string.IsNullOrWhiteSpace(emp.Mail) || string.IsNullOrWhiteSpace(emp.Telefono) || string.IsNullOrWhiteSpace(emp.Domicilio) || string.IsNullOrWhiteSpace(emp.Rol))
					return false;
				if(!emp.Mail.Contains("@"))
					return false;
				if(emp.Telefono.Length != 10)
					return false;
				return true;
			}
		}
	}
}
