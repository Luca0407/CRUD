using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Login_FORM.Services {
	public class DefaultConnectionFactory: IConnectionFactory {
		private readonly string _connectionString;

		public DefaultConnectionFactory(string connectionString) {
			_connectionString = connectionString;
		}

		public MySqlConnection CreateConnection() {
			return new MySqlConnection(_connectionString);
		}
	}
}
