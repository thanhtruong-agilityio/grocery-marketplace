variable "location" {
  type        = string
  description = "Specifies the supported Azure location where the resource exists. Changing this forces a new resource to be created."
}

variable "resource_group_name" {
  type        = string
  description = "The name of the resource group in which to create the Azure rerouce. Changing this forces a new resource to be created."
}

variable "tags" {
  type        = map(string)
  description = "A mapping of tags to assign to the resource."
  default     = {}
}

variable "prefixes" {
  type        = list(string)
  description = "List of prefixes used for resource name."
}

variable "name" {
  type        = string
  description = "Specifies the name of the Key Vault. Changing this forces a new resource to be created. The name must be globally unique. If the vault is in a recoverable state then the vault will need to be purged before reusing the name."
}

variable "sku" {
  type        = string
  description = "The Name of the SKU used for this Key Vault. Possible values are standard and premium."
}

variable "soft_delete_retention_days" {
  type        = number
  description = "The number of days that items should be retained for once soft-deleted. This value can be between 7 and 90 (the default) days."
  default     = 90
}

variable "enable_rbac_authorization" {
  type        = bool
  description = "Boolean flag to specify whether Azure Key Vault uses Role Based Access Control (RBAC) for authorization of data actions."
  default     = true
}

variable "public_network_access_enabled" {
  type        = bool
  description = "Whether the public network access is enabled? Defaults to true."
  default     = true
}

variable "role_assignments" {
  type = list(object({
    role_definition_name = string
    principal_id         = string
  }))
  description = "A list of role assignments for a principal (users, groups, service principals, or managed identities) to access resource."
  default     = []
}

variable "access_policies" {
  type = list(object({
    object_id               = string,
    certificate_permissions = list(string)
    key_permissions         = list(string)
    secret_permissions      = list(string)
    storage_permissions     = list(string)
  }))
  description = "A list of access policies for an object_id (user, service principal, security group) to access resource."
  default     = []
}

variable "network_acls" {
  type = object({
    default_action             = string
    bypass                     = string
    ip_rules                   = optional(list(string))
    virtual_network_subnet_ids = optional(list(string))
  })
  description = "Network rules restricting access to the Key Vault."
  default     = null
}

variable "enable_private_endpoint" {
  type        = bool
  description = "The flag to specify whether private endpoint is enabled for the resource."
  default     = false
}

variable "vnet_id" {
  type        = string
  description = "The ID of the Virtual Network that should be linked to the DNS Zone. Changing this forces a new resource to be created."
  default     = null
}

variable "private_endpoint_subnet_id" {
  type        = string
  description = "The ID of the Subnet from which Private IP Addresses will be allocated for this Private Endpoint. Changing this forces a new resource to be created."
  default     = null
}
