using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Login_FORM {
	public class Empleados {
		public string Nombre {
			get; set;
		}
		public string Apellido {
			get; set;
		}
		public string Mail {
			get; set;
		}
		public string Domicilio {
			get; set;
		}
		public string Rol {
			get; set;
		}
		public string Telefono {
			get; set;
		}
	}

	public class Clientes {
		public string Nombre {
			get; set;
		}
		public string Apellido {
			get; set;
		}
		public string Mail {
			get; set;
		}
		public string Domicilio {
			get; set;
		}
		public string Dni {
			get; set;
		}
		public string Telefono {
			get; set;
		}
		public string Cuil {
			get; set;
		}
	}

	public class Proveedores {
		public string Nombre {
			get; set;
		}
		public string Mail {
			get; set;
		}
		public string Telefono {
			get; set;
		}
		public string Domicilio {
			get; set;
		}
		public string Pagina {
			get; set;
		}
		
		public string Cuit {
			get; set;
		}
	}
}
