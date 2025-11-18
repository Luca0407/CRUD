using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Simple_Login_FORM.Services {
	public static class PersonaValidator {
		public static bool EsCorreoValido(string correo) {
			if(string.IsNullOrWhiteSpace(correo))
				return false;
			string patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
			return Regex.IsMatch(correo, patron);
		}

		public static bool EsTelefonoValido(string telefono) {
			if(string.IsNullOrWhiteSpace(telefono))
				return false;
			return telefono.Length == 10 && long.TryParse(telefono, out _);
		}

		public static bool CamposNoVacios(params string[] campos) {
			foreach(var c in campos)
				if(string.IsNullOrWhiteSpace(c))
					return false;
			return true;
		}
	}
}
