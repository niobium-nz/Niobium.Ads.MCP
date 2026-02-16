@description('Name of the Container Apps managed environment.')
param name string

@description('Location of the managed environment.')
param location string

@description('The resourceId of the Log Analytics workspace.')
param logAnalyticsWorkspaceId string

@description('The customerId of the Log Analytics workspace.')
param logAnalyticsCustomerId string

// Use listKeys to obtain the shared key for the workspace at deployment time.
var laKeys = listKeys(logAnalyticsWorkspaceId, '2023-09-01')

resource managedEnvironment 'Microsoft.App/managedEnvironments@2024-03-01' = {
  name: name
  location: location
  properties: {
    appLogsConfiguration: {
      destination: 'log-analytics'
      logAnalyticsConfiguration: {
        customerId: logAnalyticsCustomerId
        sharedKey: laKeys.primarySharedKey
      }
    }
  }
}

output id string = managedEnvironment.id
