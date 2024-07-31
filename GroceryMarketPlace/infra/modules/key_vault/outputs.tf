output "name" {
  description = "Specifies the name of the Key Vault"
  value       = azurerm_key_vault.this.name
}

output "vault_id" {
  description = "Specifies the vault id of the Key Vault"
  value       = azurerm_key_vault.this.id
}
