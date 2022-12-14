

Function GetKeyVaultEntries(
    [string]$keyVaultName
)
{
    $keyVaultEntries = (az keyvault secret list --vault-name $keyVaultName | ConvertFrom-Json) | Select-Object id, name
    
    foreach($entry in $keyVaultEntries)
    {
        $secretValue = (az keyvault secret show --id $entry.id | ConvertFrom-Json) | Select-Object name, value
        $header = $header + "   $($secretValue.name): ""$($secretValue.value)""`n"
    }


    $header > local-secrets.env
}


echo $args[0]
GetKeyVaultEntries($args[0])