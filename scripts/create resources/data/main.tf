output "db_sql_connection_string_payments" {
  value = "Server=tcp:${azurerm_mssql_server.db_azuresqlserver.name}.database.windows.net,1433;Database =${azurerm_mssql_database.db_paymentsDb.name} ;User ID=${var.sql_server_user};Password=${var.sql_server_password};Encrypt=True;Connection Timeout=30;"
}

output "db_sql_connection_string_orders" {
  value = "Server=tcp:${azurerm_mssql_server.db_azuresqlserver.name}.database.windows.net,1433;Database =${azurerm_mssql_database.db_ordersDb.name} ;User ID=${var.sql_server_user};Password=${var.sql_server_password};Encrypt=True;Connection Timeout=30;"
}


output "db_sql_connection_string_customers" {
  value = "Server=tcp:${azurerm_mssql_server.db_azuresqlserver.name}.database.windows.net,1433;Database =${azurerm_mssql_database.db_customersDb.name} ;User ID=${var.sql_server_user};Password=${var.sql_server_password};Encrypt=True;Connection Timeout=30;"
}

output "db_sql_connection_string_carts" {
  value = "Server=tcp:${azurerm_mssql_server.db_azuresqlserver.name}.database.windows.net,1433;Database =${azurerm_mssql_database.db_cartsDb.name} ;User ID=${var.sql_server_user};Password=${var.sql_server_password};Encrypt=True;Connection Timeout=30;"
}

output "db_sql_connection_string_auth" {
  value = "Server=tcp:${azurerm_mssql_server.db_azuresqlserver.name}.database.windows.net,1433;Database =${azurerm_mssql_database.db_authDb.name} ;User ID=${var.sql_server_user};Password=${var.sql_server_password};Encrypt=True;Connection Timeout=30;"
}


output "db_mongo_connection_string_products" {
    value = tostring("${azurerm_cosmosdb_account.db_nosql_productssvc.connection_strings[0]}")
}
  