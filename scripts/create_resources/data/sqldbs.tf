

resource "azurerm_mssql_server" "db_azuresqlserver" {
  administrator_login = var.sql_server_user
  administrator_login_password = var.sql_server_password
  location            = var.location
  name                = "kenrmarketsqldbserver"
  resource_group_name = var.resource_group_name
  version             = "12.0"
}


resource "azurerm_mssql_server_transparent_data_encryption" "azure_tde_config" {
  server_id = azurerm_mssql_server.db_azuresqlserver.id
  
  depends_on = [
    azurerm_mssql_server.db_azuresqlserver,
  ]
}
resource "azurerm_mssql_firewall_rule" "db_firewall_policy_all_azure" {
  name             = "AllowAllWindowsAzureIps"
  server_id = azurerm_mssql_server.db_azuresqlserver.id
  start_ip_address = "0.0.0.0"
  end_ip_address   = "0.0.0.0"
  depends_on = [
    azurerm_mssql_server.db_azuresqlserver,
  ]
}

resource "azurerm_mssql_firewall_rule" "db_firewall_policy" {
  name             = "AllowAll1"
  server_id = azurerm_mssql_server.db_azuresqlserver.id
  start_ip_address = "0.0.0.1"
  end_ip_address   = "255.255.255.255"
  depends_on = [
    azurerm_mssql_server.db_azuresqlserver,
  ]
}



resource "azurerm_mssql_database" "db_authDb" {
  server_id = azurerm_mssql_server.db_azuresqlserver.id
  name                 = "authDb"
  max_size_gb                 = 4
  min_capacity                = 0.5
  read_replica_count          = 0
  sku_name                    = "GP_S_Gen5_1"
  auto_pause_delay_in_minutes = 60
  storage_account_type = "Local"
  depends_on = [
    azurerm_mssql_server.db_azuresqlserver,
  ]
}

resource "azurerm_mssql_database" "db_cartsDb" {
  server_id = azurerm_mssql_server.db_azuresqlserver.id
  name                 = "cartsDb"
  max_size_gb                 = 4
  min_capacity                = 0.5
  read_replica_count          = 0
  read_scale                  = false
  sku_name                    = "GP_S_Gen5_1"
  auto_pause_delay_in_minutes = 60
  storage_account_type = "Local"
  depends_on = [
    azurerm_mssql_server.db_azuresqlserver,
  ]
}

resource "azurerm_mssql_database" "db_ordersDb" {
  server_id = azurerm_mssql_server.db_azuresqlserver.id
  name                 = "ordersDb"
  max_size_gb                 = 4
  min_capacity                = 0.5
  read_replica_count          = 0
  read_scale                  = false
  sku_name                    = "GP_S_Gen5_1"
  auto_pause_delay_in_minutes = 60
  storage_account_type = "Local"
  depends_on = [
    azurerm_mssql_server.db_azuresqlserver,
  ]
}

resource "azurerm_mssql_database" "db_customersDb" {
  server_id = azurerm_mssql_server.db_azuresqlserver.id
  name                 = "customersDb"
  max_size_gb                 = 4
  min_capacity                = 0.5
  read_replica_count          = 0
  read_scale                  = false
  sku_name                    = "GP_S_Gen5_1"
  auto_pause_delay_in_minutes = 60
  storage_account_type = "Local"
  depends_on = [
    azurerm_mssql_server.db_azuresqlserver,
  ]
}


resource "azurerm_mssql_database" "db_paymentsDb" {
  server_id = azurerm_mssql_server.db_azuresqlserver.id
  name                 = "paymentsDb"
  max_size_gb                 = 4
  min_capacity                = 0.5
  read_replica_count          = 0
  read_scale                  = false
  sku_name                    = "GP_S_Gen5_1"
  auto_pause_delay_in_minutes = 60
  storage_account_type = "Local"
  depends_on = [
    azurerm_mssql_server.db_azuresqlserver,
  ]
}
