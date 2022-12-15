
resource "azurerm_key_vault" "key_vault" {
  name                        = var.key_vault_name
  location                    = var.location
  resource_group_name         = var.resource_group_name
  enabled_for_disk_encryption = true
  tenant_id                   = data.azurerm_client_config.current.tenant_id
  purge_protection_enabled    = false
  soft_delete_retention_days = 7

  sku_name = "standard"

  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = data.azurerm_client_config.current.object_id

    key_permissions = [
      "Create",
      "Get",
    ]

    secret_permissions = [
        "Set",
      "Get",
      "List",
      "Purge",
      "Recover",
      "Restore",
      "Backup",
      "Delete"
    ]
  }


  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = "704322eb-560a-4a4c-aeb2-fbaaf2394e55"

    secret_permissions = [
      "get",
      "list",
    ]
    certificate_permissions = [
    ]
    key_permissions = [
    ]
  }

  tags = {
    environment = var.environment
  }



  depends_on = [
    azurerm_resource_group.rg_kenrmarket
  ]
}

resource "azurerm_key_vault_secret" "secret_sqldb_auth" {
  name         = "SQL-CONNECTIONSTRING-AUTH"
  key_vault_id = azurerm_key_vault.key_vault.id
  value = module.data.db_sql_connection_string_auth
  depends_on = [
    azurerm_key_vault.key_vault
  ]
}

resource "azurerm_key_vault_secret" "secret_sqldb_cart" {
  name         = "SQL-CONNECTIONSTRING-CART"
  key_vault_id = azurerm_key_vault.key_vault.id
  value = module.data.db_sql_connection_string_carts
  depends_on = [
    azurerm_key_vault.key_vault
  ]
}

resource "azurerm_key_vault_secret" "secret_sqldb_order" {
  name         = "SQL-CONNECTIONSTRING-ORDER"
  key_vault_id = azurerm_key_vault.key_vault.id
  value = module.data.db_sql_connection_string_orders
  depends_on = [
    azurerm_key_vault.key_vault
  ]
}


resource "azurerm_key_vault_secret" "secret_sqldb_customer" {
  name         = "SQL-CONNECTIONSTRING-CUSTOMER"
  key_vault_id = azurerm_key_vault.key_vault.id
  value = module.data.db_sql_connection_string_customers
  depends_on = [
    azurerm_key_vault.key_vault
  ]
}

resource "azurerm_key_vault_secret" "secret_sqldb_payment" {
  name         = "SQL-CONNECTIONSTRING-PAYMENT"
  key_vault_id = azurerm_key_vault.key_vault.id
  value = module.data.db_sql_connection_string_payments
  depends_on = [
    azurerm_key_vault.key_vault
  ]
}

resource "azurerm_key_vault_secret" "secret_mongodb_product" {
  name         = "MONGO-CONNECTIONSTRING-PRODUCT"
  key_vault_id = azurerm_key_vault.key_vault.id
  value = module.data.db_mongo_connection_string_products
  depends_on = [
    azurerm_key_vault.key_vault
  ]
}

resource "azurerm_key_vault_secret" "secret_connection_string_service_bus" {
  name = "SERVICE-BUS-CONNECTIONSTRING"
  key_vault_id = azurerm_key_vault.key_vault.id
  value = module.service_bus.sb_service_bus_connection_string
}

