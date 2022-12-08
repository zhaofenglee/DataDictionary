import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'DataDictionary',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44307/',
    redirectUri: baseUrl,
    clientId: 'DataDictionary_App',
    responseType: 'code',
    scope: 'offline_access DataDictionary',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44307',
      rootNamespace: 'JS.Abp.DataDictionary',
    },
    DataDictionary: {
      url: 'https://localhost:44334',
      rootNamespace: 'JS.Abp.DataDictionary',
    },
  },
} as Environment;
