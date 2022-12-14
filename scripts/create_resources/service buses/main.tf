output "sb_service_bus_connection_string" {
  value = azurerm_servicebus_namespace_authorization_rule.sb_namespace_kenrmarket_authorization_rule.primary_connection_string
}

  