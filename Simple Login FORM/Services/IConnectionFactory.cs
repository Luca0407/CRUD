using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace Simple_Login_FORM.Services {
	public interface IConnectionFactory {
		MySqlConnection CreateConnection();
	}
}
