using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Login_FORM 
{
	public class item {
		// Personas
		public string ID {
			get; set;
		}
		public string Name {
			get; set;
		}
		public string Mail {
			get; set;
		}
		public string Phone {
			get; set;
		}

		// Empleados
		public string EmployeeID {
			get; set;
		}
		public string Role {
			get; set;
		}
		public string PersonaID {
			get; set;
		}

		// Items (productos)
		public string ProductID {
			get; set;
		}
		public string ProductName {
			get; set;
		}
		public double? Price {
			get; set;
		}  // nullable porque no siempre hay precio
		public bool? IsActive {
			get; set;
		} // nullable porque no siempre aplica
	}

}
