import { TestBed } from '@angular/core/testing';
import { DataDictionaryService } from './services/data-dictionary.service';
import { RestService } from '@abp/ng.core';

describe('DataDictionaryService', () => {
  let service: DataDictionaryService;
  const mockRestService = jasmine.createSpyObj('RestService', ['request']);
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        {
          provide: RestService,
          useValue: mockRestService,
        },
      ],
    });
    service = TestBed.inject(DataDictionaryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
