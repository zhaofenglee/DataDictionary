import { ModuleWithProviders, NgModule } from '@angular/core';
import { DATA_DICTIONARY_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class DataDictionaryConfigModule {
  static forRoot(): ModuleWithProviders<DataDictionaryConfigModule> {
    return {
      ngModule: DataDictionaryConfigModule,
      providers: [DATA_DICTIONARY_ROUTE_PROVIDERS],
    };
  }
}
