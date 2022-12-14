resource "azurerm_servicebus_namespace" "sb_namespace_kenrmarket" {
  name                = "kenrmarketbus"
  location            = var.location
  resource_group_name = var.resource_group_name
  sku                 = "Basic"

  tags = {
    source = "terraform"
  }
}

resource "azurerm_servicebus_namespace_authorization_rule" "sb_namespace_kenrmarket_authorization_rule" {
  name         = "masstransitaccesspolicy"
  namespace_id = azurerm_servicebus_namespace.sb_namespace_kenrmarket.id
  listen = true
  send   = true
  manage = true
  depends_on = [
    azurerm_servicebus_namespace.sb_namespace_kenrmarket
  ]
}