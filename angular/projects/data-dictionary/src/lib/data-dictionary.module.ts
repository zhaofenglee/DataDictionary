import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { DataDictionaryComponent } from './components/data-dictionary.component';
import { DataDictionaryRoutingModule } from './data-dictionary-routing.module';

@NgModule({
  declarations: [DataDictionaryComponent],
  imports: [CoreModule, ThemeSharedModule, DataDictionaryRoutingModule],
  exports: [DataDictionaryComponent],
})
export class DataDictionaryModule {
  static forChild(): ModuleWithProviders<DataDictionaryModule> {
    return {
      ngModule: DataDictionaryModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<DataDictionaryModule> {
    return new LazyModuleFactory(DataDictionaryModule.forChild());
  }
}
