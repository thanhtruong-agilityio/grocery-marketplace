locals {
  module_tag = {
    environment   = var.environment,
    module        = basename(abspath(path.module)),
    app_name      = var.app_name,
    createdWith   = "Terraform",
    containerApps = "true"
  }
}

resource "azurerm_resource_group" "grocery_marketplace" {
  name     = var.resource_group_name
  location = var.location
  tags     = local.module_tag
}
