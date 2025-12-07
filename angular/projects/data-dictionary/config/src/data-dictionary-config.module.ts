import {makeEnvironmentProviders} from '@angular/core';
import { DATA_DICTIONARY_ROUTE_PROVIDERS } from './providers/route.provider';

export function provideDataDictionaryConfig() {
  return makeEnvironmentProviders([DATA_DICTIONARY_ROUTE_PROVIDERS])
}
