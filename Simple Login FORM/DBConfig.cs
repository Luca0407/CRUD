using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Login_FORM {
	public static class DBConfig {
		public static string UserId {
			get; set;
		}
		public static string Password {
			get; set;
		}

		// Método para devolver la cadena de conexión armada
		public static string GetConnectionString() {
			return $@"Server=localhost; port=3306; Database=celulares; User Id={UserId}; password='{Password}'";
		}
	}
}
