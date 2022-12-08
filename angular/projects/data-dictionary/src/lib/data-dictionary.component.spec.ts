import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { DataDictionaryComponent } from './components/data-dictionary.component';
import { DataDictionaryService } from '@j-s.Abp/data-dictionary';
import { of } from 'rxjs';

describe('DataDictionaryComponent', () => {
  let component: DataDictionaryComponent;
  let fixture: ComponentFixture<DataDictionaryComponent>;
  const mockDataDictionaryService = jasmine.createSpyObj('DataDictionaryService', {
    sample: of([]),
  });
  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [DataDictionaryComponent],
      providers: [
        {
          provide: DataDictionaryService,
          useValue: mockDataDictionaryService,
        },
      ],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DataDictionaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
