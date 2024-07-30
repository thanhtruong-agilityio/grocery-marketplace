location            = "southeastasia" // South East Asia
resource_group_name = "grocery-marketplace-rg-dev"
app_name            = "GroceryMarketplaceDev"

log_analytics_workspace_name = "GroceryMarketplaceLogsDev"
log_analytics_retention_days = 10
environment                  = "dev"

vnet_name                              = "vnet-gm-dev"
vnet_address_space                     = ["10.0.0.0/16"]
aca_subnet_name                        = "ContainerApps"
aca_subnet_address_prefix              = ["10.0.0.0/20"]
private_endpoint_subnet_name           = "PrivateEndpoints"
private_endpoint_subnet_address_prefix = ["10.0.16.0/24"]
enable_ddos_protection                 = false
