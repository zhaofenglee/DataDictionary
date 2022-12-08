import { Injectable } from '@angular/core';
import { RestService } from '@abp/ng.core';

@Injectable({
  providedIn: 'root',
})
export class DataDictionaryService {
  apiName = 'DataDictionary';

  constructor(private restService: RestService) {}

  sample() {
    return this.restService.request<void, any>(
      { method: 'GET', url: '/api/DataDictionary/sample' },
      { apiName: this.apiName }
    );
  }
}
