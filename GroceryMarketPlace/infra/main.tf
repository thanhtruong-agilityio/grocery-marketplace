module "resource_group" {
  source              = "./modules/resource_group"
  resource_group_name = var.resource_group_name
  location            = var.location
  environment         = var.environment
  app_name            = var.app_name
}

module "log_analytics_workspace" {
  source              = "./modules/log_analytics"
  resource_group_name = module.resource_group.name
  name                = var.log_analytics_workspace_name
  location            = var.location
  tags                = var.tags
}

module "virtual_network" {
  source                       = "./modules/virtual_network"
  resource_group_name          = module.resource_group.name
  vnet_name                    = var.vnet_name
  location                     = var.location
  address_space                = var.vnet_address_space
  tags                         = var.tags
  log_analytics_workspace_id   = module.log_analytics_workspace.id
  log_analytics_retention_days = var.log_analytics_retention_days
  enable_ddos_protection       = var.enable_ddos_protection

  subnets = [
    {
      name : var.aca_subnet_name
      address_prefixes : var.aca_subnet_address_prefix
      private_endpoint_network_policies_enabled : true
      private_link_service_network_policies_enabled : false
    },
    {
      name : var.private_endpoint_subnet_name
      address_prefixes : var.private_endpoint_subnet_address_prefix
      private_endpoint_network_policies_enabled : true
      private_link_service_network_policies_enabled : false
    }
  ]
}

module "container_apps" {
  source                   = "./modules/container_apps"
  environment              = var.environment
  location                 = var.location
  resource_group_name      = var.resource_group_name
  tags                     = var.tags
  infrastructure_subnet_id = module.virtual_network.subnet_ids[var.aca_subnet_name]
  instrumentation_key      = module.application_insights.instrumentation_key
  workspace_id             = module.log_analytics_workspace.id
  container_apps           = var.container_apps
}

module "application_insights" {
  source              = "./modules/application_insights"
  name                = var.application_insights_name
  location            = var.location
  resource_group_name = module.resource_group.name
  tags                = var.tags
  application_type    = var.application_insights_application_type
  workspace_id        = module.log_analytics_workspace.id
}
