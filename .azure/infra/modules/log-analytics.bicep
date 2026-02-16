@description('Name of the Log Analytics workspace.')
param name string

@description('Location of the workspace.')
param location string

resource workspace 'Microsoft.OperationalInsights/workspaces@2023-09-01' = {
  name: name
  location: location
  properties: {
    retentionInDays: 30
    publicNetworkAccessForIngestion: 'Enabled'
    publicNetworkAccessForQuery: 'Enabled'
  }
}

// The shared key for the workspace is retrieved by callers using listKeys on the workspace resource id.
output id string = workspace.id
output customerId string = workspace.properties.customerId
